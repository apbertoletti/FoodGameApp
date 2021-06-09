using System;

namespace FG.Domain.Entities.Base
{
    public abstract class Entity<T> where T : Entity<T>
    {
        public Guid Id { get; private set; }

        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
