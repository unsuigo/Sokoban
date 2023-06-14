using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokoban.Utils
{
    
    public static class PlayerPrefsHelper
    {
        public static void SetPlayerPrefsBool(string pref, bool setting)
        {
            PlayerPrefs.SetInt(pref, setting ? 1 : 0);
        }
        
        public static bool GetPlayerPrefsBool(string pref)
        {
            return PlayerPrefs.GetInt(pref, 1) == 1;
        }
        
        public static void SetPlayerPrefsFloat(string pref, float value)
        {
            PlayerPrefs.SetFloat(pref, value);
        }
        
        public static float GetPlayerPrefsFloat(string pref)
        {
            return PlayerPrefs.HasKey(pref) ? PlayerPrefs.GetFloat(pref) : 1f;
        }
    }

}