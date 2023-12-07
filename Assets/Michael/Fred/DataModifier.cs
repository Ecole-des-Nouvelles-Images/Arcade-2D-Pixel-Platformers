using System;
using System.Collections.Generic;
using Michael.Scripts.PlayerManager;
using UnityEngine;

public class DataModifier : MonoBehaviour {

    [SerializeField] private AudioSource _OtherMusic;
    [SerializeField] private AudioSource _backgroundMusic;
    
    [ContextMenu("Modify")]
    public void ChangeMusic() 
    {
        //DataManager.Instance.BackgroundMusic = _OtherMusic;
    }

    private void Start()
    {
       // _backgroundMusic = DataManager.Instance.BackgroundMusic;
    }
}