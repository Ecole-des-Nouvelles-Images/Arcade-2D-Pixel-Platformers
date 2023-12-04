using System;
using System.Collections;
using System.Collections.Generic;
using Michael.Scripts;
using UnityEngine;
[RequireComponent(typeof(Canvas))]
public class MenuPanel : MonoBehaviour
{
    [SerializeField] private PanelType _type;
    private bool _state;


    private void Awake()
    {
        
    }

    private void UpdateState()
    {
        gameObject.SetActive(_state);
    }

    private void ChangeState()
    {
        _state = !_state;
        UpdateState();
    }

    private void ChangeState(bool state)
    {
        _state = state;
        UpdateState();
    }
}
