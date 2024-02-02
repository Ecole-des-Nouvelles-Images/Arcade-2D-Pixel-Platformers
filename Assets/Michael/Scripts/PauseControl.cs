using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Michael.Fred;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseControl : MonoBehaviour
{
    public static bool IsPaused;
    public static Action OnPausePressed;
    public static Action OnControllerDisconnected;
    public static int ControllerDisconnected;
    
    
    
    void Start()
    {
        ControllerDisconnected = 0;
        IsPaused = false;
        TimeManager.Instance.timeScale = 1;
        
        
    }
    
   

    public void OnStartPause()
    {
        if (ControllerDisconnected <= 0 && CountDownController.CanPlay)
        {
          TogglePause();
        }
    }

    public void OnDeviceLost()
    { 
        ControllerDisconnected++;
       Debug.Log("device deconnected : " + ControllerDisconnected);
       
       if (TimeManager.Instance.timeScale > 0)
       {
           OnPausePressed.Invoke();
           TimeManager.Instance.timeScale = 0;
           IsPaused = true;
           DataManager.Instance.CurrentMusic.Pause();
       }
       
       OnControllerDisconnected.Invoke();
    }
    
    public void OnDeviceRegained()
    {
        ControllerDisconnected--;
        Debug.Log("device deconnected : " + ControllerDisconnected);
        OnControllerDisconnected.Invoke();
    }
    
    

    public static void TogglePause()
    {
        if (TimeManager.Instance.timeScale > 0)
        {
            OnPausePressed.Invoke();
            TimeManager.Instance.timeScale = 0;
            IsPaused = true;
          //  DataManager.Instance.CurrentMusic.Pause();
            
        }
        else if (TimeManager.Instance.timeScale == 0)
        {
            OnPausePressed.Invoke();
            TimeManager.Instance.timeScale = 1;
            IsPaused = false;
            GameManager.Instance.TutoPanel.SetActive(false);
            GameManager.Instance.TutoPanel2.SetActive(false);
            
          //  DataManager.Instance.CurrentMusic.UnPause();
        }
        
    }
    
}
