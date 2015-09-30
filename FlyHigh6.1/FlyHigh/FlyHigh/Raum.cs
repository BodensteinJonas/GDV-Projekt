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
    public class Raum
    {

        Model room;
        Model bed, couch, lowboy, plant1, plant2, rack, desk, chair, door, logo;
        Raumobjekte bett, sofa, kommode, pflanze1, pflanze2, schrank, schreibtisch, stuhl, tuer;
        Texture2D logoTex;
        public Raum(Game game)

        {
            loadContent(Game1.instance.Content);
            bett = new Raumobjekte(game, bed,new Vector3(0f, 0f, -14f), MathHelper.ToRadians(0) , 2f,new Vector3(2f, 2f, 2f));
            sofa = new Raumobjekte(game, couch, new Vector3(-14f, 0f, -12f), MathHelper.ToRadians(90), 0.9f, new Vector3(.7f, .5f, 1f));
            kommode = new Raumobjekte(game, lowboy, new Vector3(16.8f, 0f, -14f), MathHelper.ToRadians(0), 1f, new Vector3(1f, 1f, 1f));
            pflanze1 = new Raumobjekte(game, plant1, new Vector3(-14f, 0f, -5.5f), MathHelper.ToRadians(90), .7f, new Vector3(.4f, .5f, .4f));
            pflanze2 = new Raumobjekte(game, plant2, new Vector3(14.8f, 0f, 1.5f), MathHelper.ToRadians(90), 1f, new Vector3(.2f, .7f, .2f));
            schrank = new Raumobjekte(game, rack, new Vector3(16.8f, 0f, -10f), MathHelper.ToRadians(-90), 1.68f, new Vector3(.7f, 1.68f, 3f));
            schreibtisch = new Raumobjekte(game, desk, new Vector3(16.8f, 0f, 9f), MathHelper.ToRadians(180), 1.5f, new Vector3(1.7f, 1.6f, 1.5f));
            stuhl = new Raumobjekte(game, chair, new Vector3(13.0f, 0f, 9f), MathHelper.ToRadians(90), 1.1f, new Vector3(1f, 1.1f, 1f));
            tuer = new Raumobjekte(game, door, new Vector3(-18f, 0f, 12f), MathHelper.ToRadians(0), .8f, new Vector3(1, .8f, .8f));

        }

        public void loadContent(ContentManager c)
        {
            room = c.Load<Model>("Raum");
            bed = c.Load<Model>("Bett");
            couch = c.Load<Model>("couch2");
            
            lowboy = c.Load<Model>("Kommode");
            plant1 = c.Load<Model>("Pflanze 1");
            plant2 = c.Load<Model>("Pflanze 2");
            rack = c.Load<Model>("Schrank");
            desk = c.Load<Model>("Schreibtisch");
            chair = c.Load<Model>("Stuhl");
            door = c.Load<Model>("tuer");
            logo = c.Load<Model>("logo");
            logoTex = c.Load<Texture2D>("Img/logo");



        }

        public void Draw(GameTime gameTime)
        {
            Game1.instance.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);
            draw();
            drawTexObekte();
            Game1.instance.spriteBatch.End();

            Game1.instance.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);
            bett.Draw(gameTime);
            sofa.Draw(gameTime);
            
            kommode.Draw(gameTime);
            pflanze1.Draw(gameTime);
            pflanze2.Draw(gameTime);
            schrank.Draw(gameTime);
            schreibtisch.Draw(gameTime);
            stuhl.Draw(gameTime);
            tuer.Draw(gameTime); 
            Game1.instance.spriteBatch.End();

            //base.Draw(gameTime);
        }


        private void draw()
        {
            Matrix planeWorld = Matrix.Identity;

            planeWorld = Matrix.Identity
                                * Matrix.CreateScale(2f);

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

        private void drawTexObekte()
        {
            Matrix planeWorld = Matrix.Identity;

            planeWorld = Matrix.Identity
                                * Matrix.CreateScale(new Vector3(1f, 1f, 0.7f))
                                * Matrix.CreateRotationX(MathHelper.ToRadians(90f))
                                * Matrix.CreateRotationY(MathHelper.ToRadians(180f))
                                * Matrix.CreateTranslation(new Vector3(0f, 4.0f, 17.90f));

            foreach (ModelMesh mesh in logo.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = planeWorld;
                    effect.View = Game1.instance.viewMatrix;
                    effect.Projection = Game1.instance.projectionMatrix;
                    effect.EnableDefaultLighting();
                    effect.TextureEnabled = true;
                    effect.Texture = logoTex;
                }
                mesh.Draw();
            }
        }
    }
}
