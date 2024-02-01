using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Michael.Fred
{
    
    public class DataManager : MonoBehaviourSingleton<DataManager> {

        public string StartingScene;
        public AudioSource CurrentMusic;
        public AudioSource AmbientMusic;
        public Dictionary<int, int> PlayerDatasDict = new Dictionary<int, int>();
        public AudioMixer AudioMixer;
        public float SfxVolume;
        public float MusicVolume;
        public bool MenuMusicIsStop = true;

      

        public void StopMusic()
        {
            if (CurrentMusic)
            {
                CurrentMusic.Stop();
                AmbientMusic.Stop();
                MenuMusicIsStop = true;
            }
         
        }
        
        public void playMusic()
        {
            if (MenuMusicIsStop)
            {
                CurrentMusic.Play();
                AmbientMusic.Play();
                MenuMusicIsStop = false;
            }
           
        }
       


       



    }
}