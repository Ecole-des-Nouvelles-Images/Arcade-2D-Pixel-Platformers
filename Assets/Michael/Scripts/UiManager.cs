
using System;
using Christopher.Proto.Scripts;
using TMPro;
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
   public Image ImageCooldown;
   public Image ImageSkill;
   public float cooldown ;
   public float cooldownDuration;
   public Image ImageBackButtons;
   public Color BlueColor;
   public Color RedColor;

   public Sprite RedArmorSprites;
   public Sprite RedBallSprites;

   public Sprite BlueArmorSprites;
   public Sprite BlueBallSprites;
   
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

      if (GameManager.Instance.PlayerList[Uiindex - 1].GetComponent<PlayerControler>()._armorSelected)
      {
         
         if (GameManager.Instance.PlayerList[Uiindex - 1].GetComponent<PlayerControler>().CurrentColor == "rouge")
         {
            ImageSkill.sprite = RedArmorSprites;
            ImageCooldown.sprite = RedArmorSprites;
            ImageBackButtons.color = BlueColor;
         }
         else if (GameManager.Instance.PlayerList[Uiindex - 1].GetComponent<PlayerControler>().CurrentColor == "bleu")
         {
            ImageSkill.sprite = BlueArmorSprites;
            ImageCooldown.sprite = BlueArmorSprites;
            ImageBackButtons.color = RedColor;
         }
      }
      if (!GameManager.Instance.PlayerList[Uiindex - 1].GetComponent<PlayerControler>()._armorSelected)
      {
         
         if (GameManager.Instance.PlayerList[Uiindex - 1].GetComponent<PlayerControler>().CurrentColor == "rouge")
         {
            ImageSkill.sprite = RedBallSprites;
            ImageCooldown.sprite = RedBallSprites;
            ImageBackButtons.color = BlueColor;
         }
         else if (GameManager.Instance.PlayerList[Uiindex - 1].GetComponent<PlayerControler>().CurrentColor == "bleu")
         {
            ImageSkill.sprite = BlueBallSprites;
            ImageCooldown.sprite = BlueBallSprites;
            ImageBackButtons.color = RedColor;
         }
      }
      
      else
      {
         
         /*if (GameManager.Instance.PlayerList[Uiindex - 1].GetComponent<PlayerControler>().MyBalls[0]
                .GetComponent<Ball>().CurrentColor == "rouge")
         {
            ImageSkill.sprite = RedBallSprites;
            ImageCooldown.sprite = RedBallSprites;
            ImageBackButtons.color = BlueColor;
         }
         else if ( !GameManager.Instance.PlayerList[Uiindex - 1].GetComponent<PlayerControler>().HandedBall && 
                   GameManager.Instance.PlayerList[Uiindex - 1].GetComponent<PlayerControler>().MyBalls[0]
                      .GetComponent<Ball>().CurrentColor == "bleu")
         {
            ImageSkill.sprite = BlueBallSprites;
            ImageCooldown.sprite = BlueBallSprites;
            ImageBackButtons.color = RedColor;
            
         }*/
      }



      ImageCooldown.fillAmount =  Mathf.Clamp01( (cooldown / cooldownDuration));
      
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
      cooldown =  GameManager.Instance.PlayerList[Uiindex-1].GetComponent<PlayerControler>().CurrentCooldownColorChange;
      cooldownDuration =  GameManager.Instance.PlayerList[Uiindex-1].GetComponent<PlayerControler>().CooldownColorChange;
      
   }
   
}
