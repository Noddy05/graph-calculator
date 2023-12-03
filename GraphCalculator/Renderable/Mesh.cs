using GraphCalculator.BufferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCalculator.Renderable
{
    internal class Mesh
    {
        private VAO vao;
        private IBO ibo;

        public VAO GetVAO() => vao;
        public IBO GetIBO() => ibo;

        public Mesh(VAO vao, IBO ibo) {
            this.vao = vao;
            this.ibo = ibo;
        }
    }
}
