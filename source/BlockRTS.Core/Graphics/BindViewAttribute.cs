using System;

namespace BlockRTS.Core.Graphics
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class BindViewAttribute : Attribute
    {
        private readonly Type _keyDataType;

        public BindViewAttribute(Type type)
        {
            _keyDataType = type;
        }

        public Type GameObjectType
        {
            get { return _keyDataType; }
        }
    }
}
