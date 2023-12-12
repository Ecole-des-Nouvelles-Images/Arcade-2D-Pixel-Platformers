using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UiManager : MonoBehaviour
{
   public Image[] Hearts;
   public int HeartNumber;
   public Sprite FullHeart;
   public Sprite emptyHeart;

   private void Update()
   {
      for (int i = 0; i < Hearts.Length; i++)
      {
         if (i < HeartNumber)
         {
            Hearts[i].SetEnabled(true);
         }
         else
         {
            Hearts[i].SetEnabled(false);
         }
      }
   }
}
