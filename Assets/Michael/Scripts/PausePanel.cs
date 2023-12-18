using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _controllerWarningPanel;
    [SerializeField] private GameObject _eventSystem;
    [SerializeField] private GameObject OptionsPanel;
    [SerializeField] private GameObject RestartButton;
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
            OptionsPanel.SetActive(false);
            _eventSystem.SetActive(false);
        }
        else if (!PauseControl.IsPaused)
        {
            _pausePanel.SetActive(true);
            _eventSystem.SetActive(true);
             _eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(RestartButton);
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
