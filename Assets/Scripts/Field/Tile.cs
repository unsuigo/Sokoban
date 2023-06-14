using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sokoban
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private TileState _state;
        
        public TileState TileState => _state;
        
        ///////////////////////////////////////////////////
        
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsWall { get; set; }
        public bool HasBonus { get; set; }
        public bool HasMovableObject { get; set; }
        
    }

    public enum TileState
    {
        Wall,
        Floor,
        Goal
    }

}