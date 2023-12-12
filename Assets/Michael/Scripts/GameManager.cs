using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Michael.Fred;
using Michael.Scripts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public static int CurrentRound;
    public List<PlayerData> PlayerList;
    public List<GameObject> PlayerAlive;
    [SerializeField] public UnityEvent ChangeMusic;
    public int RoundDuration = 120; // 2 minutes le round
    private float _timer;
    public bool RoundIsFinished;
    public int RoundTarget = 2;

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
        StartRound();
    }

    private void Update()
    {
        _timer = Time.deltaTime;
        

        if (!RoundIsFinished && (_timer <= 0 || DetermineRoundWinner() != null))
        {
            EndRound();
            RoundIsFinished = true;
        }
    }


    public void StartRound()
    {
        RoundIsFinished = false;
        foreach (var player in PlayerList)
        {
            player.ResetHealth();
        }
        
        Debug.Log("nouveau round : " + CurrentRound);
        _timer = RoundDuration;
        //
        //start compte a rebours
        //lancer animation transition fondu
        //replacer les players 
        //mettre a jour l'ui
    }


    public void EndRound()
    {
        
        PlayerData winner = DetermineRoundWinner();

        if (winner!= null && !RoundIsFinished)
        {
            winner.WinRound++;
            
            
            Debug.Log(winner.WinRound);
        }
        if (winner.WinRound >= RoundTarget)
        {
            EndGame();
            Debug.Log(winner + " à gagné");
        }
        else
        {
            CurrentRound++;
            StartRound();
            PlayerAlive.Clear();
            foreach (var player in PlayerList)
            {
                PlayerAlive.Add(player.gameObject);
                player.gameObject.SetActive(true);
            }
        }

        
    }
    
    public void EndGame()
    {
        
        //transition fondu 
        //affichage dun podiuim avec les joueurs
        //affichage menu pause
        
    }

   

    PlayerData DetermineRoundWinner()
    {
        PlayerData lastPlayeralive = null;
        
        if (PlayerAlive.Count <= 1)// si il ne reste plus q'un joueur
        {
            lastPlayeralive = PlayerAlive[0].gameObject.GetComponent<PlayerData>();
        }

        return lastPlayeralive;

    }
    
    


}











