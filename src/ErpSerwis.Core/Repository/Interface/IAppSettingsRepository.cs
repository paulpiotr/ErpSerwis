using System.Threading.Tasks;
using ErpSerwis.Core.Models;

namespace ErpSerwis.Core.Repository.Interface
{
    public interface IAppSettingsRepository
    {
        bool Save(AppSettings appSettings);

        Task<bool> SaveAsync(AppSettings appSettings);
    }
}
