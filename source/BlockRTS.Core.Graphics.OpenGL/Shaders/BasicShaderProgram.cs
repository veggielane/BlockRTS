using BlockRTS.Core.Graphics.OpenGL.Assets;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace BlockRTS.Core.Graphics.OpenGL.Shaders
{

    public class BasicShaderProgram:BaseShaderProgram
    {
        private readonly IAssetManager _assetManager;

        public BasicShaderProgram(IAssetManager assetManager)
        {
            _assetManager = assetManager;
            CompileShader(ShaderType.VertexShader, @"#version 400
precision highp float; 

layout(std140) uniform Camera {
    mat4 MVP;
    mat4 Model;
    mat4 View;
    mat4 Projection;
    mat4 NormalMatrix;
};

layout (location = 0) in vec3 vert_position; 
layout (location = 1) in vec3 vert_normal; 
layout (location = 2) in vec4 vert_colour; 
layout (location = 3) in vec2 vert_texture;

void main(void) 
{ 
    gl_Position = (MVP) * vec4(vert_position, 1); 
}");
            CompileShader(ShaderType.FragmentShader, @"#version 400
layout( location = 0 ) out vec4 FragColor;
void main() {
    FragColor = vec4(1,0,1,1);
}");
            Link();
            _assetManager.UBO<CameraUBO>().BindToShaderProgram(this);
        }
    }
}