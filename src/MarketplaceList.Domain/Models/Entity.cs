using System;

namespace MarketplaceList.Domain.Models
{
   public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
            CreateAt = DateTime.Now;
        }
        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; }
    }
}