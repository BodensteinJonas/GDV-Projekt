using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyHigh
{
    public class IntersectionManager
    {
        public IntersectionManager()
        {

        }

        public void update()
        {
            CheckPlaneCollideWithDisc();
            
            CheckPlaneCollideWithObject();

            checkPlaneCollideWithRoom();

            CheckBulletCollideWithDisc();

        }

        private void CheckPlaneCollideWithDisc()
        {

                for (int j = 0; j < Game1.instance.scheibenManager.scheibenListe.Count; j++)
                {
                    if (Game1.instance.player.sphere.Intersects(Game1.instance.scheibenManager.scheibenListe[j].sphere))
                    {
                        Console.WriteLine("PlayerSphere collided with disc " + j);
                    }
                }
        }

        private void CheckBulletCollideWithDisc()
        {
            foreach(Bullet b in Game1.instance.schussManager.schussListe)
            {
                foreach (Scheibe s in Game1.instance.scheibenManager.scheibenListe)
                {
                    if (b.sphere.Intersects(s.sphere))
                    {
                        b.isDead = true;
                        s.isDead = true;
                        Console.WriteLine("HIT!");
                    }
                }
            }
        }
        
        private void checkPlaneCollideWithRoom()
        {
            if(!Game1.instance.room.boundingBox.Intersects(Game1.instance.player.sphere))
            {
                Console.WriteLine("Plane collide with Room!");
                Game1.instance.gameState = Game1.GameState.startMenue;
            }
        }
        private void CheckPlaneCollideWithObject()
        {
            //TODO

            for (int j = 0; j < Game1.instance.room.rObj.Count; j++)
            {
                if (Game1.instance.room.rObj[j].boundingBox.Intersects(Game1.instance.player.sphere))
                {
                    Console.WriteLine("box collide with object");
                    Game1.instance.gameState = Game1.GameState.startMenue;
                }
            }

        }
    }
}
