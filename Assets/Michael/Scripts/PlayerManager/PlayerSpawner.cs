using System;
using System.Collections;
using System.Collections.Generic;
using Michael.Fred;
using Michael.Scripts.PlayerManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public List<Sprite> CharacterSpriteList;
    public List<Transform> Spawnpoint;

    private void Start()
    {

        Debug.Log(PlayersManager.PlayerIsReady[0]);
        Debug.Log(PlayersManager.PlayerIsReady[1]);
        Debug.Log(PlayersManager.PlayerIsReady[2]);
        Debug.Log(PlayersManager.PlayerIsReady[3]);
        for (int i = 0; i < 4; i++)
        {
            GameObject player = Instantiate(playerPrefab)  as GameObject;
            OnPlayerJoined(player.GetComponent<PlayerInput>());
          
            player.transform.position = Spawnpoint[i].position;

            if (PlayersManager.PlayerIsReady[i] == false)
            {
                foreach (Transform transform in player.transform)
                {
                    Destroy(player.gameObject);
                    player.GetComponent<PlayerControler>().enabled = false;
                }

            }
           
        }
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
       playerInput.gameObject.GetComponent<Michael.Scripts.PlayerData>().Playerindex = playerInput.playerIndex+1;
        Debug.Log("index : " + playerInput.gameObject.GetComponent<Michael.Scripts.PlayerData>().Playerindex);
        
    }

   
    

    
}
