using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviourSingleton<DataManager> {

    public string StartingScene;
    public string Data;
    public AudioSource BackgroundMusic;
    public Dictionary<int, PlayerData> PlayerDatasDict;
    public List<PlayerData> PlayerDatasList;
    
    
    private void Start()
    {
        SceneManager.LoadScene(StartingScene, LoadSceneMode.Additive);
    }
    
    
}