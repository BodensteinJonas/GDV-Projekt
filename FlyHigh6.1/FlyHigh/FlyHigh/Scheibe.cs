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
    public class Scheibe
    {
        Model target;
        Vector3 pos, rotation;
        public BoundingSphere sphere;
        Matrix sphereTranslation;
        public bool isDead;

        public Scheibe(Model m, Vector3 position)
        {
            isDead = false;
            target = m;
            pos = position;
        }

        public void Update(GameTime gameTime)
        {

            rotation.Y += .05f;
        }

        public void Draw(GameTime gametime)
        {
            if (!isDead)
            {
                drawScheibe(gametime);
            }
        }

        public void drawScheibe(GameTime gameTime)
        {
            Matrix planeWorld = Matrix.Identity;

            planeWorld = Matrix.Identity
                                * Matrix.CreateScale(0.5f)
                                //* Matrix.CreateRotationX(.5f)
                                * Matrix.CreateRotationY(rotation.Y)
                                * Matrix.CreateTranslation(pos);
                                

            sphereTranslation = Matrix.CreateTranslation(pos);//planeWorld;

            foreach (ModelMesh mesh in target.Meshes)
            {
                sphere = BoundingSphere.CreateMerged(sphere, mesh.BoundingSphere);
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = planeWorld;
                    effect.View = Game1.instance.viewMatrix;
                    effect.Projection = Game1.instance.projectionMatrix;
                    effect.EnableDefaultLighting();

                    sphere.Center = sphereTranslation.Translation;
                    sphere.Radius = .3f;
                }
                mesh.Draw();
            }
            BoundingSphereRenderer.Render(sphere, Game1.instance.GraphicsDevice, Game1.instance.viewMatrix, Game1.instance.projectionMatrix, Color.Red);
        }
    }
}
