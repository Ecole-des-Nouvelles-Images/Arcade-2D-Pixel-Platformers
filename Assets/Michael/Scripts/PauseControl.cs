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
        Time.timeScale = 1;
        
        
    }
    
   

    public void OnStartPause()
    {
        if (ControllerDisconnected <= 0)
        {
          TogglePause();
        }
    }

    public void OnDeviceLost()
    { 
        ControllerDisconnected++;
       Debug.Log("device deconnected : " + ControllerDisconnected);
       
       if (Time.timeScale > 0)
       {
           OnPausePressed.Invoke();
           Time.timeScale = 0;
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
    
    

    private void TogglePause()
    {
        if (Time.timeScale > 0)
        {
            OnPausePressed.Invoke();
            Time.timeScale = 0;
            IsPaused = true;
            DataManager.Instance.CurrentMusic.Pause();
        }
        else if (Time.timeScale == 0)
        {
            OnPausePressed.Invoke();
            Time.timeScale = 1;
            IsPaused = false;
            DataManager.Instance.CurrentMusic.UnPause();
        }
        
    }
}
