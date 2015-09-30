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
            
            CheckPlaneCollideWithChair();




        }

        private void CheckPlaneCollideWithDisc()
        {
            for (int i = 0; i < Game1.instance.player.planeSpheres.Length; i++)
            {
                for (int j = 0; j < Game1.instance.scheibenManager.scheibenListe.Count; j++)
                {
                    if (Game1.instance.player.planeSpheres[i].Intersects(Game1.instance.scheibenManager.scheibenListe[j].sphere))
                    {
                        Console.WriteLine("PlayerSphere " + i + " collided with disc " + j);
                    }
                }
            }
        }
        
        private void CheckPlaneCollideWithChair()
        {
            //TODO

            //for (int j = 0; j < Game1.instance.room.stStuhlSpheres.Length; j++)
            //{
            //    if (Game1.instance.room.stStuhlSpheres[j].Intersects(Game1.instance.player.boundingBox))
            //    {
            //        Console.WriteLine("box collide with chair");
            //    }

            //}
        }
    }
}
