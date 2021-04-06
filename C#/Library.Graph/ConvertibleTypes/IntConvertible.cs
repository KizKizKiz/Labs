using System;

namespace Library.Graph.ConvertibleTypes
{
    public readonly struct IntConvertible : IStringConvertible<IntConvertible>, IEquatable<IntConvertible>, IComparable<IntConvertible>
    {
        public int Number { get; }

        public bool IsNotDefaultValue { get; }

        public IntConvertible(int number)
        {
            Number = number;
            IsNotDefaultValue = true;
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
            => Number.Equals(other.Number);

        public int CompareTo(IntConvertible other)
            => Number.CompareTo(other.Number);

        public override string ToString()
            => Number.ToString();

        public static implicit operator IntConvertible(int number)
            => new IntConvertible(number);
    }
}