using System.Collections.Generic;
using UnityEngine;
using Scripts.Audio;
using Scripts.Utilities.Singletons;

namespace Scripts.Managers
{
    public class AudioManager : NonPersistentSingleton<AudioManager>
    {
        [SerializeField] private List<SoundEffect> _soundEffectList = new List<SoundEffect>();
        private Dictionary<string, SoundEffect> _soundEffects = new Dictionary<string, SoundEffect>();
        
        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private AudioSource _sfxAudioSource;

        protected override void Awake()
        {

            base.Awake();

            CheckErrors();

            ItinializeSoundEffects();

        }

        private void CheckErrors()
        {
            List<string> errors = new();


            if (_musicAudioSource == null)
            {
                errors.Add("AudioManager: Music AudioSource is not assigned.");
            }

            if (_sfxAudioSource == null)
            {
                errors.Add("AudioManager: SFX AudioSource is not assigned.");
            }

            if (errors.Count > 0)
            {
                foreach (var error in errors)
                {
                    Debug.LogError(error);
                }
                throw new System.Exception("AudioManager initialization failed due to errors.");
            }
        }

        private void PlaySoundEffect(string soundName)
        {
            if (_soundEffects.TryGetValue(soundName, out SoundEffect soundEffect))
            {
                _sfxAudioSource.PlayOneShot(soundEffect.AudioClip, soundEffect.Volume);
            }
            else
            {
                Debug.LogWarning($"Sound effect '{soundName}' not found in the AudioManager.");
            }
        }


        private void ItinializeSoundEffects()
        {

            if (_soundEffectList == null || _soundEffectList.Count == 0)
            {
                Debug.LogWarning("AudioManager: No sound effects found in the AudioManager.");
                return;
            }

            _soundEffects.Clear();
            
            foreach (var soundEffect in _soundEffectList)
            {
                if (soundEffect != null && !string.IsNullOrEmpty(soundEffect.SoundName))
                {
                    if (!_soundEffects.ContainsKey(soundEffect.SoundName))
                    {
                        _soundEffects.Add(soundEffect.SoundName, soundEffect);
                    }
                    else
                    {
                        Debug.LogWarning($"Duplicate sound effect name '{soundEffect.SoundName}' in AudioManager.");
                    }
                }
            }
        }

        public void PlayMusic(AudioClip musicClip, float volume = 1f)
        {
            if (_musicAudioSource == null)
            {
                Debug.LogWarning("AudioManager: Music AudioSource is not assigned.");
                return;
            }

            _musicAudioSource.clip = musicClip;
            _musicAudioSource.volume = volume;
            _musicAudioSource.Play();
        }

        public void StopMusic()
        {
            if (_musicAudioSource == null)
            {
                Debug.LogWarning("AudioManager: Music AudioSource is not assigned.");
                return;
            }

            _musicAudioSource.Stop();
        }

        public void SetMusicVolume(float volume)
        {
            if (_musicAudioSource == null)
            {
                Debug.LogWarning("AudioManager: Music AudioSource is not assigned.");
                return;
            }

            _musicAudioSource.volume = volume;
        }

        public void PlayWalkSFX()
        {
            PlaySoundEffect("PlayerWalk");
        }

        public void PlayAttackSFX()
        {
            PlaySoundEffect("PlayerAttack");
        }

        public void PlayBurstSFX()
        {
            PlaySoundEffect("Burst");
        }

        public void PlayJumpSFX()
        {
            PlaySoundEffect("PlayerJump");
        }
        

        
        
    }
}