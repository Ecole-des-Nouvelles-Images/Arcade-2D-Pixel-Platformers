using System;
using System.Collections.Generic;
using Michael.Fred;
using TMPro;
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
        public TextMeshProUGUI PlayerNumberText;
        public Vector3 InitialPosition;
        public string PlayerPseudo;
        public List<string> PlayerNames = new List<string> { "maximadok", "mathisruk", "Christorar", "Mikaralik" };
        public Sprite EndGameVisual;
        public int characterChoice;

        private void Start()
        {
            Health = MaxHealth;
            WinRound = 0;
            PlayerNumberText.text = "J" + Playerindex;
            
            if (  DataManager.Instance.PlayerDatasDict.TryGetValue(Playerindex, out int value))
            {
                characterChoice = value;
              EndGameVisual = CharacterVisual[value];
                PlayerPseudo = PlayerNames[value]; 

                // CharacterVisual[value].enabled = true;
            }

            InitialPosition = transform.position;



        }


        public void ResetHealth()
        {
            Health = MaxHealth;
        }
        
       
        
        
        
    }
}
