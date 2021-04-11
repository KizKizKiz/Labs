using System;

using Library.Graph.ImportersExporters;

namespace Library.Graph.ExampleConvertibleTypes
{
    public struct StringConvertible : IStringConvertible<StringConvertible>, IEquatable<StringConvertible>, IComparable<StringConvertible>
    {
        public string Entity { get; private set; }

        public StringConvertible(string entity)
        {
            Entity = entity;
        }
        public StringConvertible ConvertFromString(string entity)
        {
            Entity = entity;
            return this;
        }

        public bool Equals(StringConvertible other) => Entity.Equals(other.Entity, StringComparison.Ordinal);

        public int CompareTo(StringConvertible other) => string.Compare(Entity, other.Entity, StringComparison.Ordinal);

        public override string ToString() => Entity;

        public static implicit operator StringConvertible(string value)
            => new(value);
    }
}