using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
        public Shader(string vertexShaderLocation, string fragmentShaderLocation,
            params string[] libraryLocations) { 
            window = Program.GetWindow();
            window.Unload += Dispose; //Automatically disposes shader whenever the window closes.

            GenerateShader(vertexShaderLocation, fragmentShaderLocation, libraryLocations);
        }

        private void GenerateShader(string vertexShaderLocation, string fragmentShaderLocation,
            params string[] libraryLocations)
        {
            handle = GL.CreateProgram();

            #region VertexShaderSource
            string vertexShaderSource = File.ReadAllText(vertexShaderLocation);
            int vertLibsStart = vertexShaderSource.IndexOf("#libs");
            string vertSource = vertexShaderSource.Substring(0, vertLibsStart);
            for (int i = 0; i < libraryLocations.Length; i++)
            {
                vertSource += File.ReadAllText(libraryLocations[i]);
            }
            Console.WriteLine(vertLibsStart);
            int start = vertLibsStart + "#libs".Length;
            vertSource += vertexShaderSource.Substring(start, vertexShaderSource.Length - start);
            #endregion

            #region FragmentShaderSource
            string fragmentShaderSource = File.ReadAllText(fragmentShaderLocation);
            int fragLibsStart = fragmentShaderSource.IndexOf("#libs");
            string fragSource = fragmentShaderSource.Substring(0, fragLibsStart);
            for (int i = 0; i < libraryLocations.Length; i++)
            {
                fragSource += File.ReadAllText(libraryLocations[i]) + "\n";
            }
            start = fragLibsStart + "#libs".Length;
            fragSource += fragmentShaderSource.Substring(start, fragmentShaderSource.Length - start);
            #endregion

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertSource);
            GL.CompileShader(vertexShader);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragSource);
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
