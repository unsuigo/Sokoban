using UnityEngine;

namespace Sokoban
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Transform _playerEntryPoint;
        public Vector2 EntryPoint => _playerEntryPoint.position;

    }
}