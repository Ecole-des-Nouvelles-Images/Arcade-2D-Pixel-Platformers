using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Christopher.Proto.Scripts;
using Michael.Fred;
using Michael.Scripts;
using TMPro;
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
    [SerializeField] private TextMeshProUGUI _timerText;
    public bool RoundIsFinished;
    public int RoundTarget = 2;
    public PlayerData Winner;
    public GameObject DeathLazer;
    

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
        StartRound();
        CurrentRound = 1;
        ChangeMusic.Invoke();
        
    }

    private void Update()
    {
        if (_timer > 0 && !PauseControl.IsPaused) {
            _timer -= Time.deltaTime;
            UpdateTimerText();
        }
        else if (!PauseControl.IsPaused){
            _timer = 0;
            _timerText.text = "00:00";
        }
      
        
        if (!RoundIsFinished &&  DetermineRoundWinner() != null) {
            Debug.Log("round terminé");
            EndRound();
        }
        if (_timer <= 0) {
          DeathLazer.SetActive(true); 
          //transition tete de mort 
        }
        else
        {
            DeathLazer.SetActive(false);
        }
        
        
    }
    public void StartRound()
    {
        
        RoundIsFinished = false;
        foreach (var player in PlayerList)
        {
            player.ResetHealth();
            player.GetComponent<PlayerControler>().ResetBall();
            player.GetComponent<PlayerControler>().HandedBall = true;
          
        }
        
        PlayerAlive.Clear();
        foreach (var player in PlayerList)
        {
            PlayerAlive.Add(player.gameObject);
            player.gameObject.SetActive(true);
        }
        _timer = RoundDuration;
        // //start compte a rebours
        CountDownController.Instance.CountDownTime = 5;
        CountDownController.Instance.InvokeRepeating("UpdateCountdown",0f,1f);
        //lancer le timer 
       
        Debug.Log("nouveau round : " + CurrentRound);
       
        //
        //lancer animation transition fondu
        //replacer les players 
        //mettre a jour l'ui
    }


    public void EndRound()
    {

        PlayerData winner = DetermineRoundWinner();

        winner.WinRound++;
        Debug.Log("le gagnant à gagné " + winner.WinRound + " round");
        RoundIsFinished = true;
        
        if (winner.WinRound < RoundTarget)
        {
            CurrentRound++;
            StartRound();
        }
        else if (winner.WinRound >= RoundTarget)
        {
            EndGame();
            Debug.Log(winner + " à gagné");
        }
        
        
           
        
       
       

        
    }
    
    public void EndGame()
    {
        Debug.Log("partie fini, gagnant ");
        //transition fondu 
        //affichage dun podiuim avec les joueurs
        //affichage menu pause
        
    }

    PlayerData DetermineRoundWinner()
    {
        PlayerData lastAlive = null;
        
        if (PlayerAlive.Count <= 1)// si il ne reste plus q'un joueur
        {
            lastAlive = PlayerAlive[0].gameObject.GetComponent<PlayerData>();
        }

        return lastAlive;

    }
    
    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(_timer / 60);
        int seconds = Mathf.FloorToInt(_timer % 60);
        
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
   

    
        
    
    
    














