using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ErpSerwis.Core.Models;
using ErpSerwis.Core.Repository;
using Telerik.Windows.Controls;

namespace ErpSerwis.WpfApp.ViewCommandProvider
{
    public class StudentsGridDataCommandAddGradeProvider : ICommand
    {
        private ICommand _commandAddGrade;
        private Students _student;
        public event EventHandler CanExecuteChanged;

        public Students Student
        {
            get => _student;
            set
            {
                if (value != _student)
                {
                    _student = value;
                }
            }
        }

        public ICommand CommandAddGrade
        {
            get
            {
                if (null == _commandAddGrade)
                {
                    _commandAddGrade = new DelegateCommand(Execute);
                }
                return _commandAddGrade;
            }
        }

        public void Execute(object parameter)
        {
            Student = (Students)parameter;
            var dialogParameters = new DialogParameters()
            {
                Content = $"Numer indeksu:\n{Student.IndexNumber}",
                Closed = AddGradePrompt_OnClosed,
                Header = $"Dodaj ocenę student:\n{Student.Name} {Student.Surname}\nId: {Student.Id}"
            };
            RadWindow.Prompt(dialogParameters);
        }

        private void AddGradePrompt_OnClosed(object sender, WindowClosedEventArgs e)
        {
            decimal grade = 0;
            if (null != Student && null != e.PromptResult && decimal.TryParse(e.PromptResult, out grade) && grade >= 2 && grade <= 5)
            {
                var task = Task.Run(async () =>
                {
                    Student.StudentsGrades = (ICollection<StudentsGrades>)await StudentsGradesRepository.GetInstance().AddAsync(Student, grade);
                });
                task.GetAwaiter().OnCompleted(() =>
                {
                    Student.EndAddGrade();
                });
            }
            else
            {
                MessageBox.Show($"Sprawdź, czy wyrano sudenta ({ Student?.Id }).\nWpisz poprawną ocenę z przedziału od 2 do 5 (wpisano { grade }).\n", "Popraw dane!");
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
