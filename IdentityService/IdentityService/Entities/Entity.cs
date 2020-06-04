using System;

namespace TestApi.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        public override bool Equals(object obj)
        {
            var temp = obj as Entity;
            return !ReferenceEquals(temp, null) && temp.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator == (Entity e1, Entity e2)
        {
            if (ReferenceEquals(e1, e2))
            {
                return true;
            }

            if (ReferenceEquals(e1, null))
            {
                return false;
            }
            if (ReferenceEquals(e2, null))
            {
                return false;
            }
            return e1.Equals(e2);
        }

        public static bool operator !=(Entity e1, Entity e2)
        {
            return !(e1 == e2);
        }
    }
}