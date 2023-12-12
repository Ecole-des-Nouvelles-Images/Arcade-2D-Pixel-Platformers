using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CountDownController : MonoBehaviour
{
    public int CountDownTime;
    public TextMeshProUGUI CountDownText;
    public GameObject CountDownPanel;

    void Start()
    {
        InvokeRepeating("UpdateCountdown",0f,1f);
    }

    void UpdateCountdown()
    {
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
    }
    


    

}
