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

        Model plane;
        public Vector3 playerPosition;
        public Matrix playerRotation;

        public BoundingSphere sphereFluegel1, sphereFluegel2;
        Matrix sphereTranslation1, sphereTranslation2;

        public Flugzeug(Game game)
            //: base(game)
        {
            playerPosition = Vector3.Zero;
        }

        public void loadContent(ContentManager c)
        {
            plane = c.Load<Model>("Flugzeug");
        }

       /* public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            draw();
            base.Draw(gameTime);
        }
        */
        public void draw()
        {
            Matrix planeWorld = Matrix.Identity;

            // !!!! Startposition vom Spieler hier setzten (wenn mehr als zwei verschiedenen Startpositionen benötigt werden) !!!! 
            //if(Game1.instance.cameraStyle != Game1.CameraStyle.FPV)                
            //playerPosition = new Vector3(0.0f, -2.0f, -10.0f);
            //else 
            //playerPosition = Vector3.Zero;

            // !!!! Startposition vom Spieler hier setzten !!!! 
            playerPosition = Game1.instance.cameraStyle != Game1.CameraStyle.FPV
                           ? playerPosition = new Vector3(0.0f, 0.0f, -4.0f)
                           : playerPosition = Vector3.Zero;

            // Plane rotation -> verwendet nur yaw
            Matrix cameraSyncRotation = Matrix.Identity * Matrix.CreateRotationY(-Game1.instance.angle.Y);
            playerRotation = cameraSyncRotation;
            // Playerposition mit cameraSyncPosition transformieren (hängt nun immer hinter dem Objekt, auch wenn man rotiert)
            playerPosition = Vector3.Transform(playerPosition, cameraSyncRotation);
            // Auf die errechnete Verschiebung nun noch die Cameraposition addieren
            playerPosition += Game1.instance.camPos;

            // Planetransformation
            planeWorld = Matrix.Identity
                                * Matrix.CreateScale(0.2f)
                                * Matrix.CreateRotationX(Game1.instance.angle.X)
                                * cameraSyncRotation
                                * Matrix.CreateRotationY(MathHelper.ToRadians(180.0f))
                                * Matrix.CreateTranslation(playerPosition);



            Matrix finRot = Matrix.CreateRotationX(Game1.instance.angle.X)
                                * cameraSyncRotation
                                * Matrix.CreateRotationY(MathHelper.ToRadians(180.0f));

            Vector3 finPosSphere1 = Vector3.Transform(playerPosition + new Vector3(0.5f, 0f, 0f), finRot);
            sphereTranslation1 = Matrix.CreateTranslation(playerPosition);// Matrix.CreateTranslation(finPosSphere1);
    
            //sphereTranslation2 = Matrix.CreateTranslation(playerPosition + new Vector3(-0.5f,0f,0f));

            foreach (ModelMesh mesh in plane.Meshes)
            {
                sphereFluegel1 = BoundingSphere.CreateMerged(sphereFluegel1, mesh.BoundingSphere);
                sphereFluegel2 = BoundingSphere.CreateMerged(sphereFluegel2, mesh.BoundingSphere);
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = planeWorld;
                    effect.View = Game1.instance.viewMatrix;
                    effect.Projection = Game1.instance.projectionMatrix;
                    effect.EnableDefaultLighting();

                    sphereFluegel1.Center = sphereTranslation1.Translation;
                    sphereFluegel1.Radius = .5f;
                    //sphereFluegel2.Center = sphereTranslation2.Translation;
                    //sphereFluegel2.Radius = .2f;
                }
                mesh.Draw();
            }

            BoundingSphereRenderer.Render(sphereFluegel1, Game1.instance.GraphicsDevice, Game1.instance.viewMatrix, Game1.instance.projectionMatrix, Color.Red);
            Game1.instance.Sphere.Add(sphereFluegel1);
           // BoundingSphereRenderer.Render(sphereFluegel2, Game1.instance.GraphicsDevice, Game1.instance.viewMatrix, Game1.instance.projectionMatrix, Color.Red);
            
        }
    }
}
