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
    public class Raum : Microsoft.Xna.Framework.DrawableGameComponent
    {

        Model room;
        Vector3 position, rotation;

        public Raum(Game game)
            : base(game)
        {
        }

        public void loadContent(ContentManager c)
        {
            room = c.Load<Model>("Raum");
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            draw();
            base.Draw(gameTime);
        }

        private void draw()
        {
            Matrix planeWorld = Matrix.Identity;

            planeWorld = Matrix.Identity
                                * Matrix.CreateRotationX(Game1.instance.angle.X)
                              //  * rotation
                                * Matrix.CreateRotationY(MathHelper.ToRadians(180.0f))
                                * Matrix.CreateTranslation(position);

            foreach (ModelMesh mesh in room.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = planeWorld;
                    effect.View = Game1.instance.viewMatrix;
                    effect.Projection = Game1.instance.projectionMatrix;
                    effect.EnableDefaultLighting();
                }
                mesh.Draw();
            }
        }
    }
}
