using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL;
using GraphCalculator.Shaders;
using GraphCalculator.BufferObjects;
using GraphCalculator.Renderers;
using GraphCalculator.Renderable;
using GraphCalculator.Generation;

namespace GraphCalculator
{
    internal class Window : GameWindow
    {
        //Public variables:
        public double windowLocalTime; //Counts the seconds since the window opened

        public Window()
            : base(GameWindowSettings.Default, new NativeWindowSettings()
            {
                Title = "Complex Graphing Calculator",
                Size = new Vector2i(1080, 1080),
                StartVisible = false, //Making sure to not show game window before its done loading
            })
        {

        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
        }

        private IBO? ibo;
        private VAO? vao;
        private Shader? shader;

        private RenderableObject plane;
        private Renderer3D defaultRenderer;
        protected override void OnLoad()
        {
            IsVisible = true;
            GL.ClearColor(new Color4(1f, 0.5f, 0.25f, 1f));

            defaultRenderer = new Renderer3D();

            #region Create Square

            float[] vertices = new float[]
            {
                -0.5f, -0.5f, -1,    // 3------2
                 0.5f, -0.5f, -1,    // |      |
                 0.5f,  0.5f, -1,    // |      |
                -0.5f,  0.5f, -1     // 0------1
            };
            int[] indices = new int[]
            {
                            // +-----+
                0, 3, 1,    // | \  1|
                3, 2, 1     // |0  \ |
                            // +-----+
            };

            //A VAO (Vertex array object) will contain all vertex data, and assign it to the vertex shader
            vao = new VAO();
            //A VBO (Vertex buffer object) will transform the vertex data into something readable by the VAO.
            VBO vbo = new VBO(vertices, BufferUsageHint.StaticDraw);
            //An IBO (Index buffer object) will contain all data about the indices (triangles) and assign it to the fragment shader
            ibo = new IBO(indices, BufferUsageHint.StaticDraw);

            //Bind VAO and VBO so that data can be transferred between the two:
            vao.Bind(vbo);
            //Tell the VAO how to handle the data from the VBO:
            vao.BindVertexAttribPointer(0, 3, VertexAttribPointerType.Float, 3, 0);

            //Unbind VAO:
            GL.BindVertexArray(0);
            //Now that the data has been transferred to the VAO, the VBO can be disposed of
            vbo.Dispose();

            //At last define the shader:
            shader = new Shader(
                @"C:\Users\noah0\source\repos\GraphCalculator\GraphCalculator\Shaders\Materials\Standard\default_vertex.glsl",
                @"C:\Users\noah0\source\repos\GraphCalculator\GraphCalculator\Shaders\Materials\Standard\default_fragment.glsl"
            );

            plane = new RenderableObject(shader);
            plane.mesh = new Mesh(vao, ibo);
            defaultRenderer.AddObject(plane);
            //plane.transform.position = new Vector3(0, 0, 0);
            //plane.transform.scale = new Vector3(100);

            #endregion

            base.OnLoad();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            windowLocalTime += args.Time;

            //Clear background every frame to make room for the next frame
            GL.Clear(ClearBufferMask.ColorBufferBit);

            defaultRenderer.RenderScene();
            plane.transform.rotation.Z += 0.5f * (float)args.Time;

            SwapBuffers();

            base.OnRenderFrame(args);
        }
    }
}
