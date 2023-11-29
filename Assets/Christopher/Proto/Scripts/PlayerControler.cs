using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private InputActionReference Move, Throw;
    [SerializeField] private float moveSpeed;
    //[SerializeField] private float rotSpeed;
    private Transform _visé;
    private Quaternion _ThrowOrientation;
    private Deplacement _playerDeplacement;
    private Throw _playerThrow;
    private float _currentSpeed;
    private Vector2 _mouvementValue;
    // Start is called before the first frame update
    void Start()
    {
        _playerDeplacement = transform.GetComponent<Deplacement>();
        _playerThrow = transform.GetComponent<Throw>();
        _currentSpeed = moveSpeed;
        _visé = transform.GetChild(0).transform;
        _playerThrow.Orientation = _mouvementValue;
    }

    // Update is called once per frame
    void Update()
    {
        _mouvementValue = Move.action.ReadValue<Vector2>();
        _playerDeplacement.PerformMovement(_mouvementValue,_currentSpeed);
        //Vector3 orientation = new Vector3()
        _visé.Rotate(_mouvementValue*_currentSpeed*Time.deltaTime);
        //_playerThrow.Orientation = _mouvementValue;
    }

    private void OnEnable()
    {
        Throw.action.performed += _playerThrow.PerformThrow;
    }

    private void OnDisable()
    {
        Throw.action.performed -= _playerThrow.PerformThrow;
    }
}
