using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _controllerWarningPanel;
    [SerializeField] private GameObject _eventSystem;
    private void OnEnable()
    {
       PauseControl.OnPausePressed += OpenPanel;
       PauseControl.OnControllerDisconnected += OpenWarningPanel;
    }

    private void OnDisable()
    {
        PauseControl.OnPausePressed  -= OpenPanel;
        PauseControl.OnControllerDisconnected -= OpenWarningPanel;
    }

    private void OpenPanel()
    {
        if (PauseControl.IsPaused)
        {
            _pausePanel.SetActive(false);
        }
        else if (!PauseControl.IsPaused)
        {
            _pausePanel.SetActive(true);
            _eventSystem.SetActive(true);
        }
    }

    private void OpenWarningPanel()
    {
        if (PauseControl.ControllerDisconnected > 0 )
        {
            _controllerWarningPanel.SetActive(true);
        }
        if (PauseControl.ControllerDisconnected <= 0 )
        {
            _controllerWarningPanel.SetActive(false);
        }
      
    }
    
    
    
    
}
