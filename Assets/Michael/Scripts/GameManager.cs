using System.Collections.Generic;
using Christopher.Proto.Scripts;
using Michael.Fred;
using Michael.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public static int CurrentRound;
    public List<PlayerData> PlayerList;
    public List<GameObject> PlayerAlive;
    public int RoundDuration = 120; // 2 minutes le round
    private float _timer;
    [SerializeField] private TextMeshProUGUI _timerText;
    public bool RoundIsFinished;
    public int RoundTarget = 2;
    public PlayerData Winner;
    public AudioClip MusicToload;
    public Animator FadeAnimator;
    public GameObject EndGamePanel;
    public GameObject EventSystem;
    public GameObject RestartButton;
    public Animator EndPanelAnimator;
    public TextMeshProUGUI PlayerName;
    public TextMeshProUGUI PlayerNumber;
    [SerializeField] private GameObject EndWinnerVisual;
    [SerializeField] private AudioSource CrowndReactSound;
    public GameObject DeathLazer;
    [SerializeField] private GameObject OverTimePanel;
    [SerializeField]private bool TutoIsFinished = false; 
    public AudioSource ChainSound;
    public AudioSource HornSound;
    public void QuitApplication()
    {
        Application.Quit();
    }
    
    private void Start()
    {
        TutoIsFinished = false;
        //FadeAnimator.SetTrigger("FadeOut");
       //StartRound();
        CurrentRound = 1;
        
       //ChangeMusic(MusicToload);
      DataManager.Instance.StopMusic();

    }

    public void FinishTutorial()
    {
        if (TutoIsFinished == false)
        {
            StartRound();
            TutoIsFinished = true;
        }
       
    }
    
    public void ChangeMusic(AudioClip musicToLoad) 
    { 
        DataManager.Instance.CurrentMusic.clip = musicToLoad;
        DataManager.Instance.CurrentMusic.Play();
    }

    private void Update()
    {
        if (_timer > 0 ) {
            _timer -= TimeManager.Instance.deltaTime;
            UpdateTimerText();
        }
        else  {
            _timer = 0;
            _timerText.text = "00:00";
        }
      
        
        if (!RoundIsFinished &&  DetermineRoundWinner() != null) {
            EndRound();
        }
        if (_timer <= 0 && TutoIsFinished == true)
        {
            HornSound.Play();
           OverTimePanel.SetActive(true);
           ChainSound.Play();
           
          //  DeathLazer.SetActive(true);
        }
        else
        {
            OverTimePanel.SetActive(false);
            ChainSound.Stop();
          
          //DeathLazer.SetActive(false);
           
        }
        
        
    }
    public void StartRound()
    {
        
        CountDownController.Instance.RoundAnimator.SetBool("ShowRoundPanel ", true);
        RoundIsFinished = false;
        foreach (var player in PlayerList)
        {
            player.ResetHealth();
            player.GetComponent<PlayerControler>().ResetBall();
            player.GetComponent<PlayerControler>().HandedBall = true;
            
            //respawn des joueurs 
            if (CurrentRound > 1)
            {
                player.transform.position = player.GetComponent<PlayerData>().InitialPosition;
            }
        }
        
        PlayerAlive.Clear();
        foreach (var player in PlayerList)
        {
            PlayerAlive.Add(player.gameObject);
            player.gameObject.SetActive(true);
        }
        
       
        CountDownController.Instance.CountDownTime = 3;
        _timer = RoundDuration;
        // //start compte a rebours
        CountDownController.Instance.InvokeRepeating("UpdateCountdown",0f,1f);
        
      
        
        //lancer le timer 
        
        
        //
        //lancer animation transition fondu
        //replacer les players 
        //mettre a jour l'ui
    }

    

    public void EndRound()
    {
        DeathLazer.SetActive(false);
        PlayerData winner = DetermineRoundWinner();
        CountDownController.CanPlay = false;
        winner.WinRound++;
        RoundIsFinished = true;
        
        if (winner.WinRound < RoundTarget)
        {
            FadeAnimator.SetTrigger("FadeOut");
            CurrentRound++;
            Invoke("StartRound",1.9f);
        }
        else if (winner.WinRound >= RoundTarget)
        {
            
         // FadeAnimator.SetTrigger("FadeOut");
          Invoke("EndGame",3f);

          PlayerName.text = winner.PlayerPseudo;
              PlayerNumber.text = "Joueur " + winner.Playerindex;
              EndWinnerVisual.GetComponent<Image>().sprite = winner.EndGameVisual;

        }
       
    }
    
    public void EndGame()
    {
        foreach (var player in PlayerList)
        {
            player.GetComponent<PlayerControler>().ResetBall();
        }

        CrowndReactSound.Play();
        CountDownController.CanPlay = false;
        EndGamePanel.SetActive(true);
        EventSystem.SetActive(true);
        EventSystem.GetComponent<EventSystem>().SetSelectedGameObject(RestartButton);
        //transition fondu 
        //affichage dun podiuim avec les joueurs
       
        
    }

    PlayerData DetermineRoundWinner()
    {
        PlayerData lastAlive = null;
        
        if (PlayerAlive.Count == 1)// si il ne reste plus q'un joueur
        {
            lastAlive = PlayerAlive[0].gameObject.GetComponent<PlayerData>();
        }

        return lastAlive;

    }
    
    void UpdateTimerText()
    {
        if (CountDownController.CanPlay)
        {
            int minutes = Mathf.FloorToInt(_timer / 60);
            int seconds = Mathf.FloorToInt(_timer % 60);
        
            _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
       
    }

    public void ChangeEndPanel()
    {
        EndPanelAnimator.SetBool("Continue",true);
    }
}
   

    
        
    
    
    














