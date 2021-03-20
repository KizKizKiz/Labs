using System;
using System.Collections.Generic;

namespace Library.StorageProcessor.Model
{
    public class Provider : IIdentifable
    {
        public int ID { get; }

        public string Name { get; } = null!;

        public string TelNumber { get; set; } = null!;

        public string Address { get; set; } = null!;

        public List<GoodsProviders> GoodsProviders { get; } = null!;

        public Provider(
            string name,
            string telNumber,
            string address)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            TelNumber = telNumber ?? throw new ArgumentNullException(nameof(telNumber));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public Provider()
        {
        }
    }
}
