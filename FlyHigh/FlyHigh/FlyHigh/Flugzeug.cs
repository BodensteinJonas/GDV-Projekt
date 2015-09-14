using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyHigh
{
    class Flugzeug
    {

        Model model;
        //public Matrix view;
        Matrix[] bonetransformations;

        /*
         *  Steuerung
         */
        public Vector3 pos;
        Vector3 rotation;
        int speed;


        public Flugzeug(Model m)
        {
            /*
             *  Position des Flugzeugs
             */
            pos = new Vector3(0, 10, 0);
            rotation = Vector3.Zero;
            model = m;

            speed = 1;
        }


        public void Update()
        {

            /*
             *  Steuerung
             */
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.W))
            {
                pos.Z += 0.1f;
            }

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.S))
            {
                pos.Z -= 0.1f;
            }

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.A))
            {

                rotation.Y += .01f;
            }

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.D))
            {

                rotation.Y -= .01f;
            }
        }

        public void Draw(Matrix projection, Matrix view)
        {

            bonetransformations = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(bonetransformations);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = bonetransformations[mesh.ParentBone.Index] * Matrix.CreateScale(0.1f) * Matrix.CreateTranslation(pos * speed/2) *Matrix.CreateRotationX(rotation.X) * Matrix.CreateRotationY(rotation.Y) * Matrix.CreateRotationZ(rotation.Z);
                    effect.View = view;
                    effect.Projection = projection;
                    effect.EnableDefaultLighting();

                }

                mesh.Draw();
            }
        }

    }
}
