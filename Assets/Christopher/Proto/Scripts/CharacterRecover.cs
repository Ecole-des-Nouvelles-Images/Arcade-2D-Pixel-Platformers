using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRecover : MonoBehaviour
{
    public bool isRecovering;

    [SerializeField] private float recoveringTimer = 2f;

    private float _currentRecoveringTime;
    // Start is called before the first frame update
    void Start()
    {
        _currentRecoveringTime = recoveringTimer;
    }

    // Update is called once per frame
    void Update()
    {
        Recovering();
    }

    private void Recovering()
    {
        if (isRecovering && _currentRecoveringTime > 0)
        {
            _currentRecoveringTime -= TimeManager.Instance.deltaTime;
        }

        if (_currentRecoveringTime <= 0)
        {
            isRecovering = false;
            _currentRecoveringTime = recoveringTimer;
        }
    }
}
