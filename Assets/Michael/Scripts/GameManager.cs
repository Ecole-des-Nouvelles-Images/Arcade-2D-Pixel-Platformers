using System;
using System.Collections;
using System.Collections.Generic;
using Michael.Fred;
using Michael.Scripts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public static int CurrentRound;
    public List<PlayerData> PlayerList;
    [SerializeField] public UnityEvent ChangeMusic;
    public int RoundDuration = 120; // 2 minutes le round
    private float _timer;

    public void QuitApplication()
    {
        Application.Quit();
        Debug.Log("à quitté le jeu");
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    private void Start()
    {
        ChangeMusic.Invoke();
        CurrentRound = 1;

    }

    private void Update()
    {
        _timer = Time.deltaTime;

        if (_timer <= 0 || DetermineRoundWinner() != null)
        {
            EndRound();
        }

        

        

    }


    public void StartRound()
    {
        //respawn
    }


    public void EndRound()
    {
        PlayerData winner = DetermineRoundWinner();

        if (winner!= null)
        {
            winner.WinRound++;
            
            Debug.Log(winner.WinRound);
        }

        if (winner.WinRound >= 2)
        {
            EndGame();
        }
        else
        {
            CurrentRound++;
            StartRound();
        }
    }
    
    public void EndGame()
    {
        
    }

   

    PlayerData DetermineRoundWinner()
    {
        PlayerData lastPlayerAlive = null;
        
        foreach (PlayerData player in PlayerList)
        {
            if (player.IsAlive)
            {
                lastPlayerAlive = player;
            }
        }
        Debug.Log(lastPlayerAlive);
        return lastPlayerAlive;
      
    }

    


    PlayerData DetermineGameWinner()
    {
        PlayerData gameWinner = null;

        foreach (PlayerData player in PlayerList)
        {
            if (player.WinRound >= 2)
            {
                gameWinner = player;
            }
        }
        Debug.Log(gameWinner);
        return gameWinner;
       
    }


}











