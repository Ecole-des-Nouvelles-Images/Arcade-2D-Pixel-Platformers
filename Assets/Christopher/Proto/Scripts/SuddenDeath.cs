using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuddenDeath : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI timerDisplay;
    [SerializeField] private float timerMortSubite = 10f;
    [SerializeField] private GameObject lazerMortSubite;
    
    private bool _isTimerSuddenDeath;
    private float _currentTimer;
    private bool _SuddenDeath;
    // Start is called before the first frame update
    
    private void Start()
    {
        _currentTimer = timerMortSubite;
    }

    // Update is called once per frame
    private void Update()
    {
        TimerMortSubite();
        //timerDisplay.text = _currentTimer.ToString();
        timerDisplay.text = string.Format("{0:0}:{1:00}", Mathf.Floor(_currentTimer / 60), _currentTimer % 60);
    }
    private void TimerMortSubite()
    {
        
        if (_currentTimer > 0) _currentTimer -= TimeManager.Instance.deltaTime;
        if (_currentTimer < 0)
        {
            _currentTimer = 0;
            lazerMortSubite.SetActive(true);
        }
    }

    public void ResetTimerMortSubite()
    {
        _currentTimer = timerMortSubite;
    }
}
