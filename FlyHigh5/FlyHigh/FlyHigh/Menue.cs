using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyHigh
{
    public class Menue
    {
        // Buttons
        Texture2D sb;
        Rectangle sbrec;

        Texture2D end;
        Rectangle endrec;        

        // Backrounds
        Texture2D backg;
        Rectangle backgrec;

        // Mouse
        Texture2D mouseTex;
        Rectangle mouseRec;
        Vector2 mousePos;

        bool debug = true;

        public Menue()
        {
            loadContent();

        }

        private void loadContent()
        {
            // Mouse
            mouseTex = Game1.instance.Content.Load<Texture2D>("MouseRec");

            // Buttons
            sb = Game1.instance.Content.Load<Texture2D>("spielstart");
            sbrec = new Rectangle(1280 / 2 - 50, 720 / 3 - 25, 100, 50);

            end = Game1.instance.Content.Load<Texture2D>("beenden");
            endrec = new Rectangle(1280 / 2 - 50, 720 / 2 - 25, 100, 50);

            //Background
            backg = Game1.instance.Content.Load<Texture2D>("hintergrund");
            backgrec = new Rectangle(0,0,1280,720);
        }

        public void update(GameTime gt)
        {
            mousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            mouseRec = new Rectangle((int)mousePos.X - 10, (int)mousePos.Y - 10, 20, 20);

            // Intersect ist collsionsüberprüfung 
            if (mouseRec.Intersects(sbrec) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Game1.instance.sound.stopStartmenueTrack();
                Game1.instance.gameState = Game1.GameState.ingame;
            }

            if (mouseRec.Intersects(endrec) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Game1.instance.Exit();
            }
            

            //Console.WriteLine(mousePos);
        }

        public void draw(SpriteBatch batch)
        {
            batch.Begin();
            //White für Standartfarbe bei Texturen
            batch.Draw(backg, backgrec, Color.White);
            batch.Draw(sb,sbrec,Color.White);
            batch.Draw(end, endrec, Color.White);
            batch.Draw(mouseTex, mouseRec, Color.White);


            // Debug
            if(debug)
             batch.Draw(mouseTex, sbrec, Color.White);
             batch.Draw(mouseTex, endrec, Color.White);



            batch.End();
        }
    }
}
