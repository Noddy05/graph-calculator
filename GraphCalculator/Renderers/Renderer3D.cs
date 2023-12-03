using GraphCalculator.BufferObjects;
using GraphCalculator.Renderable;
using GraphCalculator.Shaders;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCalculator.Renderers
{
    internal class Renderer3D
    {
        private List<RenderableObject> objects = new List<RenderableObject>();

        public void AddObject(RenderableObject renderable)
        {
            objects.Add(renderable);
        }
        public void RemoveObject(RenderableObject renderable)
        {
            objects.Remove(renderable);
        }

        public Renderer3D() {

        }

        public void RenderScene()
        {
            foreach(RenderableObject obj in objects)
            {
                if(obj.mesh == null)
                {
                    //No mesh - nothing to render
                    continue;
                }
                if (obj.GetShader() == null)
                {
                    //Apply default shader
                }
                else
                {
                    GL.UseProgram(obj.GetShader()!);
                }

                VAO vao = obj.mesh!.GetVAO();
                IBO ibo = obj.mesh!.GetIBO();

                obj.transform.BindTransform();
                GL.BindVertexArray(vao);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo);

                GL.DrawElements(PrimitiveType.Triangles, ibo.GetIndices().Length, DrawElementsType.UnsignedInt, 0);
            }
        }
    }
}
