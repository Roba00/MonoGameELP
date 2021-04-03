using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace MonogameELP.Components
{
    class AudioSource
    {
        private SoundEffectInstance audioSource;

        public AudioSource(SoundEffect effect, bool isLooping, float volume, float pitch, float pan)
        {
            audioSource = effect.CreateInstance();
            audioSource.Volume = volume;
            audioSource.Pitch = pitch;
            audioSource.Pan = pan;
            audioSource.IsLooped = isLooping;
        }

        public AudioSource(SoundEffect effect, bool isLooping)
        {
            audioSource = effect.CreateInstance();
            audioSource.Volume = 0.5f;
            audioSource.Pitch = 0f;
            audioSource.Pan = 0f;
            audioSource.IsLooped = isLooping;
        }

        public AudioSource(SoundEffect effect)
        {
            audioSource = effect.CreateInstance();
            audioSource.Volume = 0.5f;
            audioSource.Pitch = 0f;
            audioSource.Pan = 0f;
            audioSource.IsLooped = false;
        }

        public SoundState GetState()
        {
            return audioSource.State;
        }

        public void Play()
        {
            audioSource.Play();
        }

        public void Resume()
        {
            audioSource.Resume();
        }

        public void Pause()
        {
            audioSource.Pause();
        }

        public void Stop()
        {
            audioSource.Stop();
        }

        public void ChangeVolume(float volume)
        {
            audioSource.Volume = volume;
        }

        public void ChangePitch(float pitch)
        {
            audioSource.Pitch = pitch;
        }

        public void ChangePan(float pan)
        {
            audioSource.Pan = pan;
        }

        public float GetVolume()
        {
            return audioSource.Volume;
        }

        public float GetPitch()
        {
            return audioSource.Pitch;
        }

        public float GetPan()
        {
            return audioSource.Pan;
        }
    }
}
