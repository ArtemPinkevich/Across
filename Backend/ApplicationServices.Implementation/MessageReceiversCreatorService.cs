using DataAccess.Interfaces;
using Entities;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ApplicationServices.Implementation
{
    public class MessageReceiversCreatorService : IMessageReceiversCreatorService
    {
        private readonly IRepository<CarWash> _carWashesRepository;
        private readonly IRepository<Record> _recordsRepository;
        private readonly IRepository<WashService> _washServicesRepository;
        private readonly IRepository<PriceGroup> _priceGroupRepository;
        private readonly UserManager<User> _userManager;

        public MessageReceiversCreatorService(IRepository<CarWash> carWashesRepository,
            IRepository<Record> recordsRepository,
            IRepository<WashService> washServicesRepository,
            IRepository<PriceGroup> priceGroupRepository,
            UserManager<User> userManager)
        {
            _carWashesRepository = carWashesRepository;
            _recordsRepository = recordsRepository;
            _washServicesRepository = washServicesRepository;
            _priceGroupRepository = priceGroupRepository;
            _userManager = userManager;
        }

        public async Task<List<string>> GetAdminsByCarWash(int carWashId)
        {
            CarWash carWash = await _carWashesRepository.GetAsync(item => item.Id == carWashId);
            if (carWash == null)
            {
                return new List<string>();
            }

            return carWash.Users.Select(item => item.Id).ToList();
        }

        public async Task<List<string>> GetAdminsByRecordId(int recordId)
        {
            Record record = await _recordsRepository.GetAsync(item => item.Id == recordId);
            if (record == null)
            {
                return new List<string>();
            }

            return await GetAdminsByCarWash(record.CarWashId);
        }

        public async Task<List<string>> GetAdminsByWashService(int washServiceId)
        {
            var washService = await _washServicesRepository.GetAsync(item => item.Id == washServiceId);
            if (washService == null)
            {
                return new List<string>();
            }

            return await GetAdminsByCarWash(washService.CarWashId);
        }

        public async Task<List<string>> GetAdminsByPriceGroup(int priceGroupId)
        {
            var washService = await _priceGroupRepository.GetAsync(item => item.Id == priceGroupId);
            if (washService == null)
            {
                return new List<string>();
            }

            return await GetAdminsByCarWash(washService.CarWashId);
        }

        public List<string> GetMobilesByPhone(string phone)
        {
            return _userManager.Users.Where(o => o.PhoneNumber == phone).Select(u => u.Id).ToList();
        }

        public async Task<List<string>> GetMobilesByRecordId(int recordId)
        {
            Record record = await _recordsRepository.GetAsync(item => item.Id == recordId);
            if (record == null)
            {
                return new List<string>();
            }

            return GetMobilesByPhone(record.PhoneNumber);
        }

        public List<string> GetMobilesByFavoriteCarwashId(int carWashId)
        {
            var userIds = _userManager.Users.Where(o => o.FavouriteCarWashes.Select(c => c.Id).Contains(carWashId)).Select(u => u.Id).ToList();
            return userIds;
        }
    }
}
