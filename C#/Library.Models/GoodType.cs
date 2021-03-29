using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Model
{
    [Table("Types")]
    public class GoodType : IIdentifable
    {
        public virtual int ID { get; protected set; }

        [Required]
        public virtual string Name { get; protected set; } = null!;

        [Browsable(false)]
        public virtual IReadOnlyCollection<Good> Goods { get; protected set; } = null!;

        public GoodType(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public GoodType() { }
    }
}
