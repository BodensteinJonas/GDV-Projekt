using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyHigh
{
    class Couch
    {
        Model model;
        Vector3 pos;
        Vector3 rotation;
        Matrix[] bonetransformation;
        Rectangle boundingBox;


        public Couch(Model m, Vector3 position)
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
                    effect.World = bonetransformation[mesh.ParentBone.Index] * Matrix.CreateScale(1.7f) *Matrix.CreateRotationY(-2f) * Matrix.CreateTranslation(6.35f, 0, 12);
                    effect.View = view;
                    effect.Projection = projection;
                    effect.EnableDefaultLighting();
                }
                mesh.Draw();
            }
        }
    }
}
