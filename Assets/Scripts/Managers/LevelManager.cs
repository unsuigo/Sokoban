using System;
using System.Collections;
using System.Collections.Generic;
using Sokoban;
using Sokoban.Service;
using UnityEngine;
using Zenject;

public class LevelManager : MonoBehaviour
{
   [SerializeField] private LevelsContainerScriiptable _levelsContainer;
   [SerializeField] private GameObject _playerPrefab;
   [SerializeField] private int _loadLevelIndex = 0;
   
   [Inject] private FieldContainer _fieldContainer;
   [Inject] private InputSystem _input;
   [Inject] private ScoreService _scoreService;
   [Inject] private SoundService _soundService;
   
   private int _currentLevelIndex = 0;
   private GameObject[] _levelsPrefabs;
   private Level _currentLevel;
   private Player _player = null;

   public Vector2 LevelPlayerEntryPoint { get; private set; }
   private void Start()
   {
      _levelsPrefabs = _levelsContainer._levelsPrefabs;
      LoadLevel(_loadLevelIndex);
   }

   public void LoadLevel(int index)
   {
      if (_currentLevel)
      {
         DeleteCurrentLevel();
      }
      var prefab = _levelsPrefabs[index];
      _currentLevel = Instantiate(prefab).GetComponent<Level>();
      LevelPlayerEntryPoint = _currentLevel.EntryPoint;
      _fieldContainer.ContainAllLevelEnvironment();
      CreatePlayer(LevelPlayerEntryPoint);
      _fieldContainer.ContainAllLevelEnvironment();
   }

   public void DeleteCurrentLevel()
   {
      Destroy(_currentLevel);
   }

   private void CreatePlayer(Vector2 entryPoint)
   {
      if (_player != null)
      {
         _player.transform.position = (Vector3)LevelPlayerEntryPoint;
         return;
      }
      var playerObj = Instantiate(_playerPrefab);
      playerObj.transform.position = (Vector3)LevelPlayerEntryPoint;
      _player = playerObj.GetComponent<Player>();
      _player.Init(_fieldContainer, _input, _scoreService, _soundService);
   }
}
