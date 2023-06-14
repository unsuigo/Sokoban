using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sokoban
{
    
    public class FieldContainer : MonoBehaviour
    {
        private Tile[] _tiles;
        private IMovable[] _movables;

        public Tile[] Tiles => _tiles;
        public IMovable[] Movables => _movables;


        public Action OnEnvironmentContainFinifhed;

        public void ContainAllLevelEnvironment()
        {
            _tiles = FindObjectsOfType<Tile>();
            _movables = FindObjectsOfType<Chest>();
            OnEnvironmentContainFinifhed?.Invoke();
        }

        private void OnDisable()
        {
            _tiles = null;
            _movables = null;
        }
    }
}