using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    public static Action<GameObject> AddPlayerList;
    public static Action<GameObject> RemovePlayerList;
    public List<GameObject> PlayerList;
    public bool GameOver = true;
    
    [SerializeField] private RectTransform[] playersPanel = new RectTransform[4];
    [SerializeField] private float timerBegin = 3f;
    
    private int _roundNumber;
    private bool _gameStart;
    private bool _isTimerBegin;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < playersPanel.Length; i++)
        {
            playersPanel[i].GameObject().SetActive(false);
        }
        AddPlayerList += AddInPlayerList;
        RemovePlayerList += RemoveInPlayerList;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayHP();
    }

    private void AddInPlayerList(GameObject player)
    {
        PlayerList.Add(player);
        for (int i = 0; i < PlayerList.Count; i++)
        {
            if (PlayerList[i] == player)
            {
                player.transform.GetComponent<PlayerControlerV1>().PlayerNumber = i+1;
                playersPanel[i].GameObject().SetActive(true);
            }
        }
    }

    private void DisplayHP()
    {
        for (int i = 0; i < PlayerList.Count; i++)
        {
            playersPanel[i].GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = 
                "P" + i + 1 + ": hp_ " + PlayerList[i].transform.GetComponent<PlayerControlerV1>().Health;
            if (PlayerList[i] == null)
            {
                playersPanel[i].GameObject().SetActive(false);
            }
        }
    }
    private void RemoveInPlayerList(GameObject player)
    {
        for (int i = 0; i < PlayerList.Count; i++)
        {
            if (PlayerList[i] == player)
            {
                PlayerList.Remove(PlayerList[i]);
            }
        }
    }
    
    private void RoundManager()
    {
        if (PlayerList.Count >= 2 && _roundNumber == 0)
        {
            _gameStart = true;
            for (int i = 0; i < PlayerList.Count; i++)
            {
                PlayerList[i].transform.GetComponent<PlayerControlerV1>().HandedBall = true;
            }
            _isTimerBegin = true;
            _roundNumber = 1;
        }
    }
    
}
