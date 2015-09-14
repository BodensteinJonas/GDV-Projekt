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
using FlyHigh;

namespace FlyHigh
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        Matrix projection;

        Camera camera;
        Flugzeug player;
        List<Scheibe> scheibenListe = new List<Scheibe>();
        Raum raum;
        Bett bett;
        Schreibtisch schreibtisch;
        Stuhl stuhl;
        Schrank schrank;
        Kommode kommode;
        Pflanze1 pflanze1;
        Pflanze2 pflanze2;
        Couch sofa;
        Tisch tisch;

        Random rand = new Random();

        int scheibenAnzahl;

        public Game1()
        {
            /*
             *  Einstellung des Bildschirms
             */

            this.Window.Title = "Project Six Feet Under!!!";
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            /*
             * Variabeln
             */
            scheibenAnzahl = 100;

        }

        protected override void Initialize()
        {
            base.Initialize(); 
        }

        protected override void LoadContent()
        {
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), graphics.GraphicsDevice.Viewport.AspectRatio, 1f, 1000f);
          


            /*
             * Lädt den Raum
             */

            Model room = Content.Load<Model>("Raum");
            raum = new Raum(room, Vector3.Zero);

            Model bed = Content.Load<Model>("Bett");
            bett = new Bett(bed, Vector3.Zero);

            Model desk = Content.Load<Model>("Schreibtisch");
            schreibtisch = new Schreibtisch(desk, Vector3.Zero);

            Model chair = Content.Load<Model>("Stuhl");
            stuhl = new Stuhl(chair, Vector3.Zero);
            
            Model rack = Content.Load<Model>("Schrank");
            schrank = new Schrank(rack, Vector3.Zero);
            
            Model lowboy = Content.Load<Model>("Kommode");
            kommode = new Kommode(lowboy, Vector3.Zero);

            Model plant1 = Content.Load<Model>("Pflanze 1");
            pflanze1 = new Pflanze1(plant1, Vector3.Zero);

            Model plant2 = Content.Load<Model>("Pflanze 2");
            pflanze2 = new Pflanze2(plant2, Vector3.Zero);

            Model couch = Content.Load<Model>("Couch");
            sofa = new Couch(couch, Vector3.Zero);

            Model table = Content.Load<Model>("Tisch");
            tisch = new Tisch(table, Vector3.Zero);

            /*
             * Lädt das Flugzeug
             */

            Model plane = Content.Load<Model>("Flugzeug");
            player = new Flugzeug(plane);

            camera = new Camera(new Vector3(0,5,10), 0.05f, 0.005f, 0.05f, GraphicsDevice);

            /*
             * Lädt die Zielscheiben und erstellt "scheinbenAnzahl" an zufälligen Positionen
             */
            Model target = Content.Load<Model>("Scheibe");

            for (int i = 0; i <= scheibenAnzahl; i++)
            {
                Vector3 targetPos = new Vector3(rand.Next(-11, 11), rand.Next(1, 8), rand.Next(-18, 18));
                scheibenListe.Add(new Scheibe(target, targetPos));
            }
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }


        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) == true)
            {
                this.Exit();
            }

            /*
             * Aktualisiert alle Objekte
             */
            foreach (Scheibe te in scheibenListe)
            {
                te.Update(gameTime);
            }
            camera.Update();
            player.Update();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.AliceBlue);

            /*
             * Zeichne den Raum
             */
            raum.Draw(projection, camera.view);
            bett.Draw(projection, camera.view);
            schreibtisch.Draw(projection, camera.view);
            stuhl.Draw(projection, camera.view);
            schrank.Draw(projection, camera.view);
            kommode.Draw(projection, camera.view);
            pflanze1.Draw(projection, camera.view);
            pflanze2.Draw(projection, camera.view);
            sofa.Draw(projection, camera.view);
            tisch.Draw(projection, camera.view);

            /*
             *  Scheiben zeichnen
             */
            foreach (Scheibe te in scheibenListe)
            {
                te.Draw(projection, camera.view);
            }

            /*
             * Flugzeug zeichnen
             */
            player.Draw(projection, camera.view);


            base.Draw(gameTime);
        }

    }
}
