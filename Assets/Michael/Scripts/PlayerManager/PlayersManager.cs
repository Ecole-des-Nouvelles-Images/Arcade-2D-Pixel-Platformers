using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Michael.Scripts.PlayerManager
{
    public class PlayersManager : MonoBehaviour

    {
        
        [Header("input management")]
        public static bool[] PlayerIsReady = new bool[4]{ false, false, false, false};
        public static bool[] PlayersJoined = new bool[4] { false, false, false, false};
      
        public int PlayerIndex = 1;
        public GameObject PlayerPanel;
        [SerializeField] private GameObject _selectionPanel;
        [SerializeField] private GameObject _joinPanel;
        [SerializeField] private GameObject _joinButton;
        [SerializeField] private GameObject _readyButton;
        [SerializeField] private GameObject _readyText;

        [SerializeField] DataModifier _dataModifier;
        
       [Header("character selection")]
        [SerializeField] private List<Sprite> _characterSpriteslist;
        [SerializeField] private GameObject _currentCharacter;
        [SerializeField] private int _characterIndex = 0;
        [SerializeField] private TextMeshProUGUI _characterBio;
        public Image CharacterImage;

        public UnityEvent OnOpenMenu;

        private void Start()
        { 
            OnOpenMenu.Invoke();
        }
        
        

        public void PlayerJoined()
        {
            PlayersJoined[PlayerIndex - 1] = true; 
            Debug.Log("Player " + PlayerIndex + " joined");
            
        }
        
        

        public void PlayerReady()
        {
            PlayerIsReady[PlayerIndex - 1] = true;
            bool allPlayersReady = true;
            int readyCount = 0;
            
            for (int i = 0; i < PlayersJoined.Length; i++)
            {
                if (PlayersJoined[i] == true )
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
                        Debug.Log(readyCount);
                    }
                }
                
            }
            if (allPlayersReady == true && readyCount > 1 )
            {
                SceneManager.LoadScene("Prototype game", LoadSceneMode.Additive);
                SceneManager.UnloadSceneAsync("CharacterSelection");
                
                Debug.Log("2 players ready minimum");
            }
        }


        public void OnCancel()
        {
            if (PlayerIsReady[PlayerIndex - 1 ] == true)
            {
               
                PlayerIsReady[PlayerIndex - 1] = false;
                GetComponent<MultiplayerEventSystem>().SetSelectedGameObject(_readyButton);
                _readyButton.SetActive(true);
                _readyText.SetActive(false);
                Debug.Log("Cancel Ready");
                Debug.Log(PlayerIsReady[PlayerIndex-1]);
                
            }
            else if (PlayersJoined[PlayerIndex - 1] == true)
            {
                
                _selectionPanel.SetActive(false);
                _joinPanel.SetActive(true);
                GetComponent<MultiplayerEventSystem>().SetSelectedGameObject(_joinButton);

                PlayersJoined[PlayerIndex - 1] = false;
                Debug.Log("Cancel Join");
            }
            else if(PlayerIndex == 1)
            {
                SceneManager.LoadScene("Proto Menu", LoadSceneMode.Additive);
                SceneManager.UnloadSceneAsync("CharacterSelection");
            }
            
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

        public void ChangeCharacterBio()
        {
            
            
        }
        
        
        
        
        
        
        
        
    }
}
