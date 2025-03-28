﻿namespace DocumentsCreatorOwin.DAL.Entities
{

    public class Cargo
    {
        public int Id { set; get; }

        public string CreatedId { set; get; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { set; get; }

        public int Weight { set; get; }

        /// <summary>
        /// Объем, м3
        /// </summary>
        public int Volume { set; get; }

        /// <summary>
        /// Тип упаковки
        /// </summary>
        public PackagingType PackagingType { set; get; }

        /// <summary>
        /// // Количество упаковки. НЕ для всех packagingType
        /// </summary>
        public int? PackagingQuantity { set; get; }

        /// <summary>
        /// Габариты груза (Д/Ш/В), м; Не должны превышать 50м, не должно быть более 2 знаков после запятой
        /// </summary>
        public double? Length { set; get; }

        public double? Width { set; get; }
        public double? Height { set; get; }

        public double Diameter { set; get; }

        /// <summary>
        /// стоимость груза
        /// </summary>
        public int Price { set; get; }

        public int TransportationOrderId { set; get; }
        public TransportationOrder TransportationOrder { set; get; }
    }
}