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
        Model room;
        Model plane;

        
        Vector3 rotation;
        

        
        Matrix[] bonetransformation;

        public Player(Vector3 position)
        {
            
            rotation = Vector3.Zero;
            
        }

        public void loadContent(ContentManager c)
        {
            plane = c.Load<Model>("flieger");
            room = c.Load<Model>("room");

        }

        public void update()
        {

            //view = Matrix.CreateLookAt(camPos, pos, Vector3.Up);

            //if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.W))
            //{
            //    pos.Z += 2;
            //    camPos.Z += 2;
            //}

            //if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.S))
            //{
            //    pos.Z -= 2;
            //    camPos.Z -= 2;
            //}

            //if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.A))
            //{

            //    pos.X += 2;
            //    camPos.X += 2;
            //}

            //if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.D))
            //{

            //    pos.X -= 2;
            //    camPos.X -= 2;
            //}

            //mouseState = Mouse.GetState();

            //if (mouseState.LeftButton == ButtonState.Pressed)
            //{
            //    rotation.X += 0.1f;
            //}

            //if (mouseState.RightButton == ButtonState.Pressed)
            //{
            //    rotation.X -= 0.1f;
            //}

        }

        

        public void draw(Matrix projection, Vector3 pos)
        {
            bonetransformation = new Matrix[plane.Bones.Count];
            plane.CopyAbsoluteBoneTransformsTo(bonetransformation);

            foreach (ModelMesh mesh in plane.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = bonetransformation[mesh.ParentBone.Index] * Matrix.CreateScale(10f) * Matrix.CreateTranslation(pos) * Matrix.CreateRotationX(rotation.X) * Matrix.CreateRotationY(rotation.Y) * Matrix.CreateRotationZ(rotation.Z);
                    effect.View = Game1.instance.viewMatrix;
                    effect.Projection = projection;
                    effect.EnableDefaultLighting();
                }
                mesh.Draw();
            }

            bonetransformation = new Matrix[room.Bones.Count];
            room.CopyAbsoluteBoneTransformsTo(bonetransformation);

            foreach (ModelMesh mesh in room.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = bonetransformation[mesh.ParentBone.Index] * Matrix.CreateTranslation(0, -1f, 0) * Matrix.CreateScale(100f) * Matrix.CreateRotationX(rotation.X) * Matrix.CreateRotationY(MathHelper.ToRadians(135)) * Matrix.CreateRotationZ(rotation.Z);
                    effect.View = Game1.instance.viewMatrix;
                    effect.Projection = projection;
                    effect.EnableDefaultLighting();
                }
                mesh.Draw();
            }

            
            
        }
    }
}
