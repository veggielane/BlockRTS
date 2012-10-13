using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
