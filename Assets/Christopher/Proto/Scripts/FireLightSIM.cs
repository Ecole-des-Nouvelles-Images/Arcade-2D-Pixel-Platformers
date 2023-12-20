using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireLightSIM : MonoBehaviour
{
    [SerializeField] private Light2D fireLight2D;
    [SerializeField] private float timerChangingIntensityLight_min;
    [SerializeField] private float timerChangingIntensityLight_max;
    [SerializeField] private float IntensityLight_min;
    [SerializeField] private float IntensityLight_max;

    private float _currentTimerValue;
    // Start is called before the first frame update
    void Start()
    {
        _currentTimerValue = Random.Range(timerChangingIntensityLight_min, timerChangingIntensityLight_max);
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentTimerValue <= 0)
        {
            fireLight2D.volumeIntensity = Random.Range(IntensityLight_min, IntensityLight_max);
            _currentTimerValue = Random.Range(timerChangingIntensityLight_min, timerChangingIntensityLight_max);
        }
        else
        {
            _currentTimerValue -= Time.deltaTime;
        }
    }
}
