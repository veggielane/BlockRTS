using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace BlockRTS.Core.Graphics.OpenGL.Shaders
{

    public class BlockShaderProgram : BaseShaderProgram
    {
        public BlockShaderProgram()
        {
            CompileShader(ShaderType.VertexShader, @"#version 400
precision highp float;

layout (location = 0) in vec3 position;
layout (location = 1) in vec3 instance_position;
layout (location = 2) in vec4 instance_rotation;
uniform mat4 mvp;

vec3 qrot(vec4 q, vec3 v)       {
        return v + 2.0*cross(q.xyz, cross(q.xyz,v) + q.w*v);
}

void main(void)
{
    gl_Position = mvp * vec4(instance_position + qrot(instance_rotation,position) , 1.0);
}
");

            CompileShader(ShaderType.FragmentShader, @"#version 400
precision highp float;
layout( location = 0 ) out vec4 FragColor;
void main(void)
{
    FragColor = vec4(0.37,0.66,0.78,0.9);
}");

            GL.BindAttribLocation(Handle, 0, "position");
            GL.BindAttribLocation(Handle, 1, "instance_position");
            GL.BindAttribLocation(Handle, 2, "instance_rotation");
            Link();
            Uniforms.Add("mvp", new UniformMatrix4("mvp", this));
            Uniforms.Add("position", new UniformMatrix4("position", this));

        }
    }
}
