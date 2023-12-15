using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Michael.Scripts.Menu
{
    public class MenuPanel : MonoBehaviour
    {
        private Menunavigation _menuNavigation;
        [SerializeField] private Button _backButton;
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private List<GameObject> _playerPanelList;
        public static int PlayerNumber;
        

        private void Awake()
        {
            _menuNavigation = new Menunavigation();
            _menuNavigation.UI.Cancel.performed += OnBack;
        }

        private void Start()
        { 
            if (_playerPanelList.Count != 0)
            {
                _playerPanelList[PlayerNumber-2].SetActive(true);
            }
           
        }

        public void SetSelectedButton(GameObject button)
        {
            _eventSystem.SetSelectedGameObject(button);
        }
        private void OnBack(InputAction.CallbackContext context)
        {
            if (_backButton != null)
            {
                _backButton.onClick?.Invoke();
            }

        }

        private void OnEnable()
        {
            _menuNavigation.UI.Enable();
        }

        private void OnDisable()
        {
            _menuNavigation.UI.Disable();
        }

        public void ModeChoice(int playerNumber)
        {
            PlayerNumber = playerNumber;
        }
        
        
        
        
    }
}
