using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Michael.Scripts
{
    public class PlayerData : MonoBehaviour
    {
        [SerializeField] private GameObject playerArmor;
        [SerializeField] private GameObject playerBall;
        public GameObject colorTarget; 
        [SerializeField] private Image _spriteIcon;
       [SerializeField] private Sprite _ballSprite;
       [SerializeField] private Sprite _spriteArmor;
       public bool ArmorIsRed;
       public bool BallIsRed;
       public bool ArmorIsSelected; 
       

        private void Start()
        {
            colorTarget = playerArmor;
            
            
        }

        private void Update()
        {
            if (ArmorIsSelected)
            {
                _spriteIcon.sprite = _spriteArmor;
            }
            else
            {

                _spriteIcon.sprite = _ballSprite;
            }
        }

        public void SwitchColorTarget()
        {
            if (colorTarget == playerArmor)
            {
                colorTarget = playerBall;
                ArmorIsSelected = false;
            }
            else
            {
                colorTarget = playerArmor;
                ArmorIsSelected = true;
            }
        }
        
        
        
        
        
        
        
    }
}
