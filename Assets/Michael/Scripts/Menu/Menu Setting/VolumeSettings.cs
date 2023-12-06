using System;
using System.Collections;
using System.Collections.Generic;
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
        SetMusicVolume();
    }

    public void SetMusicVolume()
    {
        float volume = _musicSlider.value;
        _mixer.SetFloat("Music",Mathf.Log10(volume)*20);
    }
    
    public void SetSfxVolume()
    {
        float volume = _sfxSlider.value;
        _mixer.SetFloat("Sfx",Mathf.Log10(volume)*20);
    }
    
}
