using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.StorageProcessor.Model
{
    public class GoodsProviders : IIdentifable
    {
        public int ID { get; }

        public int Quantity { get; }

        public DateTime DeliveryDate { get; }

        [ForeignKey("ProviderId")]
        public Provider Provider { get; set; } = null!;

        [ForeignKey("GoodId")]
        public Good Good { get; set; } = null!;
    }
}
