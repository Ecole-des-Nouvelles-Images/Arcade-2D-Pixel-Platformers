using System;
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

        private void Awake()
        {
            _menuNavigation = new Menunavigation();
            _menuNavigation.UI.Cancel.performed += OnBack;
        }

        public void SetSelectedButton(GameObject button)
        {
            _eventSystem.SetSelectedGameObject(button);
        }
        private void OnBack(InputAction.CallbackContext context)
        {
            _backButton.onClick?.Invoke();
        }

        private void OnEnable()
        {
            _menuNavigation.UI.Enable();
        }

        private void OnDisable()
        {
            _menuNavigation.UI.Disable();
        }
        
        
        
        
    }
}
