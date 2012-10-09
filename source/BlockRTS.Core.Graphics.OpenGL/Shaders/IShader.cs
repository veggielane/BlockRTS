using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace BlockRTS.Core.Graphics.OpenGL.Shaders
{
    public interface IShader
    {
        int Handle { get; }
        ShaderType type { get; }
        string Source { get; }
        void Compile();

    }

    public abstract class VertexShader : Shader
    {
        public VertexShader()
            : base(ShaderType.VertexShader)
        {

        }
    }

    public abstract class FragmentShader : Shader
    {

        public FragmentShader()
            : base(ShaderType.FragmentShader)
        {

        }
    }

    public abstract class Shader:IShader
    {
        public abstract string Source { get; }
        public int Handle { get; private set; }
        public ShaderType type { get; private set; }

        public Shader(ShaderType type)
        {
            Handle = GL.CreateShader(type);

        }

        public void Compile()
        {
            GL.ShaderSource(Handle, Source);
            GL.CompileShader(Handle);
            Console.WriteLine(GL.GetShaderInfoLog(Handle));
            int compileResult;
            GL.GetShader(Handle, ShaderParameter.CompileStatus, out compileResult);
            if (compileResult != 1)
            {
                Console.WriteLine("Compile Error:" + type);
            }
        }
    }
    public class TestShaderProgram : BaseShaderProgram
    {
        public TestShaderProgram()
        {
            Add(new TestVertexShader());
            Add(new TestFragmentShader());
        }
    }

    public class TestVertexShader : VertexShader
    {
        public override string Source
        {
            get
            {
                return @"#version 150 
precision highp float; 
 
layout (location = 0) in vec3 vert_position; 
layout (location = 1) in vec3 vert_normal; 
layout (location = 2) in vec4 vert_colour; 
layout (location = 3) in vec2 vert_texture;


uniform mat4 mvp;
uniform mat4 position; 

varying vec3 normal; 
varying vec4 colour; 
varying vec2 texcoord;

void main(void) 
{ 
  normal = (mvp * vec4(vert_normal, 0)).xyz; //works only for orthogonal modelview 
  colour = vert_colour; 
  texcoord = vert_texture;
  gl_Position = mvp * position * vec4(vert_position, 1); 
}";
            }
        }
    }

    public class TestFragmentShader : FragmentShader
    {
        public override string Source
        {
            get
            {
                return @"#version 150 
precision highp float;

const vec3 ambient = vec3(0.8, 0.8, 0.8);
const vec3 lightVecNormalized = normalize(vec3(0.5, 1, 2.0));
const vec3 lightColor = vec3(0.9, 0.9, 0.9);

varying vec3 normal;
varying vec4 colour;
varying vec2 texcoord;

uniform sampler2D tex;

out vec4 out_frag_color;
void main(void)
{
  if(colour == vec4(0.0,0.0,0.0,0.0)){
      out_frag_color = texture2D(tex, texcoord);
  }else{
      out_frag_color = colour * texture2D(tex, texcoord);
  }

	//if (out_frag_color.a < 0.4)// alpha value less than user-specified threshold?
	//{
	//	discard; // yes: discard this fragment
	//}
}";
            }
        }
    }
}
