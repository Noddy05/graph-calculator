using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCalculator.Renderers
{
    internal class Camera
    {
        public Matrix4 projection;
        private float fov;
        private float cameraSensitivity;
        private Vector3 rotation;
        public Vector3 position;
        public Vector3 offset;
        public Vector3 center;

        public Camera(Vector3 center, Vector3 offset, Vector3 startingPosition, Vector3 startingRotation, float fov = 60, 
            float cameraSensitivity = 1 / 800f)
        {
            position = startingPosition;
            rotation = startingRotation;
            this.cameraSensitivity = cameraSensitivity;
            this.fov = fov;
            SetFOV(fov);
            this.center = center;
            this.offset = offset;
        }

        public Camera(Vector3 center, Vector3 offset)
        {
            fov = 60;
            this.center = center;
            cameraSensitivity = 1 / 800f;
            this.offset = offset;
            SetFOV(fov);
            Program.GetWindow().MouseMove += OnMouseMove;
            Program.GetWindow().MouseWheel += OnMouseWheel;
            Program.GetWindow().KeyDown += KeyDown;
            Program.GetWindow().KeyUp += KeyUp;
            Program.GetWindow().RenderFrame += Update;
        }

        public void OnMouseMove(MouseMoveEventArgs e)
        {
            rotation += new Vector3(e.DeltaY, e.DeltaX, 0) * cameraSensitivity;
        }

        public void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (e.OffsetY < 0)
            {
                offset.Z *= 1.15f;
            }
            else
            {
                offset.Z /= 1.15f;
            }
        }

        bool eDown = false, qDown = false;
        public void KeyDown(KeyboardKeyEventArgs e)
        {
            if (e.Key == Keys.E)
                eDown = true;
            if (e.Key == Keys.Q)
                qDown = true;
        }
        public void KeyUp(KeyboardKeyEventArgs e)
        {
            if (e.Key == Keys.E)
                eDown = false;
            if (e.Key == Keys.Q)
                qDown = false;
        }
        public void Update(FrameEventArgs e)
        {
            float moveSpeed = 6 * offset.Z;
            if (eDown)
                center.Y -= 0.1f * (float)e.Time * moveSpeed;
            if (qDown)
                center.Y += 0.1f * (float)e.Time * moveSpeed;
        }
        public void SetFOV(float fov)
        {
            this.fov = fov;
            projection = Matrix4.CreatePerspectiveFieldOfView(fov / 180f * MathF.PI,
                (float)Program.GetWindow()!.Size.X / Program.GetWindow()!.Size.Y, 0.1f, 1000f);

        }

        public Matrix4 CameraMatrix()
            => Matrix4.CreateTranslation(position - center)
                * Matrix4.CreateFromQuaternion(Quaternion.FromEulerAngles(rotation))
                * Matrix4.CreateTranslation(-(position - offset));
    }
}
