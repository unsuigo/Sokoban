using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sokoban.Data;
using Sokoban.Utils;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Sokoban.Service
{
    public enum SFXType
    {
        Step,
        Push,
        AllGoals,
        Forbiden
    }

    public enum MusicType
    {
        Menu,
        Game,
        NoMusic
    }

    public interface ISoundService
    {
        void PlaySound(SFXType sfxType, string audioClipName = null);
    }

    public class SoundService : MonoBehaviour, ISoundService
    {
        private const string _musicVolumeName = "Music_volume";
        private const string _sfxVolumeName = "SFX_volume";

        [SerializeField] private SoundBank _soundBank;
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource[] _sfxSource;
        [SerializeField] private AudioMixer _audioMixer;

        public float MusicVolume => PlayerPrefsHelper.GetPlayerPrefsFloat(_musicVolumeName);
        public float SfxVolume => PlayerPrefsHelper.GetPlayerPrefsFloat(_sfxVolumeName);
        
        private float _currentMusicVolumeValue = default;
        private float _currentSfxVolumeValue = default;
        
        private void Start()
        {
            DontDestroyOnLoad(this);
            SetMusicVolume(MusicVolume);
            SetSfxVolume(SfxVolume);
            PlayMusic(MusicType.Game);
        }
        public void PlaySound(SFXType sfxType, string audioClipName = null)
        {
            var sfxSource = _sfxSource.FirstOrDefault(x => !x.isPlaying);
            if (sfxSource == null)
            {
                Debug.LogWarning($"Cannot play audio for sfx {sfxType}. All audio sources are busy");
                return;
            }
            _soundBank.Sounds.TryGetValue(sfxType, out var audioClips);
            if (audioClips.Length > 0)
            {
                var clip = audioClips[Random.Range(0, _soundBank.Sounds[sfxType].Length)];
                if (audioClipName != null)
                    clip = audioClips.ToList().Find(ac => ac.name == audioClipName);
                sfxSource.clip = clip;
                sfxSource.Play();
            }
        }
        public void PlayMusic(MusicType musicType)
        {
            _soundBank.Music.TryGetValue(musicType, out var audioClip);
            if (audioClip == _musicSource.clip)
                return;
            _musicSource.clip = audioClip;
            _musicSource.Play();
        }
        
        public void SetMusicVolume(float value)
        {
            _currentMusicVolumeValue = value;
            _audioMixer.SetFloat(_musicVolumeName, ConvertToDecibelByLog(value));
        }

        public void SetSfxVolume(float value)
        {
            _currentSfxVolumeValue = value;
            _audioMixer.SetFloat(_sfxVolumeName, ConvertToDecibelByLog(value));
        }

        private float ConvertToDecibelByLog(float value)
        {
            if (value < 0.0001f) value = 0.0001f;
            
            return Mathf.Log10(value) * 20f;
        }
        
        public void SaveVolumeValues()
        {
            PlayerPrefsHelper.SetPlayerPrefsFloat(_musicVolumeName, _currentMusicVolumeValue);
            PlayerPrefsHelper.SetPlayerPrefsFloat(_sfxVolumeName, _currentSfxVolumeValue);
        }
    }
}
    

