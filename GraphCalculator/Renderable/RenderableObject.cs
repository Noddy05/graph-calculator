using GraphCalculator.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCalculator.Renderable
{
    internal class RenderableObject
    {
        public bool render = true;
        public Mesh? mesh;
        public Transform3D transform;
        private Shader? shader;
        public Shader? GetShader() => shader;

        public RenderableObject(Shader shader)
        {
            transform = new Transform3D(this);
            SetShader(shader);
        }

        public void SetShader(Shader shader)
        {
            this.shader = shader;
            transform.SetShader(shader);

        }
    }
}
