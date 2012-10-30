using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace BlockRTS.Core.Graphics.OpenGL.Shaders
{

    public class DefaultShaderProgram:BaseShaderProgram
    {
        public DefaultShaderProgram()
        {
            CompileShader(ShaderType.VertexShader, @"#version 400
precision highp float; 
 
layout (location = 0) in vec3 vert_position; 
layout (location = 1) in vec3 vert_normal; 
layout (location = 2) in vec4 vert_colour; 
layout (location = 3) in vec2 vert_texture;

uniform mat4 mvp;
uniform mat4 position; 

out vec3 normal; 
out vec4 colour; 
out vec2 texcoord;
void main(void) 
{ 
  normal = (mvp * vec4(vert_normal, 0)).xyz; //works only for orthogonal modelview 
  colour = vert_colour; 
  texcoord = vert_texture;
  gl_Position = mvp * position * vec4(vert_position, 1); 
}");

            CompileShader(ShaderType.FragmentShader, @"#version 400
precision highp float;

const vec3 ambient = vec3(0.5, 0.5, 0.5);
const vec3 lightVecNormalized = normalize(vec3(0.5, 1, 2.0));
const vec3 lightColor = vec3(0.9, 0.9, 0.9);

in vec3 normal;
in vec4 colour;
in vec2 texcoord;

uniform sampler2D tex;

layout( location = 0 ) out vec4 FragColor;

void main(void)
{
  float diffuse = clamp(dot(lightVecNormalized, normalize(normal)), 0.0, 1.0);
  FragColor = colour * vec4(ambient + diffuse * lightColor, 1.0);
  //out_frag_color = texture2D(tex, texcoord) * vec4(ambient + diffuse * lightColor, 1.0);


  if(colour == vec4(0.0,0.0,0.0,0.0)){
     // FragColor = texture2D(tex, texcoord);
  }else{
     // FragColor = colour * texture2D(tex, texcoord);
  }
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