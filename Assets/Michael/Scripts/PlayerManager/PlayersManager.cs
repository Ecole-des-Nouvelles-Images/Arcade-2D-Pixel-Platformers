using System;
using System.Collections.Generic;
using System.Linq;
using Michael.Fred;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace Michael.Scripts.PlayerManager
{
    public class PlayersManager : MonoBehaviour

    {
        public static bool[] PlayerIsReady;
        public static bool[] PlayerIsJoined;
        public static bool CanStart;
        public int PlayerIndex = 1;
        public int _maxPlayers;
        public GameObject PlayerPanel;
        [SerializeField] private GameObject _selectionPanel;
        [SerializeField] private GameObject _joinPanel;
        [SerializeField] private GameObject _joinButton;
        [SerializeField] private GameObject _readyButton;
        [SerializeField] private GameObject _readyText;
        [SerializeField] private GameObject _choiceModePanel;
        [SerializeField] private GameObject _playerPanel;
        [SerializeField] private TextMeshProUGUI _playerName;
        [SerializeField] private TextMeshProUGUI _playerNumber;

        [Header("character selection")] [SerializeField]
        private List<Sprite> _characterSpriteslist;

        [SerializeField] private GameObject _currentCharacter;
        [SerializeField] private int _characterIndex = 0;
        [SerializeField] private TextMeshProUGUI _characterBio;
        public Image CharacterImage;
       
        
        private void Start()
        {
            CanStart = false;
            PlayerIsReady = new bool[4] { false, false, false, false };
            PlayerIsJoined = new bool[4] { false, false, false, false };

            _playerNumber.text = "P" + PlayerIndex;
        }

        private void Update()
        {
           // ChangeCharacterBio();
        }

        public void PlayerJoined()
        {
            PlayerIndex = GetComponent<PlayerInput>().playerIndex +1;
            PlayerIsJoined[PlayerIndex - 1] = true;
            Debug.Log("Player " + PlayerIndex + " joined");
        }
        
        public void PlayerReady()
        {
            PlayerIsReady[PlayerIndex - 1] = true;
            bool allPlayersReady = true;
            int readyCount = 0;
            int Maxplayer = _maxPlayers;

            for (int i = 0; i < PlayerIsJoined.Length; i++)
            {
                if (PlayerIsJoined[i] == true)
                {
                    if (PlayerIsReady[i] == false)
                    {
                        allPlayersReady = false;
                    }
                    else
                    {
                        readyCount++;
                        GetComponent<MultiplayerEventSystem>().SetSelectedGameObject(null);
                        Debug.Log("player " + PlayerIndex + " Is Ready");
                        Debug.Log("index is " + (GetComponent<PlayerInput>().playerIndex + 1));
                        ConfirmChoice(PlayerIndex, _characterIndex);
                        
                    }
                }

            }
            if (allPlayersReady == true && readyCount > _maxPlayers)
            {
                CanStart = true;
                
                /*SceneManager.LoadScene("Game", LoadSceneMode.Additive);
                SceneManager.UnloadSceneAsync("Character Selection");
                Debug.Log("2 players ready minimum");
                string logMessage = "Contenu du dictionnaire : \n";
                foreach (var pair in DataManager.Instance.PlayerDatasDict)
                {
                    logMessage += $"Clé: {pair.Key}, Valeur: {pair.Value}\n";
                }
                Debug.Log( logMessage);*/
            }
            else
            {
                CanStart = false;
            }
        }



        public void OnStartPause()
        {
            if (CanStart)
            {
                CustomSceneManager.Instance.LoadScene("Game");
                Debug.Log("ledqgfqeggoooo");
            }
            else
            {
                Debug.Log("pas pret");
            }
           
        }
        
        public void OnCancel()
        {
            if (PlayerIsReady[PlayerIndex - 1] == true)
            {

                PlayerIsReady[PlayerIndex - 1] = false;
                GetComponent<MultiplayerEventSystem>().SetSelectedGameObject(_readyButton);
                _readyButton.SetActive(true);
                _readyText.SetActive(false);
                Debug.Log("Cancel Ready");
                Debug.Log(PlayerIsReady[PlayerIndex - 1]);
            }
            else if (PlayerIsJoined[PlayerIndex - 1] == true)
            {

                _selectionPanel.SetActive(false);
                _joinPanel.SetActive(true);
                GetComponent<MultiplayerEventSystem>().SetSelectedGameObject(_joinButton);
                PlayerIsJoined[PlayerIndex - 1] = false;
                Debug.Log("Cancel Join");
            }
            else if (PlayerIsJoined.All(element => !element))
            {
               CustomSceneManager.Instance.LoadScene("Menu");
            }

        }
        
        public void OnDeviceLost()
        {
            PlayerIsReady[PlayerIndex - 1] = false;
            _readyButton.SetActive(true);
            _readyText.SetActive(false);
            _selectionPanel.SetActive(false);
            _joinPanel.SetActive(true);
            GetComponent<MultiplayerEventSystem>().SetSelectedGameObject(_joinButton);
            PlayerIsJoined[PlayerIndex - 1] = false;
            RemoveChoice(PlayerIndex);
            
            Debug.Log("deconnect");
         //   _playerPanel.SetActive(false);
        }

        public void NextCharacter()
        {
            _characterIndex += 1;
            if (_characterIndex == _characterSpriteslist.Count)
            {
                _characterIndex = 0;
            }

            CharacterImage.sprite = _characterSpriteslist[_characterIndex];
            ChangeCharacterBio();
        }

        public void PreviousCharacter()
        {
            _characterIndex -= 1;
            if (_characterIndex < 0)
            {
                _characterIndex = _characterSpriteslist.Count - 1;
            }

            CharacterImage.sprite = _characterSpriteslist[_characterIndex];
            ChangeCharacterBio();
        }

        public void ConfirmChoice(int playerIndex, int skinIndex)
        {
            
            if (DataManager.Instance.PlayerDatasDict.ContainsKey(playerIndex))
            {
                DataManager.Instance.PlayerDatasDict[playerIndex] = skinIndex;
            }
            else
            {
                DataManager.Instance.PlayerDatasDict.Add(playerIndex, skinIndex);
            }
            
            
        }
        public void RemoveChoice(int playerIndex)
        {
            DataManager.Instance.PlayerDatasDict.Remove(playerIndex);
        }
        
    
        public void ChangeCharacterBio()
        {
             if(_characterIndex == 0)
             {
                 _playerName.text = "maximadok"; 
             }
             else if (_characterIndex == 1)
             {
                 _playerName.text = "mathisruk"; 
             }
             else if (_characterIndex == 2)
             {
                 _playerName.text = "Christorar"; 
             }
             else if (_characterIndex == 3)
             {
                 _playerName.text = "Mikaralik"; 
             }
               
                 
                 
                
            
        }

       
        
        
        
        
        
        
        
        
        
    }
}
