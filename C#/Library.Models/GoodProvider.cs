using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Model
{
    [Table("GoodsProviders")]
    public class GoodProvider : IIdentifable
    {
        public virtual int ID { get; protected set; }

        public virtual int Quantity { get; set; }

        [DisplayName("Delivery date")]
        public virtual DateTime DeliveryDate { get; set; }

        [DisplayName("ID of provider")]
        public virtual int ProviderId { get; set; }

        [DisplayName("ID of good")]
        public virtual int GoodId { get; set; }

        [ForeignKey("ProviderId"), Required, Browsable(false)]
        public virtual Provider Provider { get; set; } = null!;

        [ForeignKey("GoodId"), Required, Browsable(false)]
        public virtual Good Good { get; set; } = null!;
    }
}