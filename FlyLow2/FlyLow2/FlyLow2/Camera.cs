using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlyLow2
{
    class Camera
    {
        public Vector3 cameraPosition;
        public float moveSpeed, rotateSpeed;

        public GraphicsDevice device;

        public Matrix view, projection;



        float yaw = 0;
        float pitch = 0;

        int oldX, oldY;

        public Camera(Vector3 cameraPosition, float moveSpeed, float rotateSpeed,GraphicsDevice device)
        {
            this.cameraPosition = cameraPosition;
            this.moveSpeed = moveSpeed;
            this.rotateSpeed = rotateSpeed;

            this.device = device;

            view = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Up);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),device.Viewport.AspectRatio, 0.1f, 1000.0f);

            ResetMouseCursor();


        }

        public void Update()
        {
            KeyboardState kState = Keyboard.GetState();

            if (kState.IsKeyDown(Keys.W))
            {
                Vector3 v = new Vector3(0, 0, -1) * moveSpeed;
                move(v);
                
            }

            if (kState.IsKeyDown(Keys.S))
            {
                Vector3 v = new Vector3(0, 0, 1) * moveSpeed;
                move(v);

            }

            if (kState.IsKeyDown(Keys.A))
            {
                Vector3 v = new Vector3(-1, 0, 0) * moveSpeed;
                move(v);

            }

            if (kState.IsKeyDown(Keys.D))
            {
                Vector3 v = new Vector3(1, 0, 0) * moveSpeed;
                move(v);

            }


            pitch = MathHelper.Clamp(pitch, -1.5f, 1.5f);

            MouseState mState = Mouse.GetState();

            int dx = mState.X - oldX;
            yaw -= rotateSpeed * dx;
            int dy = mState.Y - oldY;
            pitch -= rotateSpeed * dy;

            ResetMouseCursor();

            UpdateMatrices();

        }

        private void ResetMouseCursor()
        {
            int centerX = device.Viewport.Width / 2;
            int centerY = device.Viewport.Height / 2;

            Mouse.SetPosition(centerX, centerY);
            oldX = centerX;
            oldY = centerY;
        }

        private void UpdateMatrices()
        {
            Matrix rotation = Matrix.CreateRotationY(yaw) * Matrix.CreateRotationX(pitch);
            Vector3 transformedReference = Vector3.Transform(new Vector3(0,0,-1),rotation);
            Vector3 lookAt = cameraPosition + transformedReference;

            view = Matrix.CreateLookAt(cameraPosition, lookAt, Vector3.Up);
        }

        private void move(Vector3 v)
        {
            Matrix yRotation = Matrix.CreateRotationY(yaw);

            v = Vector3.Transform(v, yRotation);

            cameraPosition += v;
        }
    }
}
