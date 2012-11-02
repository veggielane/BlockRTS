using BlockRTS.Core.Graphics.OpenGL.Assets;
using OpenTK.Graphics.OpenGL;

namespace BlockRTS.Core.Graphics.OpenGL.Shaders
{

    public class BlockShaderProgram : BaseShaderProgram
    {
        private readonly IAssetManager _assetManager;

        public BlockShaderProgram(IAssetManager assetManager)
        {
            _assetManager = assetManager;
            CompileShader(ShaderType.VertexShader, @"#version 400
precision highp float;

layout (location = 0) in vec3 position;
layout (location = 1) in vec3 instance_position;
layout (location = 2) in vec4 instance_rotation;
layout (location = 3) in vec4 instance_color;

layout(std140) uniform Camera {
    mat4 MVP;
    mat4 Model;
    mat4 View;
    mat4 Projection;
    mat4 NormalMatrix;
};

vec3 qrot(vec4 q, vec3 v)       {
        return v + 2.0*cross(q.xyz, cross(q.xyz,v) + q.w*v);
}

out vec3 normal; 
out vec4 color;
void main(void)
{
  color = instance_color;
  //normal = normalize(mat3(NormalMatrix) * vert_normal);
  gl_Position = MVP * vec4(instance_position + qrot(instance_rotation,position) , 1.0);
}
");

            CompileShader(ShaderType.FragmentShader, @"#version 400
precision highp float;
layout( location = 0 ) out vec4 FragColor;
in vec4 color;
void main(void)
{
    FragColor = color;
    //FragColor = vec4(0.37,0.66,0.78,0.9);
}");

            GL.BindAttribLocation(Handle, 0, "position");
            GL.BindAttribLocation(Handle, 1, "instance_position");
            GL.BindAttribLocation(Handle, 2, "instance_rotation");
            GL.BindAttribLocation(Handle, 2, "instance_color");
            Link();
            _assetManager.UBO<CameraUBO>().BindToShaderProgram(this);
        }
    }
}
