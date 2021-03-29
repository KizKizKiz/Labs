using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Model
{
    [Table("[Goods]")]
    public class Good : IIdentifable
    {
        public virtual int ID { get; protected set; }

        [Required]
        [MinLength(1)]
        public virtual string Description { get; set; } = null!;

        [Required]
        [MinLength(1)]
        public virtual string Manufacturer { get; set; } = null!;

        [Required]
        [MinLength(1)]
        public virtual string Model { get; set; } = null!;

        [Required]
        [DisplayName("Price without discont")]
        public virtual decimal OriginalPrice { get; set; }

        [DisplayName("Discont percent")]
        [DefaultValue("Not set.")]
        public virtual short DiscontPercent { get; set; }

        [DisplayName("ID of good's type")]
        public virtual int TypeId { get; set; }

        [Required]
        [Browsable(false)]
        public virtual GoodType GoodType { get; protected set; } = null!;

        [Browsable(false)]
        public virtual IReadOnlyCollection<GoodProvider> GoodsProviders { get; protected set; } = null!;

        public Good(
            string description, 
            string manufacturer, 
            string model, 
            decimal originalPrice, 
            GoodType goodType,
            short discontPercent)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Manufacturer = manufacturer ?? throw new ArgumentNullException(nameof(manufacturer));
            Model = model ?? throw new ArgumentNullException(nameof(model));
            OriginalPrice = originalPrice <= 0 ? 
                throw new ArgumentException("The price cannot be less than zero or equals.", nameof(originalPrice)) 
                : originalPrice;
            GoodType = goodType ?? throw new ArgumentNullException(nameof(goodType));
            DiscontPercent = discontPercent < 0 ?
                throw new ArgumentException("The discont cannot be less than zero or equals.", nameof(discontPercent))
                : discontPercent;
        }

        public Good() { }
    }
}
