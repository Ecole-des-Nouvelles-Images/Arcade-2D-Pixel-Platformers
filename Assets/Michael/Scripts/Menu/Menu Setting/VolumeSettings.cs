using System;
using System.Collections;
using System.Collections.Generic;
using Michael.Fred;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class VolumeSettings : MonoBehaviour
{
  
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;
    
    private void Start()
    {
        _musicSlider.value = DataManager.Instance.MusicVolume;
        _sfxSlider.value =  DataManager.Instance.SfxVolume;
        SetMusicVolume();
        SetSfxVolume();

       
    }

    public void SetMusicVolume()
    {
        DataManager.Instance.MusicVolume =  _musicSlider.value;
        _mixer.SetFloat("Music",Mathf.Log10( DataManager.Instance.MusicVolume)*20);
    }
    
    public void SetSfxVolume()
    {
        DataManager.Instance.SfxVolume = _sfxSlider.value;
        _mixer.SetFloat("Sfx",Mathf.Log10( DataManager.Instance.SfxVolume)*20);
      
    }
    
}
