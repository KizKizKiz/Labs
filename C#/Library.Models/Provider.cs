using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Model
{
    [Table("Providers")]
    public class Provider : IIdentifable
    {
        public virtual int ID { get; protected set; }

        [Required]
        public virtual string Name { get; protected set; } = null!;

        [Required]
        [DisplayName("Telephone number")]
        public virtual string TelNumber { get; protected set; } = null!;

        [DefaultValue("Not presented")]
        public virtual string? Address { get; protected set; }

        [Browsable(false)]
        public virtual IReadOnlyCollection<GoodProvider> GoodsProviders { get; protected set; } = null!;

        public Provider(
            string name,
            string telNumber,
            string address)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            TelNumber = telNumber ?? throw new ArgumentNullException(nameof(telNumber));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public Provider() { }
    }
}
