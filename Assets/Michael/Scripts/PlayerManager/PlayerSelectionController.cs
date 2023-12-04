using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Michael.Scripts.PlayerManager
{
    public class PlayerSelectionController : MonoBehaviour
    {
        private int _playerIndex;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private GameObject _readyPanel;
        [SerializeField] private GameObject _selectionPanel;
        [SerializeField] private Button _readyButton;

        private float _ignoreInputTime = 1.5f;
        private bool _inputEnabled;

        public void SetPlayerIndex(int pi)
        {
            _playerIndex = pi;
            _titleText.SetText("Joueur" + (pi+1).ToString());
            _ignoreInputTime = Time.time + _ignoreInputTime; 
        }

        private void Update()
        {
            if (Time.time > _ignoreInputTime)
            {
                _inputEnabled = true;
            }
        }

        public void SetColor(Material color)
        {
            if (!_inputEnabled) { return; }
            PlayerConfigsManager.Instance.SetPlayerColor(_playerIndex,color);
            _readyPanel.SetActive(true);
            _readyButton.Select();
            _selectionPanel.SetActive(false);
        }


        public void ReadyPlayer()
        {
            if(!_inputEnabled) { return;}
            PlayerConfigsManager.Instance.PlayerIsReady(_playerIndex);
            _readyButton.gameObject.SetActive(false);
            
        }
        
        
    }
}
