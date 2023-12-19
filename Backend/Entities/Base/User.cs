namespace Entities
{
    using Entities.Enums;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User: IdentityUser
    {
        public string Name { set; get; }

        public string Surname { set; get; }

        public string Patronymic { set; get; }

        public string BirthDate { set; get; }

        public Gender Gender { set; get; }

        public List<Vehicle> Vehicles { set; get; }

        /// <summary>
        /// у владельца может быть несколько автомоек
        /// админ может быть связан только с одной автомойкой
        /// у пользователя нет автомоек
        /// </summary>
        public List<CarWash> CarWashes { set; get; }

        /// <summary>
        /// Для мобильных пользователей
        /// Список автомоек которые добавлены в избранные у мобильного пользователя
        /// </summary>
        public List<CarWash> FavouriteCarWashes { set; get; }
    }
}
