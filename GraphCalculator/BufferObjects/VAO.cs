using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCalculator.BufferObjects
{
    class VAO : IDisposable
    {
        private int handle;
        private float[] vertices;
        private bool disposed;

        public VAO()
        {
            handle = GL.GenVertexArray();
        }

        public void Bind(VBO vbo)
        {
            GL.BindVertexArray(this);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            vertices = vbo.GetVertices();
        }

        public void BindVertexAttribPointer(int index, int size, VertexAttribPointerType pointerType, int stride, int offset)
        {
            //Instantiate vertex array, which will permanently hold vertex data from the vertex buffer.
            //Assign data to vertex array:
            GL.VertexAttribPointer(index, size, pointerType, false, stride * sizeof(float), offset * sizeof(float));
            GL.EnableVertexAttribArray(index);
        }

        public int GetHandle() => handle;


        /// <summary>
        /// Disposes the <see cref="VAO"/> object.<br/>
        /// Should be done for all instantiated <see cref="VAO"/>s
        /// </summary>
        public void Dispose()
        {
            if (disposed)
                return;

            GL.BindVertexArray(0);
            GL.DeleteVertexArray(handle);

            disposed = true;
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Implicitly converts from <see cref="VAO"/> to an <see cref="int"/> representing its handle.
        /// </summary>
        public static implicit operator int(VAO vao) => vao.GetHandle();
    }
}