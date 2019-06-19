using System;

namespace VirtualCashCard.Domain
{
    public class Money : IEquatable<Money>
    {
        public decimal Value { get; set; }

        public bool Equals(Money other)
        {
            if (other is null) return false;
            if (ReferenceEquals(other, this)) return true;
            return this.Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
