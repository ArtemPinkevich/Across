namespace Entities
{
    using System.Collections.Generic;

    public class CarWash: EntityBase
    {
        public string Phone { set; get; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// Долгота
        /// </summary>
        public string Longitude { set; get; }

        /// <summary>
        /// Широта
        /// </summary>
        public string Latitude { set; get; }

        /// <summary>
        /// адрес автомойки
        /// </summary>
        public string Location { set; get; }

        public string StartWorkTime { set; get; }

        public string EndWorkTime { set; get; }
        public int BoxesQuantity { set; get; }

        /// <summary>
        ///список пользователей, которые имеют отношение к этой автомойке (админы и владельцы) 
        /// </summary>
        public List<User> Users { set; get; }

        /// <summary>
        /// Список пользователей, которыми автомойка была добавлена в избранное
        /// </summary>
        public List<User> SelectedByUsers { set; get; }

        /// <summary>
        /// Время в минутах, которое нужно оставлять между записями
        /// </summary>
        public int ReservedMinutesBetweenRecords { set; get; }
    }
}
