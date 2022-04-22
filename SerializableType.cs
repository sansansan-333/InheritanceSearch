using System;

namespace InheritanceSearch
{
    public class SerializableType
    {
        public string typeName;
        public Type type;

        public SerializableType(Type type)
        {
            this.type = type;
            typeName = type.ToString();
        }

        public override int GetHashCode()
        {
            if (typeName == null) return 0;
            return typeName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is SerializableType other)
                return other != null && other.typeName == this.typeName;
            else
                return false;
        }

        public override string ToString()
        {
            return this;
        }  
        public static implicit operator string(SerializableType serializableType) => serializableType.typeName;
    }
}