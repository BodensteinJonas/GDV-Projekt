﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyHigh
{
    public class ScheibenManager
    {
        public List<Scheibe> scheibenListe = new List<Scheibe>();
        Random rand = new Random();
        int scheibenAnzahl;

        public ScheibenManager()
        {
            scheibenAnzahl = 10;
            Model target = Game1.instance.Content.Load<Model>("Scheibe");

            for (int i = 0; i <= scheibenAnzahl; i++)
            {
                Vector3 targetPos = new Vector3(rand.Next(-11, 11), rand.Next(1, 8), rand.Next(-18, 18));
                scheibenListe.Add(new Scheibe(target, targetPos));
            }
        }

        public void update(GameTime gameTime)
        {
            foreach (Scheibe target in scheibenListe)
            {
                target.Update(gameTime);
            }
        }

        public void draw(GameTime gameTime)
        {
            foreach (Scheibe target in scheibenListe)
                target.Draw(gameTime);
        }
    }
}
