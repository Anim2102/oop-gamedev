using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Sound
{
    public interface ISoundManager
{
        Dictionary<string, SoundEffect> SoundEffects { get; }
        void AddSoundEffect(string nameSound, SoundEffect soundEffect);
        void PlaySoundEffect(string nameSound);
    }
}
