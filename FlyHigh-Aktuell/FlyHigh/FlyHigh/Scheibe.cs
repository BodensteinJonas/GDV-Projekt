using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyHigh
{
    class Scheibe
    {
        Model model;
        Vector3 pos, rotation;
        Matrix[] bonetransformation;



        public Scheibe(Model m, Vector3 position)
        {
            rotation = Vector3.Zero;
            pos = position;
            model = m;
        }

        public void Update(GameTime gameTime)
        {

            rotation.Y += .05f;
        }

        public void Draw(Matrix projection, Matrix view)
        {

            bonetransformation = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(bonetransformation);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = bonetransformation[mesh.ParentBone.Index] * Matrix.CreateScale(0.3f) * Matrix.CreateRotationY(rotation.Y) * Matrix.CreateTranslation(pos);
                    effect.View = view;
                    effect.Projection = projection;
                    effect.EnableDefaultLighting();

                }

                mesh.Draw();

            }
        }

    }
}
