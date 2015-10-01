using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyHigh
{
    public class Sounds
    {
        SoundEffectInstance schuss;
        Song lied;
        Song igame;
        bool liedIsFinished = false;

        public Sounds()
        {
            loadContent();
        }

        public void loadContent()
        {
            schuss = Game1.instance.Content.Load<SoundEffect>("Schuss_Sound").CreateInstance();
            //lied = Game1.instance.Content.Load<Song>("startmusik");
            igame = Game1.instance.Content.Load<Song>("ingame");
        }

        public void playSchussSound()
        {
            if (schuss.State != SoundState.Playing)
            schuss.Play();
        }

        public void playStartmenueTrack()
        {
            if (!liedIsFinished)
            {
                // MediaPlayer.Play(lied);
                 MediaPlayer.IsRepeating = true;
                 liedIsFinished = true;
            }      
        }

        public void playInGameTrack()
        {
            if (!liedIsFinished)
            {
                MediaPlayer.Play(igame);
                MediaPlayer.IsRepeating = true;
                liedIsFinished = true;
            }
        }
       
        public void stopStartmenueTrack()
        {
            MediaPlayer.Stop();
        }
    }
}
