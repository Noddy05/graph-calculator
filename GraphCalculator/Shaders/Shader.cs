using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCalculator.Shaders
{
    internal class Shader : IDisposable
    {
        private int handle; //Handle is a unique reference to this shader.
        public int GetHandle() => handle; //Returns the shader handle.

        private Window window;

        //Takes in file references to frag and vertex shaders.
        public Shader(string vertexShaderLocation, string fragmentShaderLocation) { 
            window = Program.GetWindow();
            window.Unload += Dispose; //Automatically disposes shader whenever the window closes.

            GenerateShader(vertexShaderLocation, fragmentShaderLocation);
        }

        private void GenerateShader(string vertexShaderLocation, string fragmentShaderLocation)
        {
            handle = GL.CreateProgram();

            string vertexShaderSource = File.ReadAllText(vertexShaderLocation);
            string fragmentShaderSource = File.ReadAllText(fragmentShaderLocation);

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexShaderSource);
            GL.CompileShader(vertexShader);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentShaderSource);
            GL.CompileShader(vertexShader);

            handle = GL.CreateProgram();
            GL.AttachShader(handle, vertexShader);
            GL.AttachShader(handle, fragmentShader);
            GL.LinkProgram(handle);

            GL.DetachShader(handle, vertexShader);
            GL.DetachShader(handle, fragmentShader);

            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            string outputLog = GL.GetProgramInfoLog(handle);

            if(string.IsNullOrEmpty(outputLog))
            {
                return;
            }

            throw new Exception(outputLog);
        }

        public void Dispose()
        {
            GL.DeleteProgram(handle);
        }

        public static implicit operator int(Shader shader) => shader.GetHandle();
    }
}
