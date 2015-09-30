using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FlyHigh
{

    public class Flugzeug// : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public int hp = 1000;

        Model plane1;
        Model plane2;
        Texture2D texture1; 
        Texture2D texture2;

        public Vector3 playerPosition;
        public Matrix mPlayerRotation;
        public Quaternion qPlayerRotation;
        Quaternion calculatedRotation; 

        public float playerSpeed;
        float playerLeftRightRot;
        float playerUpDownRot;
        float playerRollRot;
        float sensitivity = 0.003f;
        public float speedToAdd = 0.003f;
        public float maxSpeed = 0.002f;

        public BoundingSphere sphere;
        KeyboardState kbState;
        MouseState mState;

        // BoundingBox
        public BoundingBox boundingBox;
        private BasicEffect lineEffect;
        public BoundingBoxRenderer bbRenderer = new BoundingBoxRenderer();
        public Color bbColor = Color.Blue;

        public Flugzeug(Game game)
            //: base(game)
        {
            playerPosition = new Vector3(0, 2, 0);
            qPlayerRotation = Quaternion.Identity;
        }

        public void loadContent(ContentManager c)
        {
            plane1 = c.Load<Model>("plane1");
            plane2 = c.Load<Model>("plane2");
            texture1 = c.Load<Texture2D>("texture1");
            texture2 = c.Load<Texture2D>("texture2");

            lineEffect = new BasicEffect(Game1.instance.GraphicsDevice);
            lineEffect.LightingEnabled = false;
            lineEffect.TextureEnabled = false;
            lineEffect.VertexColorEnabled = true;
        }

         public void update(){
            //setBoundingBox();
            KeyboardControls();
            MouseControls();
            MoveForward();
        }

        public void draw()
        {
            Matrix PlayerTransformation = Matrix.CreateScale(new Vector3(0.1f, 0.1f, 0.1f))
                
                                        * Matrix.CreateFromQuaternion(qPlayerRotation)
                                        * Matrix.CreateTranslation(playerPosition);
            //Vector3 offsetLeft = Vector3.Transform(new Vector3(-1f, 0,0), Matrix.CreateFromQuaternion(qPlayerRotation));

            Matrix sphereTrans1 = Matrix.Identity
                                * Matrix.CreateFromQuaternion(qPlayerRotation)
                                * Matrix.CreateTranslation(playerPosition);
                            //    * Matrix.CreateTranslation(offsetLeft);

           //Vector3 offsetRight = Vector3.Transform(new Vector3(1f, 0,0), Matrix.CreateFromQuaternion(qPlayerRotation));

           // Matrix sphereTrans2 = Matrix.Identity
           //                     * Matrix.CreateFromQuaternion(qPlayerRotation)
           //                     * Matrix.CreateTranslation(playerPosition)
           //                     * Matrix.CreateTranslation(offsetRight);

            if (Game1.instance.model == 1) {            
                foreach (ModelMesh mesh in plane1.Meshes)
                {
                    sphere = BoundingSphere.CreateMerged(sphere, mesh.BoundingSphere);

                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.World = PlayerTransformation; //planeWorld;
                        effect.View = Game1.instance.viewMatrix;
                        effect.Projection = Game1.instance.projectionMatrix;
                        effect.EnableDefaultLighting();
                        effect.TextureEnabled = true;
                        effect.Texture = texture1;

                        sphere.Center = sphereTrans1.Translation;
                        sphere.Radius = 0.3f;
                    }
                    mesh.Draw();
                }
            }
            else {
                foreach (ModelMesh mesh in plane2.Meshes)
                {
                    sphere = BoundingSphere.CreateMerged(sphere, mesh.BoundingSphere);

                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.World = PlayerTransformation; //planeWorld;
                        effect.View = Game1.instance.viewMatrix;
                        effect.Projection = Game1.instance.projectionMatrix;
                        effect.EnableDefaultLighting();
                        effect.TextureEnabled = true;
                        effect.Texture = texture2;

                        sphere.Center = sphereTrans1.Translation;
                        sphere.Radius = 0.1f;

                    }
                    mesh.Draw();
                }
            }

                BoundingSphereRenderer.Render(sphere, Game1.instance.GraphicsDevice, Game1.instance.viewMatrix, Game1.instance.projectionMatrix, Color.Red); 

             DrawBoundingBox(bbRenderer.CreateBoundingBoxBuffers(boundingBox, Game1.instance.GraphicsDevice, bbColor),
                    lineEffect, Game1.instance.GraphicsDevice, Game1.instance.viewMatrix, Game1.instance.projectionMatrix);
        }

        #region Controls
        private void KeyboardControls()
        {
            // Rotation of the Ship
            kbState = Keyboard.GetState();
           // playerLeftRightRot = 0.0f;
           // playerUpDownRot = 0.0f;

            if (kbState.IsKeyDown(Keys.Down) || kbState.IsKeyDown(Keys.W))
                playerUpDownRot += sensitivity;

            if (kbState.IsKeyDown(Keys.Up) || kbState.IsKeyDown(Keys.S))
                playerUpDownRot -= sensitivity;
            
            if (kbState.IsKeyDown(Keys.Left) || kbState.IsKeyDown(Keys.A))
                playerLeftRightRot += sensitivity / 2;

            if (kbState.IsKeyDown(Keys.Right) || kbState.IsKeyDown(Keys.D))
                playerLeftRightRot -= sensitivity / 2;

            calculatedRotation = Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), playerLeftRightRot)
                               * Quaternion.CreateFromAxisAngle(new Vector3(-1, 0, 0), playerUpDownRot);
            qPlayerRotation = calculatedRotation;

            // Speed of the Ship 
            if (kbState.IsKeyDown(Keys.LeftShift))
                playerSpeed += -speedToAdd;
            else if (kbState.IsKeyDown(Keys.LeftControl))
                playerSpeed += speedToAdd;
           // else playerSpeed = 0.0f;
        }

      
        private void MouseControls()
        {
            //mState = Mouse.GetState();
            Game1.instance.mouse = Mouse.GetState();
            //Mouse.SetPosition(646, 371);
           // mState.X = Mouse.GetState().X;
           // mState.Y = Mouse.GetState().Y;



            playerLeftRightRot = 0.0f;
            playerUpDownRot = 0.0f;

            playerLeftRightRot -= (Game1.instance.mouse.X - (Game1.instance.GraphicsDevice.Viewport.Width / 2));
            playerUpDownRot -= (Game1.instance.mouse.Y - (Game1.instance.GraphicsDevice.Viewport.Height / 2));

            calculatedRotation = Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), playerLeftRightRot * 0.01f)
                               * Quaternion.CreateFromAxisAngle(new Vector3(-1, 0, 0), playerUpDownRot * 0.01f);
            qPlayerRotation = calculatedRotation;
          
        }

        private void MoveForward()
        {
            Vector3 calculatedVector = Vector3.Transform(new Vector3(0, 0, -playerSpeed), Matrix.CreateFromQuaternion(qPlayerRotation));
            playerPosition += calculatedVector;
        }
        #endregion

        #region BoundingBox
        public BoundingBox getBoundingBox()
        {
            return boundingBox;
        }

        protected void setBoundingBox()
        {
            Vector3 offset = Vector3.Transform(new Vector3(0.0f, 4.5f, 35.0f), qPlayerRotation);
            Matrix translation = Matrix.CreateScale(new Vector3(0.3f, 0.3f, 0.3f)) * Matrix.CreateFromQuaternion(qPlayerRotation) * Matrix.CreateTranslation(playerPosition);// * Matrix.CreateTranslation(offset);  // scaleParam
            boundingBox = bbRenderer.CreateBoundingBox(plane1, translation);
            // 6.1f, 2.1f, 10.1f
        }

        protected void DrawBoundingBox(BoundingBoxRenderer bbRenderer, BasicEffect effect, GraphicsDevice graphicsDevice, Matrix view, Matrix projection)
        {
            graphicsDevice.SetVertexBuffer(bbRenderer.Vertices);
            graphicsDevice.Indices = bbRenderer.Indices;

            effect.World = Matrix.Identity; // Matrix.CreateFromQuaternion(playerRotation) * Matrix.CreateTranslation(playerPosition);
            effect.View = view;
            effect.Projection = projection;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.LineList, 0, 0,
                    bbRenderer.VertexCount, 0, bbRenderer.PrimitiveCount);
            }
        }
        #endregion
    }
}
