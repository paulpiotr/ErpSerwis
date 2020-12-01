using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ErpSerwis.Core.Data;
using ErpSerwis.Core.Models;
using ErpSerwis.Core.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ErpSerwis.Core.Repository
{
    public class StudentsGradesRepository : IStudentsGradesRepository
    {
        private IEnumerable<StudentsGrades> _studentsGrades;

        #region private static readonly log4net.ILog log4net
        /// <summary>
        /// Log4net Logger
        /// Log4net Logger
        /// </summary>
        private static readonly log4net.ILog Log4net = Log4netLogger.Log4netLogger.GetLog4netInstance(MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region protected Data.IUIntegrationSystemDataDbContext context...
        /// <summary>
        /// Kontekst do bazy danych jako Data.IUIntegrationSystemDataDbContext
        /// Context to the database as Data.IUIntegrationSystemDataDbContext
        /// </summary>
        private readonly ErpSerwisDbContext _context;
        #endregion

        public StudentsGradesRepository()
        {
            _context = new ErpSerwisDbContext();
        }

        public StudentsGradesRepository(ErpSerwisDbContext context)
        {
            _context = context;
        }

        public static StudentsGradesRepository GetInstance()
        {
            return new StudentsGradesRepository();
        }

        public static StudentsGradesRepository GetInstance(ErpSerwisDbContext context)
        {
            return new StudentsGradesRepository(context);
        }

        public IEnumerable<StudentsGrades> Add(Students student, decimal grade)
        {
            StudentsGrades studentsGrades = null;
            try
            {
                studentsGrades = new StudentsGrades { StudentId = student.Id, Grade = grade };
                _context.Entry(studentsGrades).State = EntityState.Added;
                _context.SaveChanges();
                _studentsGrades = _context.StudentsGrades.Where(w => w.StudentId == student.Id).ToList();
            }
            catch (Exception e)
            {
                Log4net.Error(string.Format("\n{0}\n{1}\n{2}\n{3}\n", e.GetType(), e.InnerException?.GetType(), e.Message, e.StackTrace), e);
            }
            return _studentsGrades;
        }

        public async Task<IEnumerable<StudentsGrades>> AddAsync(Students student, decimal grade)
        {
            StudentsGrades studentsGrades = null;
            try
            {
                studentsGrades = new StudentsGrades { StudentId = student.Id, Grade = grade };
                _context.Entry(studentsGrades).State = EntityState.Added;
                await _context.SaveChangesAsync();
                _studentsGrades = await _context.StudentsGrades.Where(w => w.StudentId == student.Id).ToListAsync();
            }
            catch (Exception e)
            {
                Log4net.Error(string.Format("\n{0}\n{1}\n{2}\n{3}\n", e.GetType(), e.InnerException?.GetType(), e.Message, e.StackTrace), e);
            }
            return _studentsGrades;
        }
    }
}
