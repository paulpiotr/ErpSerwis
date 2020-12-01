using System.Collections.Generic;
using System.Threading.Tasks;
using ErpSerwis.Core.Models;

namespace ErpSerwis.Core.Repository.Interface
{
    public interface IStudentsRepository
    {
        IEnumerable<Students> GetStudents(int take = 0);

        Task<IEnumerable<Students>> GetStudentsAsync(int take = 0);

        bool CreateExampleStudents(int count);

        Task<bool> CreateExampleStudentsAsync(int count);

        Students Add(Students student);

        Task<Students> AddAsync(Students student);

        Students Save(Students student);

        Task<Students> SaveAsync(Students student);

        Students Delete(Students student);

        Task<Students> DeleteAsync(Students student);
    }
}
