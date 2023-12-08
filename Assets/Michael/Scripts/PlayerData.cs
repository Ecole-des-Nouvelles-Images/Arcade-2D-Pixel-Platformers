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
      
        

        private void Start()
        {
            if (  DataManager.Instance.PlayerDatasDict.TryGetValue(Playerindex, out int value))
            { 
                gameObject.GetComponent<SpriteRenderer>().sprite = CharacterVisual[value];
            }
            
            
           
         
         
        }
       
    }
}
