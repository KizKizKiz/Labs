using System;

namespace Library.Graph.ConvertibleTypes
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

        public bool Equals(StringConvertible other)
            => Entity.Equals(other.Entity);
        

        public int CompareTo(StringConvertible other)
            => Entity.CompareTo(other.Entity);
        
        public override string ToString() => Entity;

        public static implicit operator StringConvertible(string value)
            => new StringConvertible(value);
    }
}