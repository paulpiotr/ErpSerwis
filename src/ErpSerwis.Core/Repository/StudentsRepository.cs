using System;
using System.Collections.Generic;

/* Niescalona zmiana z projektu „ErpSerwis.Core (net5.0)”
Przed:
using ErpSerwis.Core.Models;
using ErpSerwis.Core.Data;
Po:
using ErpSerwis.Collections.ObjectModel;
using System.Linq;
*/

/* Niescalona zmiana z projektu „ErpSerwis.Core (netstandard2.1)”
Przed:
using ErpSerwis.Core.Models;
using ErpSerwis.Core.Data;
Po:
using ErpSerwis.Collections.ObjectModel;
using System.Linq;
*/
using System.Linq;
using System.Reflection;

/* Niescalona zmiana z projektu „ErpSerwis.Core (net5.0)”
Przed:
using System.Linq;
using System.Reflection;
Po:
using System.Text;
using System.Threading.Tasks;
*/

/* Niescalona zmiana z projektu „ErpSerwis.Core (netstandard2.1)”
Przed:
using System.Linq;
using System.Reflection;
Po:
using System.Text;
using System.Threading.Tasks;
*/
using System.Threading.Tasks;
using ErpSerwis.Core.Data;
using ErpSerwis.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ErpSerwis.Core.Repository
{
    public class StudentsRepository : Interface.IStudentsRepository
    {
        private IEnumerable<Students> _students;

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

        public StudentsRepository()
        {
            _context = new ErpSerwisDbContext();
        }

        public StudentsRepository(ErpSerwisDbContext context)
        {
            _context = context;
        }

        public static StudentsRepository GetInstance()
        {
            return new StudentsRepository();
        }

        public static StudentsRepository GetInstance(ErpSerwisDbContext context)
        {
            return new StudentsRepository(context);
        }

        public IEnumerable<Students> GetStudents(int take = 0)
        {
            _students = take > 0 ? _context.Students.Include(i => i.StudentsGrades).Take(take).ToList() : _context.Students.Include(i => i.StudentsGrades).ToList();
            return _students;
        }

        public async Task<IEnumerable<Students>> GetStudentsAsync(int take = 0)
        {
            _students = await _context.Students.ToListAsync();
            _students = take > 0 ? await _context.Students.Include(i => i.StudentsGrades).Take(take).ToListAsync() : await _context.Students.Include(i => i.StudentsGrades).ToListAsync();
            return _students;
        }

        public bool CreateExampleStudents(int count = 100)
        {
            try
            {
                IList<Students> _students = new List<Students>();
                for (var i = 0; i < count; i++)
                {
                    DateTime dateTime = new DateTime(DateTime.Now.Year - new Random().Next(18, 100), new Random().Next(1, 12), 1).AddDays(new Random().Next(0, 31));
                    _students.Add(new Students() { Id = NetAppCommon.Helpers.Object.ObjectHelper.GuidFromString(i.ToString("D8")), Name = string.Format("{0} {1}", "Imię", i.ToString("D8")), Surname = string.Format("{0} {1}", "Nazwisko", i.ToString("D8")), DateOfBirth = dateTime, IndexNumber = i.ToString("D8") });
                }
                try
                {
                    Log4net.Info($"Remove tmp data { _students.Count() }");
                    _context.Students.RemoveRange(_context.Students.ToList());
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    Log4net.Error(string.Format("\n{0}\n{1}\n{2}\n{3}\n", e.GetType(), e.InnerException?.GetType(), e.Message, e.StackTrace), e);
                }
                try
                {
                    Log4net.Info($"Add tmp data { _students }");
                    _context.AddRange(_students.ToList());
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    Log4net.Error(string.Format("\n{0}\n{1}\n{2}\n{3}\n", e.GetType(), e.InnerException?.GetType(), e.Message, e.StackTrace), e);
                }
                return true;
            }
            catch (Exception e)
            {
                Log4net.Error(string.Format("\n{0}\n{1}\n{2}\n{3}\n", e.GetType(), e.InnerException?.GetType(), e.Message, e.StackTrace), e);
            }
            return false;
        }

        public Task<bool> CreateExampleStudentsAsync(int count) => throw new NotImplementedException();

        public Students Add(Students student)
        {
            try
            {
                _context.Entry(student).State = EntityState.Added;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Log4net.Error(string.Format("\n{0}\n{1}\n{2}\n{3}\n", e.GetType(), e.InnerException?.GetType(), e.Message, e.StackTrace), e);
            }
            return student;
        }

        public async Task<Students> AddAsync(Students student)
        {
            try
            {
                _context.Entry(student).State = EntityState.Added;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                await Task.Run(() => { Log4net.Error(string.Format("\n{0}\n{1}\n{2}\n{3}\n", e.GetType(), e.InnerException?.GetType(), e.Message, e.StackTrace), e); });
            }
            return student;
        }

        public Students Save(Students student)
        {
            try
            {
                _context.Entry(student).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Log4net.Error(string.Format("\n{0}\n{1}\n{2}\n{3}\n", e.GetType(), e.InnerException?.GetType(), e.Message, e.StackTrace), e);
            }
            return student;
        }

        public async Task<Students> SaveAsync(Students student)
        {
            try
            {
                _context.Entry(student).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                await Task.Run(() => { Log4net.Error(string.Format("\n{0}\n{1}\n{2}\n{3}\n", e.GetType(), e.InnerException?.GetType(), e.Message, e.StackTrace), e); });
            }
            return student;
        }

        public Students Delete(Students student)
        {
            try
            {
                _context.Entry(student).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Log4net.Error(string.Format("\n{0}\n{1}\n{2}\n{3}\n", e.GetType(), e.InnerException?.GetType(), e.Message, e.StackTrace), e);
            }
            return student;
        }

        public async Task<Students> DeleteAsync(Students student)
        {
            try
            {
                _context.Entry(student).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                await Task.Run(() => { Log4net.Error(string.Format("\n{0}\n{1}\n{2}\n{3}\n", e.GetType(), e.InnerException?.GetType(), e.Message, e.StackTrace), e); });
            }
            return student;
        }
    }
}
