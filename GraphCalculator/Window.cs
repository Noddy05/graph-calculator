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
                Size = new Vector2i(1920, 1080),
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
        public Matrix4 perspectiveMatrix;
        public Camera camera;
        protected override void OnLoad()
        {
            IsVisible = true;
            GL.ClearColor(new Color4(1f, 0.5f, 0.25f, 1f));
            GL.Enable(EnableCap.DepthTest);

            camera = new Camera(new Vector3(0, 0, 0), new Vector3(0, 0, -5f));
            defaultRenderer = new Renderer3D();
            perspectiveMatrix = Matrix4.CreatePerspectiveFieldOfView(1.2f, 1f, 0.1f, 1000f);
            CursorState = CursorState.Grabbed;

            #region Create Square
            //At last define the shader:
            shader = new Shader(
                @"C:\Users\noah0\source\repos\GraphCalculator\GraphCalculator\Shaders\Materials\Standard\default_vertex.glsl",
                @"C:\Users\noah0\source\repos\GraphCalculator\GraphCalculator\Shaders\Materials\Standard\default_fragment.glsl",
                @"C:\Users\noah0\source\repos\GraphCalculator\GraphCalculator\Shaders\Materials\Libraries\complex.glsl",
                @"C:\Users\noah0\source\repos\GraphCalculator\GraphCalculator\Shaders\Materials\Libraries\hue.glsl"
            );

            plane = new RenderableObject(shader);
            plane.mesh = Plane.CreateMesh(1000, 1000);
            defaultRenderer.AddObject(plane);
            plane.transform.position = new Vector3(0, 0, 0);
            plane.transform.rotation = new Vector3(3.14f / 2, 0, 0);
            plane.transform.scale = new Vector3(100);

            #endregion

            base.OnLoad();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            windowLocalTime += args.Time;

            //Clear background every frame to make room for the next frame
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            defaultRenderer.RenderScene();

            SwapBuffers();

            base.OnRenderFrame(args);
        }
    }
}
