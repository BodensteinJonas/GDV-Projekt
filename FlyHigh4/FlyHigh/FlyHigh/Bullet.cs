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
    class Bullet
    {
        Model missile;
        public Vector3 spawnPos, spawnScale, addPos, offset, finOffset, finX;
        public Matrix rotation;
        float finalSpeed, speed, xRot;
        public bool isDead = false;

        public BoundingSphere sphere;
        Matrix sphereTranslation;

        public Bullet(Vector3 spawnPos, Vector3 spawnScale, Vector3 offset, Matrix rotation, Model missile, float speed, float xRot)
        {
            this.spawnPos = spawnPos;
            this.spawnScale = spawnScale;
            this.offset = offset;
            this.missile = missile;// Game1.instance.Content.Load<Model>(@"Assets/Weapons/LaserBeam");
            this.speed = speed;
            this.rotation = rotation;//Game1.instance.playerOne.PlayerRotation;
            this.xRot = xRot;

        }

        public void Update()
        {
            finalSpeed += speed;
            addPos = Vector3.Transform(new Vector3(0.0f, 0.0f, -finalSpeed), rotation);
            //finOffset = Vector3.Transform(offset, rotation);
            //finX = Vector3.Transform(new Vector3(0.0f, 0.0f, -finalSpeed), Matrix.CreateRotationX(-xRot));
            addPos = Vector3.Transform(addPos, Matrix.CreateRotationX(-xRot));

            // Check distance to  bulletspawnpos
            Vector3 distance = spawnPos - addPos;
            float dist = distance.Length();

            if (dist >= 7.0f)
            {
                isDead = true;
            }

        }

        public void Draw()
        {
            //Switch Offset for Single/DoubleFire in WeaponManager
            DrawSingleFire();
            //DrawDoubleFire();
        }

        public void DrawSingleFire()
        {

            Matrix bullets = Matrix.Identity;

            bullets = Matrix.Identity
                                 * Matrix.CreateScale(spawnScale)
                                 * Matrix.CreateRotationX(xRot)
                                 * rotation
                                 * Matrix.CreateTranslation(spawnPos)
                                 * Matrix.CreateTranslation(addPos);
                                 //* Matrix.CreateTranslation(finX);
            sphereTranslation = bullets;
            foreach (ModelMesh mesh in this.missile.Meshes)
            {
                sphere = BoundingSphere.CreateMerged(sphere, mesh.BoundingSphere);
                foreach (BasicEffect effect in mesh.Effects)
                {
                    //effect.EnableDefaultLighting();
                    effect.World = bullets;
                    effect.View = Game1.instance.viewMatrix;
                    effect.Projection = Game1.instance.projectionMatrix;

                    sphere.Center = sphereTranslation.Translation;
                    sphere.Radius = 0.25f;
                }
                mesh.Draw();
            }
            BoundingSphereRenderer.Render(sphere, Game1.instance.GraphicsDevice, Game1.instance.viewMatrix, Game1.instance.projectionMatrix, Color.Red);

        }
    }

    
}
