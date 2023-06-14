using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokoban.Data
{
    
    public class DataManager : MonoBehaviour
    {
        [ContextMenu("Save game Data")]

        public void SaveData()
        {
            Debug.Log($" Save");
        }

        [ContextMenu("Load game Data")]
        public void LoadData()
        {
            Debug.Log($" Load");
        }
    }
}
