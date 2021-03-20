using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.StorageProcessor.Model
{
    public class Good : IIdentifable
    {
        public int ID { get; }

        public string Description { get; } = null!;

        public string Manufacturer { get; } = null!;

        public string Model { get; } = null!;

        public double OriginalPrice { get; }

        public int? DiscontPercent { get; } = null!;

        [ForeignKey("TypeId")]
        public GoodType GoodType { get; } = null!;

        public List<GoodsProviders> GoodsProviders { get; } = null!; 

        public Good(
            string description, 
            string manufacturer, 
            string model, 
            double originalPrice, 
            GoodType goodType)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Manufacturer = manufacturer ?? throw new ArgumentNullException(nameof(manufacturer));
            Model = model ?? throw new ArgumentNullException(nameof(model));
            OriginalPrice = originalPrice <= 0 ? 
                throw new ArgumentException("The price cannot be less than zero or equals.", nameof(originalPrice)) 
                : originalPrice;
            GoodType = goodType ?? throw new ArgumentNullException(nameof(goodType));
        }

        public Good(
            string description,
            string manufacturer,
            string model,
            double originalPrice,
            GoodType goodType,
            int discontPercent)
            :this(description, manufacturer, model, originalPrice, goodType)
        {
            DiscontPercent = discontPercent <= 0 ?
                throw new ArgumentException("The discont cannot be less than zero or equals.", nameof(discontPercent))
                : discontPercent;
        }

        public Good()
        {
        }
    }
}
