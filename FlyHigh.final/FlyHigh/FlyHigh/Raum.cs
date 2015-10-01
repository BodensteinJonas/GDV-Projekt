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
        // BoundingBox
        public BoundingBox boundingBox;
        private BasicEffect lineEffect;
        public BoundingBoxRenderer bbRenderer = new BoundingBoxRenderer();
        public Color bbColor = Color.Blue;

        Model room;
        Model bed, couch, lowboy, plant1, plant2, rack, desk, chair, door, logo;
        Raumobjekte bett, sofa, kommode, pflanze1, pflanze2, schrank, schreibtisch, stuhl, tuer;
        Texture2D logoTex;

        public List<Raumobjekte> rObj;


        public Raum(Game game)
        {
            rObj = new List<Raumobjekte>();

            loadContent(Game1.instance.Content);
            rObj.Add(bett = new Raumobjekte(game, bed,new Vector3(0f, 0f, -14f), MathHelper.ToRadians(0) , 2f,new Vector3(2f, 2f, 2f)));
            rObj.Add(sofa = new Raumobjekte(game, couch, new Vector3(-14f, 0f, -12f), MathHelper.ToRadians(90), 0.9f, new Vector3(.7f, .5f, 1f)));
            rObj.Add(kommode = new Raumobjekte(game, lowboy, new Vector3(16.8f, 0f, -14f), MathHelper.ToRadians(0), 1f, new Vector3(1f, 1f, 1f)));
            rObj.Add(pflanze1 = new Raumobjekte(game, plant1, new Vector3(-14f, 0f, -5.5f), MathHelper.ToRadians(90), .7f, new Vector3(.4f, .5f, .4f)));
            rObj.Add(pflanze2 = new Raumobjekte(game, plant2, new Vector3(14.8f, 0f, 1.5f), MathHelper.ToRadians(90), 1f, new Vector3(.2f, .7f, .2f)));
            rObj.Add(schrank = new Raumobjekte(game, rack, new Vector3(16.8f, 0f, -10f), MathHelper.ToRadians(-90), 1.68f, new Vector3(.7f, 1.68f, 3f)));
            rObj.Add(schreibtisch = new Raumobjekte(game, desk, new Vector3(16.8f, 0f, 9f), MathHelper.ToRadians(180), 1.5f, new Vector3(1.7f, 1.6f, 1.5f)));
            rObj.Add(stuhl = new Raumobjekte(game, chair, new Vector3(13.0f, 0f, 9f), MathHelper.ToRadians(90), 1.1f, new Vector3(1f, 1.1f, 1f)));
            rObj.Add(tuer = new Raumobjekte(game, door, new Vector3(-18f, 0f, 12f), MathHelper.ToRadians(0), .8f, new Vector3(1, .8f, .8f)));

            lineEffect = new BasicEffect(Game1.instance.GraphicsDevice);
            lineEffect.LightingEnabled = false;
            lineEffect.TextureEnabled = false;
            lineEffect.VertexColorEnabled = true;

            setBoundingBox();
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

            DrawBoundingBox(bbRenderer.CreateBoundingBoxBuffers(boundingBox, Game1.instance.GraphicsDevice, bbColor),
                            lineEffect, Game1.instance.GraphicsDevice, Game1.instance.viewMatrix, Game1.instance.projectionMatrix);
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
        #region BoundingBox
        public BoundingBox getBoundingBox()
        {
            return boundingBox;
        }

        protected void setBoundingBox()
        {
            Matrix translation = Matrix.CreateScale(new Vector3(2f, 2f, 2f))
                               * Matrix.CreateFromQuaternion(Quaternion.Identity)
                               * Matrix.CreateTranslation(new Vector3(0, 0, 0));

            boundingBox = bbRenderer.CreateBoundingBox(room, translation);
            // 6.1f, 2.1f, 10.1f
        }

        protected void DrawBoundingBox(BoundingBoxRenderer bbRenderer, BasicEffect effect, GraphicsDevice graphicsDevice, Matrix view, Matrix projection)
        {
            graphicsDevice.SetVertexBuffer(bbRenderer.Vertices);
            graphicsDevice.Indices = bbRenderer.Indices;

            effect.World = Matrix.Identity; // Matrix.CreateFromQuaternion(playerRotation) * Matrix.CreateTranslation(playerPosition);
            effect.View = view;
            effect.Projection = projection;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.LineList, 0, 0,
                    bbRenderer.VertexCount, 0, bbRenderer.PrimitiveCount);
            }
        }
        #endregion
    }
}
