
using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class UiManager : MonoBehaviour
{
   
   public int HeartNumber;
   private int health;
   [SerializeField] private int Uiindex;
   public Image[] Hearts;
   public Sprite FullHeart;

   public int Round;
   public Image[] Rounds;
   public Sprite EmptyHeart;
   public Sprite FullRound;
   public Sprite EmptyRound;


   
   private void Start()
   {
     
   }

   private void Update()
   {
      ShowPlayerUI();
      if (health > HeartNumber)
      {
         health = HeartNumber;
      }
      
      for (int i = 0; i < Hearts.Length; i++)
      {
         if (i < health)
         {
            Hearts[i].sprite = FullHeart;
         }
         else
         {
            Hearts[i].sprite = EmptyHeart;  
         }
      }

      for (int i = 0; i < Rounds.Length; i++)
      {
         if (i < Round)
         {
            Rounds[i].sprite = FullRound;
         }
         else
         {
            Rounds[i].sprite = EmptyRound;
         }
      }
      
   }

   private void ShowPlayerUI()
   {
      health = GameManager.Instance.PlayerList[Uiindex-1].Health;
      Round = GameManager.Instance.PlayerList[Uiindex - 1].WinRound;
   }
   
}
