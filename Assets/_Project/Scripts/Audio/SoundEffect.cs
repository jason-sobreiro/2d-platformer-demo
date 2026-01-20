using System.ComponentModel;
using UnityEngine;

namespace Scripts.Audio
{
    /// <summary>
    /// Represents a single sound effect with audio clip, volume, and name.
    /// Used as a component in SoundEffectContainer.
    /// </summary>
    [CreateAssetMenu(fileName = "SoundEffect_", menuName = "Audio/Sound Effect")]
    public class SoundEffect : ScriptableObject
    {
        [ReadOnly(true)]
        [SerializeField]
        private string soundName;

        [SerializeField]
        private AudioClip audioClip;

        [SerializeField]
        [Range(0f, 1f)]
        private float volume = 1f;

        public string SoundName => soundName;
        public AudioClip AudioClip => audioClip;
        public float Volume => volume;
        
        public void SetSoundName(string name)
        {
            soundName = name;
        }
    }
}
