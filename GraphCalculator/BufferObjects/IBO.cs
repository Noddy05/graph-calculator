using OpenTK.Graphics.OpenGL;
using System;

namespace GraphCalculator.BufferObjects
{
    class IBO : IDisposable //Must be kept to bind to buffer
    {
        private int handle;
        private bool disposed;
        private int[] indices = new int[0];

        public int[] GetIndices() => indices;

        public IBO(int[] indices, BufferUsageHint hint)
        {
            this.indices = indices;
            handle = GL.GenBuffer();
            GenerateIBO(indices, hint);
        }

        public IBO()
        {
            handle = GL.GenBuffer();
        }

        public void GenerateIBO(int[] indices, BufferUsageHint hint)
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, handle);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StaticCopy);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public int GetHandle() => handle;


        /// <summary>
        /// Disposes the <see cref="IBO"/> object.<br/>
        /// Should be done for all instantiated <see cref="IBO"/>s
        /// </summary>
        public void Dispose()
        {
            if (disposed)
                return;

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.DeleteBuffer(handle);

            disposed = true;
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Implicitly converts from <see cref="IBO"/> to an <see cref="int"/> representing its handle.
        /// </summary>
        public static implicit operator int(IBO ibo) => ibo.GetHandle();
    }
}
