using System;
using System.Collections.Generic;
using MediatR;

namespace MerchandiseService.Domain.Base.Models
{
    public abstract class Entity<TKey>
    {
        private int? RequestedHashCode { get; set; }

        private TKey _id;

        public TKey Id
        {
            get => _id;
            set
            {
                if (!IsTransient)
                    throw new InvalidOperationException($"{nameof(Entity<TKey>)} property {nameof(Id)} already set"); 
                _id = value;
            }
        }

        public bool IsTransient => ReferenceEquals(_id, default(TKey)) || _id.Equals(default(TKey));

        private List<INotification> _domainEvents;

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public override bool Equals(object obj)
        {
            if (obj is not Entity<TKey> entity) return false;

            if (ReferenceEquals(this, entity)) return true;

            if (GetType() != entity.GetType()) return false;

            if (IsTransient || entity.IsTransient) return false;
            
            return Id.Equals(entity.Id);
        }

        public override int GetHashCode()
        {
            if (IsTransient) return base.GetHashCode();
            
            RequestedHashCode ??= Id.GetHashCode();
            return RequestedHashCode.Value;

        }
        public static bool operator ==(Entity<TKey> left, Entity<TKey> right) =>
            (left is null == right is null) && (left is null || left.Equals(right));

        public static bool operator !=(Entity<TKey> left, Entity<TKey> right) => !(left == right);
    }
}