using System;
using System.Collections.Generic;
using Michael.Scripts.PlayerManager;
using UnityEngine;
using UnityEngine.Serialization;

public class DataModifier : MonoBehaviour {

    [SerializeField] private AudioSource _battleMusic;
    [SerializeField] private AudioSource _backgroundMusic;
    
    
    [ContextMenu("Modify")]
    public void ChangeMusic() 
    {
       // DataManager.Instance.BackgroundMusic = _battleMusic;
    }

    public void ModifyData()
    {
        
    }

    private void Start()
    {
       
    }
}