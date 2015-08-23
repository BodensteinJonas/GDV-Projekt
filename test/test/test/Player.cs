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

namespace test
{
    class Player
    {
        Model model;
        Model plane;

        Vector3 pos;
        Vector3 rotation;
        Vector3 camPos;

        Matrix view;
        Matrix[] bonetransformation;

        public Player(Vector3 position)
        {
            
            rotation = Vector3.Zero;
            camPos = new Vector3(position.X, position.Y, position.Z-100);
        }

        public void loadContent(ContentManager c)
        {
            model = c.Load<Model>("eurofighter fbx");
            plane = c.Load<Model>("plane");

        }

        public void update()
        {

            view = Matrix.CreateLookAt(camPos, pos, Vector3.Up);

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.W))
            {
                pos.Z += 1;
            }

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.S))
            {
                pos.Z -= 1;
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

        public void draw(Matrix projection)
        {
            bonetransformation = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(bonetransformation);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = bonetransformation[mesh.ParentBone.Index] *Matrix.CreateTranslation(pos)* Matrix.CreateScale(2f) * Matrix.CreateRotationX(rotation.X) * Matrix.CreateRotationY(rotation.Y) * Matrix.CreateRotationZ(rotation.Z);
                    effect.View = view;
                    effect.Projection = projection;
                    effect.EnableDefaultLighting();
                }
                mesh.Draw();
            }

            foreach (ModelMesh mesh in plane.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = bonetransformation[mesh.ParentBone.Index] * Matrix.CreateScale(6f) * Matrix.CreateRotationX(MathHelper.ToRadians(90f)) * Matrix.CreateRotationY(rotation.Y) * Matrix.CreateRotationZ(rotation.Z);
                    effect.View = view;
                    effect.Projection = projection;
                    effect.EnableDefaultLighting();
                }
                mesh.Draw();
            }

            
            
        }
    }
}
