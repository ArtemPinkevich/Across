namespace Entities
{
    using Enums;
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User: IdentityUser
    {
        public string Name { set; get; }

        public string Surname { set; get; }

        public string Patronymic { set; get; }

        public string BirthDate { set; get; }

        public Gender Gender { set; get; }
        
        public string ReservePhoneNumber { set; get; }
        
        public UserStatus UserStatus { set; get; }
        
        public List<Document> Documents { set; get; }
        
        /*
        
        /// <summary>
        /// List of Trucks of Driver
        /// </summary>
        public List<Truck> Trucks { set; get; }
        
        /// <summary>
        /// List of TransportationOrders of Shipper
        /// </summary>
        public List<TransportationOrder> TransportationOrders { set; get; }
        
        */
    }
}
