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
            CheckPlaneCollideWithBed();
            CheckPlaneCollideWithBlume1();
            CheckPlaneCollideWithBlume2();
            //CheckBulletCollideWithDisc();



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
            for (int i = 0; i < Game1.instance.player.planeSpheres.Length; i++)
            {
                for (int j = 0; j < Game1.instance.room.stStuhlSpheres.Length; j++)
                {
                    if (Game1.instance.player.planeSpheres[i].Intersects(Game1.instance.room.stStuhlSpheres[j]))
                    {
                        Console.WriteLine("PlayerSphere " + i + " collided with chair " + j);
                    }
                }
            }
        }
        
        private void CheckPlaneCollideWithBed()
        {
            for (int i = 0; i < Game1.instance.player.planeSpheres.Length; i++)
            {
                for (int j = 0; j < Game1.instance.room.bettSpheres.Length; j++)
                {
                    if (Game1.instance.player.planeSpheres[i].Intersects(Game1.instance.room.bettSpheres[j]))
                    {
                        Console.WriteLine("PlayerSphere " + i + " collided with Bed " + j);
                    }
                }
            }
        }

        private void CheckPlaneCollideWithBlume1()
        {
            for (int i = 0; i < Game1.instance.player.planeSpheres.Length; i++)
            {
                for (int j = 0; j < Game1.instance.room.blumeSpheres.Length; j++)
                {
                    if (Game1.instance.player.planeSpheres[i].Intersects(Game1.instance.room.blumeSpheres[j]))
                    {
                        Console.WriteLine("PlayerSphere " + i + " collided with Blume1 " + j);
                    }
                }
            }
        }

        private void CheckPlaneCollideWithBlume2()
        {
            for (int i = 0; i < Game1.instance.player.planeSpheres.Length; i++)
            {
                for (int j = 0; j < Game1.instance.room.blume2Spheres.Length; j++)
                {
                    if (Game1.instance.player.planeSpheres[i].Intersects(Game1.instance.room.blume2Spheres[j]))
                    {
                        Console.WriteLine("PlayerSphere " + i + " collided with Blume2 " + j);
                    }
                }
            }
        }

         /*private void CheckBulletCollideWithDisc()
       {
            for (int k = 0; k < Game1.instance.scheibenManager.scheibenListe.Count; k++)
            {
                if (Game1.instance.bullet.sphere.Intersects(Game1.instance.scheibenManager.scheibenListe[k].sphere))
                {
                    Console.WriteLine("Bullet collided with disc " + k);
                }
             }
        }*/
    }
}
