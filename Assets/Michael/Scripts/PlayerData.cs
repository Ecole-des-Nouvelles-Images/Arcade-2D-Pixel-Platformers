using System;
using System.Collections.Generic;
using Michael.Fred;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Michael.Scripts
{
    public class PlayerData : MonoBehaviour
    {
        public int Playerindex;
        public List<Sprite> CharacterVisual;
        public int WinRound = 0;
        public int Health;
        public int MaxHealth = 3 ;

        private void Start()
        {
            Health = MaxHealth;
            WinRound = 0;
            
            if (  DataManager.Instance.PlayerDatasDict.TryGetValue(Playerindex, out int value))
            { 
                gameObject.GetComponent<SpriteRenderer>().sprite = CharacterVisual[value];
              // CharacterVisual[value].enabled = true;
               
            }
        }


        public void ResetHealth()
        {
            Health = MaxHealth;
        }
    }
}
