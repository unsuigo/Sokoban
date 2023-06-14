using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sokoban.Service;
using UnityEngine;
using Zenject;


namespace Sokoban
{
    public interface IScoreService
    {
        public  bool IsAllGoalsResolved();
    }
    
    public class ScoreService : MonoBehaviour, IScoreService
    {
        [Inject] private FieldContainer _fieldContainer;

        
        private Tile[] _tiles;
        private Tile[] _goalTiles;
        private IMovable[] _movables;
        private List<Tile> _goalsList;
        
        public Action OnAllGoalsResolved;

        private void Start()
        {
            DontDestroyOnLoad(this);
            _fieldContainer.OnEnvironmentContainFinifhed += Init;
        }

        private void Init()
        {
            if (_fieldContainer != null)
            {
                _tiles = _fieldContainer.Tiles;
                _goalsList = new List<Tile>();
                Debug.Log($"_fieldContainer.Tiles {_fieldContainer.Tiles.Length}");
                foreach (var tile in _tiles)
                {
                    if (tile.TileState == TileState.Goal)
                    {
                        Debug.Log($"_tiles ::::TileState.Goal");
                        _goalsList.Add(tile);
                    }
                }
                // var array = (Tile[]) _tiles.Where(x => x.TileState == TileState.Goal);
                // _goalTiles = array;
            }
            else
            {
                Debug.Log($"_fieldContainer nulll !!!!!!!!!");

            }
           
            Debug.Log($"_GoalsList {_goalsList.Count}");
            _movables = _fieldContainer.Movables;
            
            _fieldContainer.OnEnvironmentContainFinifhed -= Init;
        }
        

        public  bool IsAllGoalsResolved()
        {
            if (_goalsList == null)
            {
                Debug.Log($"_goalsList nulll !!!!!!!!!");
                return false;

            }
            for ( int i = 0; i < _goalsList.Count; i++)
            {
               
                for (int j = 0; j < _movables.Length; j++)
                {
                    if (_goalsList[i].transform.position.x == _movables[j].Transform.position.x &&
                        _goalsList[i].transform.position.y == _movables[j].Transform.position.y)
                    {
                        Debug.Log($"1st is  _goalsList {i} ==  _movables {j}  ");
                        break;
                    }

                    if (j == _movables.Length-1)
                    {
                        if (_goalsList[i].transform.position.x != _movables[j].Transform.position.x ||
                            _goalsList[i].transform.position.y != _movables[j].Transform.position.y)
                        {
                            Debug.Log($"2nd if _goalsList {i} !=  last _movables {_movables.Length-1}  ");

                            return false;
                        }
                    }
                   
                }
            }

            OnAllGoalsResolved?.Invoke();
            return true;
        }

        private void SaveTime(string time)
        {
            
        }
    }
}