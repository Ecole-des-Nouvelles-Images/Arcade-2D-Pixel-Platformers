using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CountDownController : MonoBehaviourSingleton<CountDownController>
{
    public int CountDownTime;
    public TextMeshProUGUI CountDownText;
    public TextMeshProUGUI RoundText;
    public static bool CanPlay;
    public Animator RoundAnimator;
    void Start()
    {
       
    }

   

    void UpdateCountdown()
    {

        
            CanPlay = false;
            RoundText.text = "Round " + GameManager.CurrentRound;
            CountDownText.text = CountDownTime.ToString();
            CountDownTime--;
            //sound design
        
      

        if (CountDownTime < 0)
        {
            RoundAnimator.SetBool("ShowRoundPanel ", false);
            CountDownText.text = "GO";
            //sound design
            CancelInvoke("UpdateCountdown");
           Invoke(nameof(HidePanel),1f);
        }
    }

    private void HidePanel()
    {
        //GameManager.Instance.FadeAnimator.SetBool("FadeIn", true);
        CanPlay = true;

    }
    


    

}
