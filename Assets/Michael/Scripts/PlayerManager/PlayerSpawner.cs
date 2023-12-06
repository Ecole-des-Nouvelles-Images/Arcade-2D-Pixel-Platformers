using System;
using System.Collections;
using System.Collections.Generic;
using Michael.Scripts.PlayerManager;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public List<Transform> Spawnpoint;
    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject player = Instantiate(playerPrefab) as GameObject;

            player.transform.position = Spawnpoint[i + 1].position;
            player.transform.rotation = Spawnpoint[i + 1].rotation;
                
            if (PlayersManager.PlayerIsReady[i] == false)
            {
                foreach (Transform transform in player.transform)
                {
                    Destroy(transform.gameObject);
                    player.GetComponent<PlayerControler>().enabled = false; 
                }
             
            }
        }

        
    }
}
