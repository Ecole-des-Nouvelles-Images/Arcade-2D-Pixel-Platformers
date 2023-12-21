using System.Collections.Generic;
using Christopher.Proto.Scripts;
using Michael.Scripts.PlayerManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public List<Sprite> CharacterSpriteList;
    public List<Transform> Spawnpoint;
    public List<GameObject> PlayerUiList; 
    
    private void Start()
    {
        
      
        for (int i = 0; i < 4; i++) 
        {
            GameObject player = Instantiate(playerPrefab, transform);
            OnPlayerJoined(player.GetComponent<PlayerInput>());
            player.transform.position = Spawnpoint[i].position;
            
            if (PlayersManager.PlayerIsReady[i] == false) {
                Destroy(player.gameObject);
                player.GetComponent<PlayerControler>().enabled = false;
            }
            else {
               GameManager.Instance.PlayerList.Add(player.GetComponent<Michael.Scripts.PlayerData>());
               GameManager.Instance.PlayerAlive.Add(player);
            }
        }
        
        PlayerUiList[GameManager.Instance.PlayerList.Count-2].SetActive(true);
    }

    public void OnPlayerJoined(PlayerInput playerInput) {
       playerInput.gameObject.GetComponent<Michael.Scripts.PlayerData>().Playerindex = playerInput.playerIndex+1;
        
    }

   
    

    
}
