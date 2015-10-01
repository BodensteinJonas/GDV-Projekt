using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyHigh
{
    public class SchussManager
    {
        public Model missile;
        public List<Bullet> schussListe = new List<Bullet>();
        public List<Bullet> schussRemoveListe = new List<Bullet>();

        KeyboardState lkb;
        public SchussManager()
        {
            missile = Game1.instance.Content.Load<Model>("Missile");
            lkb = Keyboard.GetState();
        }

        public void update() {


            if (Keyboard.GetState().IsKeyDown(Keys.Space) && lkb.IsKeyUp(Keys.Space))
            {
                Game1.instance.sound.playFliegerSchussSound();
                schussListe.Add(new Bullet(Game1.instance.player.playerPosition,
                                new Vector3(0.05f, 0.05f, 0.05f),
                                Vector3.Right,
                                Game1.instance.player.qPlayerRotation,
                                missile,
                                0.5f,
                                Game1.instance.angle.X));
            }

            foreach (Bullet m in schussListe)
            {
                m.Update();
                if (m.isDead)
                    schussRemoveListe.Add(m);
            }


            foreach (Bullet m in schussRemoveListe)
            {
                schussListe.Remove(m);
            }
            schussRemoveListe.Clear();

            lkb = Keyboard.GetState();
        }

        public void draw()
        {
            foreach (Bullet m in schussListe)
            {
                m.Draw();
            }
        }
       
    }
}
