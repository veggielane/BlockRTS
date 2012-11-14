using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace BlockRTS.Core.Graphics.OpenGL.Shaders
{


    public class FlatShaderProgram : BaseShaderProgram
    {
        public FlatShaderProgram()
        {
            CompileShader(ShaderType.VertexShader, @"#version 400
precision highp float; 
layout (location = 0) in vec3 vert_position; 
layout (location = 1) in vec3 vert_normal; 
layout (location = 2) in vec4 vert_colour; 
layout (location = 3) in vec2 vert_texture;

out vec2 vertTexcoord;

void main(void) 
{ 
    vertTexcoord = vert_texture;
    gl_Position = vec4(vert_position, 1); 
}
");

            CompileShader(ShaderType.FragmentShader, @"#version 400
uniform sampler2D texture;
in vec2 vertTexcoord;
layout( location = 0 ) out vec4 FragColor;
void main() {
    FragColor = texture2D(texture, vertTexcoord);
}
");
            Link();
        }
    }
}
