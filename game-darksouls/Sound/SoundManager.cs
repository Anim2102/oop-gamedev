using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Sound
{
    public class SoundManager
{
        private bool isPlaying = false;
        private SoundEffectInstance lastEffect= null;

        private Dictionary<string,SoundEffect> soundEffects = new();

        public Dictionary<string, SoundEffect> SoundEffects
        {
            get { return soundEffects; }
        }

        public SoundManager() { }

        public void AddSoundEffect(string nameSound,SoundEffect soundEffect)
        {
            soundEffects.Add(nameSound, soundEffect);
        }

        public void PlaySoundEffect(string nameSound) 
        {
            

            if (soundEffects.ContainsKey(nameSound) && !isPlaying)
            {
                lastEffect = soundEffects[nameSound].CreateInstance();
                lastEffect.Play();
                isPlaying = true;
            }

            if (lastEffect.State == SoundState.Stopped)
                isPlaying = false;
        }
}
}
