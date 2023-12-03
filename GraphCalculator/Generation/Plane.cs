using GraphCalculator.BufferObjects;
using GraphCalculator.Renderable;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCalculator.Generation
{
    internal class Plane
    {
        public static Mesh CreateMesh(int xSubdivisions, int ySubdivisions)
        {
            float[] vertices = new float[xSubdivisions * ySubdivisions * 5];
            int[] triangles = new int[(xSubdivisions - 1) * (ySubdivisions - 1) * 6];
            for (int y = 0; y < ySubdivisions - 1; y++)
            {
                for (int x = 0; x < xSubdivisions - 1; x++)
                {
                    if (y <= (ySubdivisions) / 2 && y >= (ySubdivisions - 2) / 2)
                        continue;
                    triangles[(x + (xSubdivisions - 1) * y) * 6 + 0] =     x + xSubdivisions * y;
                    triangles[(x + (xSubdivisions - 1) * y) * 6 + 1] =     x + xSubdivisions * (y + 1);
                    triangles[(x + (xSubdivisions - 1) * y) * 6 + 2] = 1 + x + xSubdivisions * y;
                    triangles[(x + (xSubdivisions - 1) * y) * 6 + 3] = 1 + x + xSubdivisions * y;
                    triangles[(x + (xSubdivisions - 1) * y) * 6 + 4] =     x + xSubdivisions * (y + 1);
                    triangles[(x + (xSubdivisions - 1) * y) * 6 + 5] = 1 + x + xSubdivisions * (y + 1);
                }
            }
            for (int y = 0; y < ySubdivisions; y++)
            {
                for (int x = 0; x < xSubdivisions; x++)
                {
                    vertices[(x + xSubdivisions * y) * 5 + 0] = x / (float)(xSubdivisions - 1);
                    vertices[(x + xSubdivisions * y) * 5 + 1] = 0;
                    vertices[(x + xSubdivisions * y) * 5 + 2] = y / (float)(ySubdivisions - 1);
                    vertices[(x + xSubdivisions * y) * 5 + 3] = x / (float)(xSubdivisions - 1);
                    vertices[(x + xSubdivisions * y) * 5 + 4] = y / (float)(ySubdivisions - 1);
                }
            }

            VAO vao = new VAO();
            VBO vbo = new VBO(vertices, BufferUsageHint.StaticCopy);
            IBO ibo = new IBO(triangles, BufferUsageHint.StaticCopy);

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

            vao.BindVertexAttribPointer(0, 3, VertexAttribPointerType.Float, 5, 0);
            vao.BindVertexAttribPointer(1, 2, VertexAttribPointerType.Float, 5, 3);

            GL.BindVertexArray(0);
            vbo.Dispose();

            return new Mesh(vao, ibo);
        }
    }
}
