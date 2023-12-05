using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action<GameObject> AddPlayerList;
    public List<GameObject> PlayerList;
    public bool GameOver = true;
    
    [SerializeField] private TMPro.TextMeshProUGUI timerDisplay;
    [SerializeField] private float timerBegin = 3f;

    private int _roundNumber;
    private bool _isTimerBegin;
    private bool _isTimerRound;
    private float _currentTimer;
    private bool _SuddenDeath;
    // Start is called before the first frame update
    void Start()
    {
        AddPlayerList += AddInPlayerList;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddInPlayerList(GameObject player)
    {
        PlayerList.Add(player);
        for (int i = 0; i < PlayerList.Count; i++)
        {
            if (PlayerList[i] == player)
            {
                player.transform.GetComponent<PlayerControler>().PlayerNumber = i+1;
            }
        }
        
    }

    private void RoundManager()
    {
        if (PlayerList.Count >= 2 && _roundNumber == 0)
        {
            _isTimerBegin = true;
            _roundNumber = 1;
        }
        
    }
    private void TimerManager()
    {
        if (_isTimerBegin)
        {
            _currentTimer = timerBegin;
            _isTimerBegin = false;
        }
        if (_currentTimer <= 0)
        {
            _isTimerRound = true;
        }
    }
}
