using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities;

namespace DataAccess.BaseImplementation;

public static class DbCarBodiesSeed
{
    public static async Task InitDbCarBodies(IRepository<CarBody> carBodiesRepository)
    {
        var bodies = new List<CarBody>();
        if (await carBodiesRepository.GetAsync(x => x.CarBodyName == "Седан") == null)
            bodies.Add(new CarBody()
            {
                CarBodyName = "Седан"
            });
        
        if (await carBodiesRepository.GetAsync(x => x.CarBodyName == "Хэтчбек") == null)
            bodies.Add(new CarBody()
            {
                CarBodyName = "Хэтчбек"
            });
        
        if (await carBodiesRepository.GetAsync(x => x.CarBodyName == "Универсал") == null)
            bodies.Add(new CarBody()
            {
                CarBodyName = "Универсал"
            });
        
        if (await carBodiesRepository.GetAsync(x => x.CarBodyName == "Кабиролет") == null)
            bodies.Add(new CarBody()
            {
                CarBodyName = "Кабиролет"
            });
        
        if (await carBodiesRepository.GetAsync(x => x.CarBodyName == "Внедорожник") == null)
            bodies.Add(new CarBody()
            {
                CarBodyName = "Внедорожник"
            });
        
        if (await carBodiesRepository.GetAsync(x => x.CarBodyName == "Кроссовер") == null)
            bodies.Add(new CarBody()
            {
                CarBodyName = "Кроссовер"
            });
        if (await carBodiesRepository.GetAsync(x => x.CarBodyName == "Пикап") == null)
            bodies.Add(new CarBody()
            {
                CarBodyName = "Пикап"
            });
        
        if (await carBodiesRepository.GetAsync(x => x.CarBodyName == "Фургон") == null)
            bodies.Add(new CarBody()
            {
                CarBodyName = "Фургон"
            });
        
        if (await carBodiesRepository.GetAsync(x => x.CarBodyName == "Лимузин") == null)
            bodies.Add(new CarBody()
            {
                CarBodyName = "Лимузин"
            });
        
        if (await carBodiesRepository.GetAsync(x => x.CarBodyName == "Минивэн") == null)
            bodies.Add(new CarBody()
            {
                CarBodyName = "Минивэн"
            });
        
        if (await carBodiesRepository.GetAsync(x => x.CarBodyName == "Автобус") == null)
            bodies.Add(new CarBody()
            {
                CarBodyName = "Автобус"
            });
        
        await carBodiesRepository.AddAsync(bodies);
        await carBodiesRepository.SaveAsync();
    }
}