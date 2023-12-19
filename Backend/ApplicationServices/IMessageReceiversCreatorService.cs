using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationServices
{
    public interface IMessageReceiversCreatorService
    {
        Task<List<string>> GetAdminsByCarWash(int carWashId);

        Task<List<string>> GetAdminsByRecordId(int recordId);

        Task<List<string>> GetAdminsByWashService(int washServiceId);

        Task<List<string>> GetAdminsByPriceGroup(int priceGroupId);

        List<string> GetMobilesByPhone(string phone);

        Task<List<string>> GetMobilesByRecordId(int recordId);

        List<string> GetMobilesByFavoriteCarwashId(int carWashId);
    }
}
