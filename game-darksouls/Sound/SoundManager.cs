using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Sound
{
    public class SoundManager : ISoundManager
{
        private SoundEffectInstance lastEffect = null;
        private Song backGroundSong = null;

        private Dictionary<string,SoundEffect> soundEffects = new();

        public Dictionary<string, SoundEffect> SoundEffects
        {
            get { return soundEffects; }
        }

        public SoundManager() { }
        
        public void PlayBackGroundSong(Song song)
        {
            backGroundSong = song;
            MediaPlayer.Play(backGroundSong);
            MediaPlayer.IsRepeating = true;

        }

        public void AddSoundEffect(string nameSound,SoundEffect soundEffect)
        {
            soundEffects.Add(nameSound, soundEffect);
        }

        public void PlaySoundEffect(string nameSound) 
        {
            if (!soundEffects.ContainsKey(nameSound))
                return;

            if (lastEffect != null)
            {
                lastEffect.Stop();
                lastEffect.Dispose();
            }

            lastEffect = soundEffects[nameSound].CreateInstance();
            lastEffect.Play();
        }

        public void ForceSoundEffect(string nameSound)
        {
            if (soundEffects.ContainsKey(nameSound))
            {
                lastEffect = soundEffects[nameSound].CreateInstance();
                lastEffect.Play();
                
            }
        }
}
}
