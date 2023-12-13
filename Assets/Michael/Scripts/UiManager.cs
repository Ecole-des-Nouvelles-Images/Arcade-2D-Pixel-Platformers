
using UnityEngine;
using UnityEngine.UI;


public class UiManager : MonoBehaviour
{
   
   public int HeartNumber;
   private int health;
   [SerializeField] private int Uiindex;
   
   public Image[] Hearts;
   public Sprite FullHeart;
   public Sprite emptyHeart;

   


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
            Hearts[i].sprite = emptyHeart;  
         }
      }
   }

   private void ShowPlayerUI()
   {
      health = GameManager.Instance.PlayerList[Uiindex-1].Health;
   }
   
}
