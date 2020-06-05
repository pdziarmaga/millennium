using System;

namespace Millennium.Shared.BaseObjects
{
    public abstract class Entity : IEntity
    {
        protected Entity()
        {
            Id = new Random().Next(1, int.MaxValue);
        }

        public long Id { get; protected internal set; }
    }
}