using GraphCalculator.Renderers;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCalculator.Renderable
{
    internal class Transform3D
    {
        public Vector3 position = new Vector3(0);
        public Vector3 rotation = new Vector3(0);
        public Vector3 scale = new Vector3(1);

        private RenderableObject renderable;
        public Transform3D(RenderableObject renderable)
        {
            this.renderable = renderable;
        }
        public Transform3D(RenderableObject renderable, Vector3 position)
        {
            this.renderable = renderable;
            this.position = position;
        }
        public Transform3D(RenderableObject renderable, Vector3 position,
            Vector3 rotation)
        {
            this.renderable = renderable;
            this.position = position;
            this.rotation = rotation;
        }
        public Transform3D(RenderableObject renderable, Vector3 position,
            Vector3 rotation, Vector3 scale)
        {
            this.renderable = renderable;
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }

        private int uniformTransformLocation = -1;
        private int uniformPerspectiveLocation = -1;
        private int uniformCameraLocation = -1;
        public void SetShader(int shader)
        {
            uniformTransformLocation = GL.GetUniformLocation(shader, "transform");
            uniformPerspectiveLocation = GL.GetUniformLocation(shader, "perspective");
            uniformCameraLocation = GL.GetUniformLocation(shader, "camera");
        }

        public void BindTransform()
        {
            Matrix4 transformationMatrix = Matrix4.CreateFromQuaternion(
                Quaternion.FromEulerAngles(rotation))
                * Matrix4.CreateTranslation(position) * Matrix4.CreateScale(scale);
            GL.UniformMatrix4(uniformTransformLocation, false, ref transformationMatrix);
            Camera camera = Program.GetWindow().camera;
            Matrix4 cameraMatrix = camera.CameraMatrix();
            Matrix4 perspectiveMatrix = camera.projection;
            GL.UniformMatrix4(uniformPerspectiveLocation, false, ref perspectiveMatrix);
            GL.UniformMatrix4(uniformCameraLocation, false, ref cameraMatrix);
        }
    }
}
