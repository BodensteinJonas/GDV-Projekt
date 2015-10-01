﻿using Microsoft.Xna.Framework.Audio;
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
        Song Start, SpaceIngame, FliegerIngame, GameOver, Victory;
        public bool liedIsFinished = false;

        public Sounds()
        {
            loadContent();
        }

        public void loadContent()
        {
            SpaceSchuss = Game1.instance.Content.Load<SoundEffect>("Sounds/SpaceSchuss").CreateInstance();
            FliegerSchuss = Game1.instance.Content.Load<SoundEffect>("Sounds/FliegerSchuss2").CreateInstance();
            ScheibenSound = Game1.instance.Content.Load<SoundEffect>("Sounds/ZielscheibeSound").CreateInstance();
            GameOver = Game1.instance.Content.Load<Song>("Sounds/GameOver");
            SpaceIngame = Game1.instance.Content.Load<Song>("Sounds/SpaceIngame");
            FliegerIngame = Game1.instance.Content.Load<Song>("Sounds/FliegerIngame");
            Start = Game1.instance.Content.Load<Song>("Sounds/Start");
            Victory = Game1.instance.Content.Load<Song>("Sounds/Victory");
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
                MediaPlayer.Play(Start);
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

        public void playVictory()
        {
            if (!liedIsFinished)
                MediaPlayer.Play(Victory);
            MediaPlayer.IsRepeating = true;
            liedIsFinished = true;
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

        public void stopTrack()
        {
            MediaPlayer.Stop();
            liedIsFinished = false;
        }

    }
}