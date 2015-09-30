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
    public class Raum// : Microsoft.Xna.Framework.DrawableGameComponent
    {

        Model room;
        Model bed, couch, lowboy, plant1, plant2, rack, desk, chair, door;
        Raumobjekte bett, sofa, kommode, pflanze1, pflanze2, schrank, schreibtisch, stuhl, tuer;

        //Links zur Wand 
        public BoundingSphere sphereBettLW;
        Matrix sphereBettLWTranslation;
        //Rechts zur Wand
        public BoundingSphere sphereBettRW;
        Matrix sphereBettRWTranslation;
        //Links richtung Zimmer
        public BoundingSphere sphereBettL;
        Matrix sphereBettLTranslation;
        //Rechts richtung Zimmer
        public BoundingSphere sphereBettR;
        Matrix sphereBettRTranslation;
        //Mitte Links
        public BoundingSphere sphereBettML;
        Matrix sphereBettMLTranslation;
        //Mitte Rechts
        public BoundingSphere sphereBettMR;
        Matrix sphereBettMRTranslation;

        //Blume Topf
        public BoundingSphere sphereBlumeT;
        Matrix sphereBlumeTTranslation;
        //Blume Kopf
        public BoundingSphere sphereBlumeK;
        Matrix sphereBlumeKTranslation;

        //Blume Schreibtisch Topf
        public BoundingSphere sphereBlumeSchreibtischT;
        Matrix sphereBlumeSchreibtischTTranslation;
        //Blume Schreibtisch Kopf
        public BoundingSphere sphereBlumeSchreibtischK;
        Matrix sphereBlumeSchreibtischKTranslation;


        //Schreibtisch Stuhl Bottom
        public BoundingSphere spherestStuhlB;
        Matrix spherestStuhlBTranslation;
        //Schreibtisch Stuhl Top
        public BoundingSphere spherestStuhlT;
        Matrix spherestStuhlTTranslation;


        //...
        public BoundingSphere sphereschreibtisch;
        Matrix sphereschreibtischTranslation;

        public BoundingSphere[] bettSpheres = new BoundingSphere[6];
        public BoundingSphere[] blumeSpheres = new BoundingSphere[2];
        public BoundingSphere[] blume2Spheres = new BoundingSphere[2];
        public BoundingSphere[] stStuhlSpheres = new BoundingSphere[2];

        public Raum(Game game)
            //: base(game)
        {
            loadContent(Game1.instance.Content);
            bett = new Raumobjekte(game, bed,new Vector3(0f, 1.4f, -14f), MathHelper.ToRadians(90) , 2f);
            sofa = new Raumobjekte(game, couch, new Vector3(-16f, 0f, -12f), MathHelper.ToRadians(90), 0.04f);
            kommode = new Raumobjekte(game, lowboy, new Vector3(16.8f, 0f, -14f), MathHelper.ToRadians(0), 1f);
            pflanze1 = new Raumobjekte(game, plant1, new Vector3(-15.8f, 0f, -6f), MathHelper.ToRadians(90), 0.004f);
            pflanze2 = new Raumobjekte(game, plant2, new Vector3(14.8f, 0f, 1.5f), MathHelper.ToRadians(90), 1f);
            schrank = new Raumobjekte(game, rack, new Vector3(16.8f, 0f, -10f), MathHelper.ToRadians(0), 1.68f);
            schreibtisch = new Raumobjekte(game, desk, new Vector3(16.8f, 0f, 9f), MathHelper.ToRadians(180), 1.5f);
            stuhl = new Raumobjekte(game, chair, new Vector3(13.0f, 0f, 9f), MathHelper.ToRadians(90), 1.1f);
            tuer = new Raumobjekte(game, door, new Vector3(-18f, 0f, 12f), MathHelper.ToRadians(-90), 0.04f);

          //  tisch = new Raumobjekte(game, table, new Vector3(0f, 1.4f, -14f), MathHelper.ToRadians(0), 1f);
            
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
          //  table = c.Load<Model>("Tisch");

            bettSpheres[0] = sphereBettLW;
            bettSpheres[1] = sphereBettRW;
            bettSpheres[2] = sphereBettL;
            bettSpheres[3] = sphereBettR;
            bettSpheres[4] = sphereBettML;
            bettSpheres[5] = sphereBettMR;
            blumeSpheres[0] = sphereBlumeT;
            blumeSpheres[1] = sphereBlumeK;
            blume2Spheres[0] = sphereBlumeSchreibtischT;
            blume2Spheres[1] = sphereBlumeSchreibtischK;
            stStuhlSpheres[0] = spherestStuhlB;
            stStuhlSpheres[1] = spherestStuhlT;

        }

       // public override void Update(GameTime gameTime)
        //{

            //base.Update(gameTime);
        //}


        //public override void Draw(GameTime gameTime)
        public void Draw(GameTime gameTime)
        {
            Game1.instance.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone);
            draw();
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
           // tisch.Draw(gameTime);

            //Translation Bett
            sphereBettLWTranslation = Matrix.CreateTranslation(1.3f, 1.4f, -17f);
            sphereBettRWTranslation = Matrix.CreateTranslation(-1.3f, 1.4f, -17f);
            sphereBettLTranslation = Matrix.CreateTranslation(1.3f, 1.4f, -10.8f);
            sphereBettRTranslation = Matrix.CreateTranslation(-1.3f, 1.4f, -10.8f);
            sphereBettMLTranslation = Matrix.CreateTranslation(1.3f, 1.4f, -14f);
            sphereBettMRTranslation = Matrix.CreateTranslation(-1.3f, 1.4f, -14f);
            //Translation Blume an Couch
            sphereBlumeTTranslation = Matrix.CreateTranslation(-15.8f, 1.1f, -6f);
            sphereBlumeKTranslation = Matrix.CreateTranslation(-15.8f, 3.5f, -6f);
            //Translation Blume Schreibtisch
            sphereBlumeSchreibtischTTranslation = Matrix.CreateTranslation(14.8f, 0.8f, 1.5f);
            sphereBlumeSchreibtischKTranslation = Matrix.CreateTranslation(14.8f, 3f, 1.5f);
            //Translation Schreibtisch Stuhl
            spherestStuhlBTranslation = Matrix.CreateTranslation(13.0f, 0f, 9f);
            spherestStuhlTTranslation = Matrix.CreateTranslation(13.0f, 2.5f, 9f);
            //Translation Schreibtisch
            sphereschreibtischTranslation = Matrix.CreateTranslation(16.8f, 2f, 9f);

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

                    //Bett
                    bettSpheres[0].Center = sphereBettLWTranslation.Translation;
                    bettSpheres[0].Radius = 1.5f;
                    bettSpheres[1].Center = sphereBettRWTranslation.Translation;
                    bettSpheres[1].Radius = 1.5f;
                    bettSpheres[2].Center = sphereBettLTranslation.Translation;
                    bettSpheres[2].Radius = 1.5f;
                    bettSpheres[3].Center = sphereBettRTranslation.Translation;
                    bettSpheres[3].Radius = 1.5f;
                    bettSpheres[4].Center = sphereBettMLTranslation.Translation;
                    bettSpheres[4].Radius = 1.5f;
                    bettSpheres[5].Center = sphereBettMRTranslation.Translation;
                    bettSpheres[5].Radius = 1.5f;
                    //Blume Couch
                    blumeSpheres[0].Center = sphereBlumeTTranslation.Translation;
                    blumeSpheres[0].Radius = 1.2f;
                    blumeSpheres[1].Center = sphereBlumeKTranslation.Translation;
                    blumeSpheres[1].Radius = 1.5f;
                    //Blume Schreibtisch
                    blume2Spheres[0].Center = sphereBlumeSchreibtischTTranslation.Translation;
                    blume2Spheres[0].Radius = 1.0f;
                    blume2Spheres[1].Center = sphereBlumeSchreibtischKTranslation.Translation;
                    blume2Spheres[1].Radius = 1.7f;
                    //Schreibtischstuhl
                    stStuhlSpheres[0].Center = spherestStuhlBTranslation.Translation;
                    stStuhlSpheres[0].Radius = 1.5f;
                    stStuhlSpheres[1].Center = spherestStuhlTTranslation.Translation;
                    stStuhlSpheres[1].Radius = 1.3f;
                }
                mesh.Draw();
            }
            //Bett
            BoundingSphereRenderer.Render(bettSpheres[0], Game1.instance.GraphicsDevice, Game1.instance.viewMatrix, Game1.instance.projectionMatrix, Color.Red);
            BoundingSphereRenderer.Render(bettSpheres[1], Game1.instance.GraphicsDevice, Game1.instance.viewMatrix, Game1.instance.projectionMatrix, Color.Red);
            BoundingSphereRenderer.Render(bettSpheres[2], Game1.instance.GraphicsDevice, Game1.instance.viewMatrix, Game1.instance.projectionMatrix, Color.Red);
            BoundingSphereRenderer.Render(bettSpheres[3], Game1.instance.GraphicsDevice, Game1.instance.viewMatrix, Game1.instance.projectionMatrix, Color.Red);
            BoundingSphereRenderer.Render(bettSpheres[4], Game1.instance.GraphicsDevice, Game1.instance.viewMatrix, Game1.instance.projectionMatrix, Color.Red);
            BoundingSphereRenderer.Render(bettSpheres[5], Game1.instance.GraphicsDevice, Game1.instance.viewMatrix, Game1.instance.projectionMatrix, Color.Red);
            //Blume Couch
            BoundingSphereRenderer.Render(blumeSpheres[0], Game1.instance.GraphicsDevice, Game1.instance.viewMatrix, Game1.instance.projectionMatrix, Color.Red);
            BoundingSphereRenderer.Render(blumeSpheres[1], Game1.instance.GraphicsDevice, Game1.instance.viewMatrix, Game1.instance.projectionMatrix, Color.Red);
            //Blume Schreibtisch
            BoundingSphereRenderer.Render(blume2Spheres[0], Game1.instance.GraphicsDevice, Game1.instance.viewMatrix, Game1.instance.projectionMatrix, Color.Red);
            BoundingSphereRenderer.Render(blume2Spheres[1], Game1.instance.GraphicsDevice, Game1.instance.viewMatrix, Game1.instance.projectionMatrix, Color.Red);
            //Schreibtischstuhl
            BoundingSphereRenderer.Render(stStuhlSpheres[0], Game1.instance.GraphicsDevice, Game1.instance.viewMatrix, Game1.instance.projectionMatrix, Color.Red);
            BoundingSphereRenderer.Render(stStuhlSpheres[1], Game1.instance.GraphicsDevice, Game1.instance.viewMatrix, Game1.instance.projectionMatrix, Color.Red);
        }
    }
}
