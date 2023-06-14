using System;
using System.Collections;
using System.Collections.Generic;
using Sokoban.Service;
using UnityEngine;
using Zenject;


namespace Sokoban
{
    
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _movement;
        private bool _readyToMove = true;
        private FieldContainer _fieldContainer;
        private InputSystem _input;

        private ScoreService _scoreService;
        private SoundService _soundService;

        public Action LevelDone;
        
        public void Init(FieldContainer fieldContainer, InputSystem input, ScoreService scoreService, SoundService soundService)
        {
            _fieldContainer = fieldContainer;
            _input = input;
            _scoreService = scoreService;
            _soundService = soundService;
            _input.OnInput += OnInput;
        }
        
      
        private void OnInput(Vector2 direction)
        {
            if (_readyToMove)
            {
                _readyToMove = false;
                Move(direction);
            }
        }
        
        private void Move(Vector2 direction)
        {
            if (CanMove(direction))
            {
                _movement.Move(direction, result =>
                {
                    _readyToMove = result;
                    // if (_scoreService.IsAllGoalsResolved())
                    // {
                    //     LevelDone?.Invoke();
                    //     _soundService.PlaySound(SFXType.AllGoals);
                    // }
                    
                });
            }
        }

        private bool CanMove(Vector2 direction)
        {
            Vector2 newPosition = new Vector2(transform.position.x, transform.position.y) + direction;
            
            //Check All Tiles on direction if it Wall
            foreach (var tile in _fieldContainer.Tiles)
            {
                if (tile.transform.position.x == newPosition.x && tile.transform.position.y == newPosition.y)
                {
                    if (tile.TileState == TileState.Wall)
                    {
                        _readyToMove = true;
                        return false;
                    }
                }
            }

            //Check  Movables on direction
            foreach (IMovable movable in _fieldContainer.Movables)
            {
                Transform movableTransform = ((MonoBehaviour)movable).transform; 
                if (movableTransform.position.x == newPosition.x && movableTransform.position.y == newPosition.y)
                {
                    Vector2 positionBehindMovable = newPosition + direction;

                    //check the wall on direction direction
                    foreach (var tile in _fieldContainer.Tiles)
                    {
                        if (tile.transform.position.x == positionBehindMovable.x && tile.transform.position.y == positionBehindMovable.y)
                        {
                            if (tile.TileState == TileState.Wall)
                            {
                                _readyToMove = true;
                                return false;
                            }
                        }
                    }

                    //check the movables on direction direction
                    foreach (var item in _fieldContainer.Movables)
                    {
                        Transform itemTransform = ((MonoBehaviour)item).transform; 
                        if (itemTransform.position.x == positionBehindMovable.x && itemTransform.position.y == positionBehindMovable.y)
                        {
                            _readyToMove = true;
                            
                            return false;
                        }
                    }

                    movable.Push(direction, () =>
                    {
                        if (_scoreService.IsAllGoalsResolved())
                        {
                            LevelDone?.Invoke();
                            _soundService.PlaySound(SFXType.AllGoals);
                            Debug.Log($"IsAllGoalsResolved  ==  true  ");

                        }
                        
                    });
                    return true;
                }
            }
          
            return true;
        }
      
        private void OnDestroy()
        {
            _input.OnInput -= OnInput;
        }
    }
}