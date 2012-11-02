using BlockRTS.Core.Graphics.OpenGL.Assets;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace BlockRTS.Core.Graphics.OpenGL.Shaders
{

    public class DefaultShaderProgram:BaseShaderProgram
    {
        private readonly IAssetManager _assetManager;

        public DefaultShaderProgram(IAssetManager assetManager)
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

uniform mat4 position; 


out vec3 normal; 
out vec4 colour; 
out vec3 LightIntensity;

struct LightInfo {
  vec4 Position; // Light position in eye coords.
  vec3 La;       // Ambient light intensity
  vec3 Ld;       // Diffuse light intensity
  vec3 Ls;       // Specular light intensity
};

struct MaterialInfo {
  vec3 Ka;            // Ambient reflectivity
  vec3 Kd;            // Diffuse reflectivity
  vec3 Ks;            // Specular reflectivity
  float Shininess;    // Specular shininess factor
};

LightInfo Light = LightInfo(vec4(0.0,50.0,0.0,1.0),vec3(0.4, 0.4, 0.4),vec3(1.0, 1.0, 1.0),vec3(1.0, 1.0, 1.0));
MaterialInfo Material = MaterialInfo(vec3(0.9, 0.5, 0.3),vec3(0.9, 0.5, 0.3),vec3(0.8, 0.8, 0.8),100.0);

void main(void) 
{ 
    vec3 tnorm = normalize(mat3(NormalMatrix) * vert_normal);
    vec4 eyeCoords = (View * Model) * vec4(vert_position,1.0);
    vec3 s = normalize(vec3((Light.Position * View) - eyeCoords));
    vec3 v = normalize(-eyeCoords.xyz);
    vec3 r = reflect( -s, tnorm );
    float sDotN = max( dot(s,tnorm), 0.0 );
    vec3 ambient = Light.La * Material.Ka;
    vec3 diffuse = Light.Ld * Material.Kd * sDotN;
    vec3 spec = vec3(0.0);
    if( sDotN > 0.0 )
       spec = Light.Ls * Material.Ks *
              pow( max( dot(r,v), 0.0 ), Material.Shininess );

    LightIntensity = ambient + diffuse*vert_colour + spec;
    gl_Position = (MVP) * position * vec4(vert_position, 1); 
}");

            CompileShader(ShaderType.FragmentShader, @"#version 400

in vec3 LightIntensity;
layout( location = 0 ) out vec4 FragColor;
void main() {
    FragColor = vec4(LightIntensity, 1.0);
}");
            Link();
            Uniforms.Add("position", new UniformMatrix4("position", this));
            _assetManager.UBO<CameraUBO>().BindToShaderProgram(this);

        }
    }
}