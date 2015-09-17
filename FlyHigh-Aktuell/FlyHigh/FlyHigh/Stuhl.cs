using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyHigh
{
    class Stuhl
    {
        Model model;
        Vector3 pos;
        Vector3 rotation;
        Matrix[] bonetransformation;


        public Stuhl(Model m, Vector3 position)
        {
            pos = position;
            model = m;
            rotation = Vector3.Zero;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Update()
        {

        }

        public void Draw(Matrix projection, Matrix view)
        {

            bonetransformation = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(bonetransformation);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = bonetransformation[mesh.ParentBone.Index] * Matrix.CreateTranslation(-6f, 0.01f, -3) * Matrix.CreateScale(1.1f);
                    effect.View = view;
                    effect.Projection = projection;
                    effect.EnableDefaultLighting();
                }
                mesh.Draw();
            }
        }
    }
}
