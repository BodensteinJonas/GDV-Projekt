using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyHigh
{
    class Pflanze2
    {
        Model model;
        Vector3 pos;
        Vector3 rotation;
        Matrix[] bonetransformation;


        public Pflanze2(Model m, Vector3 position)
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
                    effect.World = bonetransformation[mesh.ParentBone.Index] * Matrix.CreateScale(1f) * Matrix.CreateTranslation(-9f, 0, -13f);
                    effect.View = view;
                    effect.Projection = projection;
                    effect.EnableDefaultLighting();
                }
                mesh.Draw();
            }
        }
    }
}
