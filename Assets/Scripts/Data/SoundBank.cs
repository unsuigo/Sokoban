using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sokoban.Service;
using UnityEngine;


namespace Sokoban.Data
{
    
   [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SoundBank")]
        public class SoundBank : ScriptableObject
        {
            [SerializeField] private List<SoundMap> _sounds;
            [SerializeField] private List<MusicMap> _music;
            public Dictionary<SFXType, AudioClip[]> Sounds => _sounds.ToDictionary(x => x.SfxType, x => x.AudioClip);
            public Dictionary<MusicType, AudioClip> Music => _music.ToDictionary(x => x.MusicType, x => x.AudioClip);
        }
        [Serializable]
        public struct SoundMap
        {
            [SerializeField] private SFXType _sfxType;
            [SerializeField] private AudioClip[] _audioClip;
            public SFXType SfxType => _sfxType;
            public AudioClip[] AudioClip => _audioClip;
        }
        [Serializable]
        public struct MusicMap
        {
            [SerializeField] private MusicType _musicType;
            [SerializeField] private AudioClip _audioClip;
            public MusicType MusicType => _musicType;
            public AudioClip AudioClip => _audioClip;
        }

}