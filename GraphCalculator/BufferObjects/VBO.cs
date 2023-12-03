using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCalculator.BufferObjects
{
    class VBO : IDisposable //Can be disposed after initialization
    {
        private int handle;
        private bool disposed;
        private float[] vertices = new float[0];

        public float[] GetVertices() => vertices;

        public VBO(float[] vertices, BufferUsageHint hint)
        {
            this.vertices = vertices;
            handle = GL.GenBuffer();
            GenerateVBO(vertices, hint);
        }

        public VBO()
        {
            handle = GL.GenBuffer();
        }

        public void GenerateVBO(float[] vertices, BufferUsageHint hint)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, handle);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, hint);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public int GetHandle() => handle;


        /// <summary>
        /// Disposes the <see cref="VBO"/> object.<br/>
        /// Should be done for all instantiated <see cref="VBO"/>s
        /// </summary>
        public void Dispose()
        {
            if (disposed)
                return;

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(handle);

            disposed = true;
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Implicitly converts from <see cref="VBO"/> to an <see cref="int"/> representing its handle.
        /// </summary>
        public static implicit operator int(VBO vbo) => vbo.GetHandle();
    }
}