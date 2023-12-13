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
    public GameObject CountDownPanel;

    void Start()
    {
       
    }

    void UpdateCountdown()
    {
        
        
            PauseControl.IsPaused = true;
            CountDownPanel.gameObject.SetActive(true);
            CountDownText.text = CountDownTime.ToString();
            CountDownTime--;
            //sound design
        
      

        if (CountDownTime < 0)
        {
            CountDownText.text = "GO";
            //sound design
            CancelInvoke("UpdateCountdown");
           Invoke(nameof(HidePanel),1f);
        }
    }

    private void HidePanel()
    {
        CountDownPanel.gameObject.SetActive(false);
        PauseControl.IsPaused = false;
    }
    


    

}
