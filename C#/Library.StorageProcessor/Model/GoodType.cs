using Library.StorageProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.StorageProcessor.Model
{
    public class GoodType : IIdentifable
    {
        public int ID { get; }

        public string Name { get; } = null!;

        public List<Good> Goods { get; } = null!;

        public GoodType(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
        public GoodType()
        {
        }
    }
}
