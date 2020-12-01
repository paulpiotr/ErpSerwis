using System.Collections.Generic;
using System.Threading.Tasks;
using ErpSerwis.Core.Models;

namespace ErpSerwis.Core.Repository.Interface
{
    public interface IStudentsGradesRepository
    {
        IEnumerable<StudentsGrades> Add(Students student, decimal grade);

        Task<IEnumerable<StudentsGrades>> AddAsync(Students student, decimal grade);
    }
}
