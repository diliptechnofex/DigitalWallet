using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.BuildingBlocks.Domain.Primitives
{
    public abstract class Entity<TId> where TId : notnull
    {
        protected Entity()
        {
        }

        protected Entity(TId id)
        {
            Id = id;
        }

        public TId Id { get; protected set; } = default!;

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is not Entity<TId> other)
            {
                return false;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

            if (IsTransient() || other.IsTransient())
            {
                return false;
            }

            return EqualityComparer<TId>.Default.Equals(
                Id,
                other.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GetType(), Id);
        }

        public static bool operator ==(
            Entity<TId>? left,
            Entity<TId>? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(
            Entity<TId>? left,
            Entity<TId>? right)
        {
            return !Equals(left, right);
        }

        private bool IsTransient()
        {
            return EqualityComparer<TId>.Default.Equals(
                Id,
                default);
        }
    }
}
