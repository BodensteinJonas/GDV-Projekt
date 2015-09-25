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
    public class Raumobjekte : Microsoft.Xna.Framework.DrawableGameComponent
    {

        Model objekt;
        Vector3 position;
        float rotation, scale;

        public BoundingSphere sphereBett;
        Matrix sphereBettTranslation;

        public BoundingSphere sphereBlume;
        Matrix sphereBlumeTranslation;

        public BoundingSphere sphereBlume2;
        Matrix sphereBlume2Translation;

        public BoundingSphere sphereschreibtisch;
        Matrix sphereschreibtischTranslation;

        public Raumobjekte(Game game, Model model, Vector3 pos, float rot, float sca)
            : base(game)
          {
              objekt = model;
              position = pos;
              rotation = rot;
              scale = sca;
          }

        public override void Draw(GameTime gameTime)
        {
            draw();
        }

        public void draw()
        {
            Matrix planeWorld = Matrix.Identity;

            planeWorld = Matrix.Identity
                                * Matrix.CreateScale(scale)
                                * Matrix.CreateRotationY(rotation)
                                * Matrix.CreateTranslation(position);

            sphereBettTranslation = Matrix.CreateTranslation(0f, 1.4f, -14f);
            sphereBlumeTranslation = Matrix.CreateTranslation(-15.8f, 1.4f, -6f);
            sphereBlume2Translation = Matrix.CreateTranslation(14.8f, 0.9f, 1.5f);
            sphereschreibtischTranslation = Matrix.CreateTranslation(16.8f, 2f, 9f);

            foreach (ModelMesh mesh in objekt.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = planeWorld;
                    effect.View = Game1.instance.viewMatrix;
                    effect.Projection = Game1.instance.projectionMatrix;
                    effect.EnableDefaultLighting();

                    sphereBett.Center = sphereBettTranslation.Translation;
                    sphereBett.Radius = 2.8f;

                    sphereBlume.Center = sphereBlumeTranslation.Translation;
                    sphereBlume.Radius = 1.5f;

                    sphereBlume2.Center = sphereBlume2Translation.Translation;
                    sphereBlume2.Radius = 1.2f;

                    sphereschreibtisch.Center = sphereschreibtischTranslation.Translation;
                    sphereschreibtisch.Radius = 2.2f;
                }
                mesh.Draw();
            }
            BoundingSphereRenderer.Render(sphereBett, Game1.instance.GraphicsDevice, Game1.instance.viewMatrix, Game1.instance.projectionMatrix, Color.Red);
            Game1.instance.Sphere.Add(sphereBett);
            BoundingSphereRenderer.Render(sphereBlume, Game1.instance.GraphicsDevice, Game1.instance.viewMatrix, Game1.instance.projectionMatrix, Color.Red);
            Game1.instance.Sphere.Add(sphereBlume);
            BoundingSphereRenderer.Render(sphereBlume2, Game1.instance.GraphicsDevice, Game1.instance.viewMatrix, Game1.instance.projectionMatrix, Color.Red);
            Game1.instance.Sphere.Add(sphereBlume2);
            BoundingSphereRenderer.Render(sphereschreibtisch, Game1.instance.GraphicsDevice, Game1.instance.viewMatrix, Game1.instance.projectionMatrix, Color.Red);
            Game1.instance.Sphere.Add(sphereschreibtisch);
        }

    }
}
