using System;

namespace BlockRTS.Core.Graphics.OpenGL
{
    public class ShaderProgramException  : Exception
    {
        public ShaderProgramException(string message)
            : base(message)
        {
        }
    }
}
