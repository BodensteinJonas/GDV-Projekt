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
        SoundEffectInstance FliegerSchuss;
        SoundEffectInstance SpaceSchuss;
        SoundEffectInstance ScheibenSound;
        Song Start;
        Song SpaceIngame;
        Song FliegerIngame;
        Song GameOver;
        public bool liedIsFinished = false;

        public Sounds()
        {
            loadContent();
        }

        public void loadContent()
        {
<<<<<<< HEAD
            SpaceSchuss = Game1.instance.Content.Load<SoundEffect>("SpaceSchuss").CreateInstance();
            FliegerSchuss = Game1.instance.Content.Load<SoundEffect>("FliegerSchuss2").CreateInstance();
            ScheibenSound = Game1.instance.Content.Load<SoundEffect>("ZielscheibeSound").CreateInstance();
            GameOver = Game1.instance.Content.Load<Song>("GameOver");
            SpaceIngame = Game1.instance.Content.Load<Song>("SpaceIngame");
            FliegerIngame = Game1.instance.Content.Load<Song>("FliegerIngame");
            Start = Game1.instance.Content.Load<Song>("Start");
=======
            schuss = Game1.instance.Content.Load<SoundEffect>("Schuss_Sound").CreateInstance();
            //lied = Game1.instance.Content.Load<Song>("startmusik");
            igame = Game1.instance.Content.Load<Song>("ingame");
>>>>>>> Final
        }

        public void playFliegerSchussSound()
        {
            if (FliegerSchuss.State != SoundState.Playing)
            FliegerSchuss.Play();
        }

        public void playSpaceSchussSound()
        {
            if (SpaceSchuss.State != SoundState.Playing)
                SpaceSchuss.Play();
        }
        public void playScheibenSound()
        {
            if (ScheibenSound.State != SoundState.Playing)
                ScheibenSound.Play();
        }

        public void playStartmenueTrack()
        {
            if (!liedIsFinished)
            {
<<<<<<< HEAD
                 MediaPlayer.Play(Start);
=======
                // MediaPlayer.Play(lied);
>>>>>>> Final
                 MediaPlayer.IsRepeating = true;
                 liedIsFinished = true;
            }      
        }

        public void playInGameTrackSpace()
        {
            if (!liedIsFinished)
            {
                MediaPlayer.Play(SpaceIngame);
                MediaPlayer.IsRepeating = true;
                liedIsFinished = true;
            }
        }
        
        public void playInGameTrackFlieger()
        {
            if (!liedIsFinished)
            {
                MediaPlayer.Play(FliegerIngame);
                MediaPlayer.IsRepeating = true;
                liedIsFinished = true;
            }
        }
        public void playGameover()
        {
            if (!liedIsFinished)
            {
                MediaPlayer.Play(GameOver);
                MediaPlayer.IsRepeating = true;
                liedIsFinished = true;
            }
        }
       
        public void stopStartmenueTrack()
        {
            MediaPlayer.Stop();
            liedIsFinished = false;
        }
        public void stopInGameTrackFlieger()
        {
            MediaPlayer.Stop();
            liedIsFinished = false;
        }
        public void stopInGameTrackSpace()
        {
            MediaPlayer.Stop();
            liedIsFinished = false;
        }

    }
}
