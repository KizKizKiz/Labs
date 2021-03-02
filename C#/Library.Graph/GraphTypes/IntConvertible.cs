using System;

namespace Library.GraphTypes
{
    public readonly struct IntConvertible : IStringConvertible<IntConvertible>, IEquatable<IntConvertible>, IComparable<IntConvertible>
    {
        public int Number { get; }
        public IntConvertible(int number)
        {
            Number = number;
        }
        public IntConvertible ConvertFromString(string entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            if (string.IsNullOrWhiteSpace(entity))
            {
                throw new ArgumentException("Received empty string or string that contains only whitespaces.", nameof(entity));
            }
            return new IntConvertible(int.Parse(entity));
        }

        public bool Equals(IntConvertible other)
        {
            return Number.Equals(other.Number);
        }

        public int CompareTo(IntConvertible other)
        {
            return Number.CompareTo(other.Number);
        }
        public override string ToString()
        {
            return Number.ToString();
        }
    }
}