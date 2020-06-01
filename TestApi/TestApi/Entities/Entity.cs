using System;

namespace TestApi.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        public override bool Equals(object obj)
        {
            var entity = obj as Entity;
            if (!(entity is null))
            {
                return Id == entity.Id;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity e1, Entity e2)
        {
            return e1 != null && e1.Equals(e2);
        }

        public static bool operator !=(Entity e1, Entity e2)
        {
            return !(e1 == e2);
        }
    }
}