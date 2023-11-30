using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

public class PlayerControler : MonoBehaviour
{
    public string CurrentColor;
    [SerializeField] private InputActionReference Move, Throw;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotSpeed;
    //[SerializeField] private float rotSpeed;
    private string[] _colorList = new string[] { "bleu", "rouge" };
    private Transform _pivot;
    private Transform _visé;
    private Quaternion _ThrowOrientation;
    private Deplacement _playerDeplacement;
    private Throw _playerThrow;
    private Rotation _playerRotation;
    private float _currentSpeed;
    private Vector2 _mouvementValue;
    private bool _handedBall = true;
    private bool _throw = false;

    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        int randomColorIndex = Random.Range(0, 1);
        CurrentColor = _colorList[randomColorIndex];
        _playerDeplacement = transform.GetComponent<Deplacement>();
        _playerThrow = transform.GetComponent<Throw>();
        _currentSpeed = moveSpeed;
        _pivot = transform.GetChild(0).transform;
        _visé = _pivot.GetChild(0).transform;
        _playerThrow.Orientation = _visé;
        _rb = transform.GetComponent<Rigidbody2D>();

    }

    public void OnMove()
    {
        _mouvementValue = Move.action.ReadValue<Vector2>(); 
    }
    public void OnThrow()
    {
        //_throw = Throw.
       _throw = Throw.action.triggered; 
    }
    private void FixedUpdate()
    {
        float tmpX = Mathf.Clamp(_visé.position.x, transform.position.x-1f,transform.position.x+ 1f);
        float tmpY = Mathf.Clamp(_visé.position.y, transform.position.y-1f,transform.position.y+ 1f);
        _visé.position = new Vector3(tmpX, tmpY, _visé.position.z);
    }

    // Update is called once per frame
    private void Update()
    {
        if(CurrentColor == "bleu")transform.GetComponent<Renderer>().material.color = Color.blue;
        if(CurrentColor == "rouge")transform.GetComponent<Renderer>().material.color = Color.red;
        
        _playerDeplacement.PerformMovement(_mouvementValue,_currentSpeed);
        float movementMag = Mathf.Clamp01(_mouvementValue.magnitude);
        
        //visé en mode gros dégueulasse
        if (_mouvementValue.x > 0.1f && _mouvementValue.y > 0.1f) //diag haut droit
        {
            _visé.position = new Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z);
        } 
        if (_mouvementValue.x < -0.1f && _mouvementValue.y < -0.1f)//diag bas gauche
            _visé.position = new Vector3(transform.position.x - 1, transform.position.y - 1, transform.position.z);
        if (_mouvementValue.x < -0.1f && _mouvementValue.y > 0.1f)//diag haut gauche
            _visé.position = new Vector3(transform.position.x - 1, transform.position.y + 1, transform.position.z);
        if (_mouvementValue.x > 0.1f && _mouvementValue.y < -0.1f)//diag bas droit
            _visé.position = new Vector3(transform.position.x + 1, transform.position.y - 1, transform.position.z);
        if(_mouvementValue == Vector2.zero)// par défaut : droit
            _visé.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        if (_mouvementValue.y < 0.1 && _mouvementValue.y > -0.1 && _mouvementValue.x > 0.1f )//droit
            _visé.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        if (_mouvementValue.y < 0.1 && _mouvementValue.y > -0.1 && _mouvementValue.x < -0.1f )//gauche
            _visé.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        if (_mouvementValue.x < 0.1 && _mouvementValue.x > -0.1 && _mouvementValue.y > 0.1f)//haut
            _visé.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        if (_mouvementValue.x < 0.1 && _mouvementValue.x > -0.1 && _mouvementValue.y < -0.1f)//bas
            _visé.position = new Vector3(transform.position.x , transform.position.y - 1, transform.position.z);
       /* 
       if (_mouvementValue != Vector2.zero )
       {
           _visé.Translate(_mouvementValue * _currentSpeed * movementMag * Time.deltaTime);
       }
       else
       {
           _visé.position = transform.position;
       }
       */
       
        if (_throw && _handedBall)
        {
            _playerThrow.PerformThrow(_mouvementValue);
            _handedBall = false;
        }
        //_playerRotation.LookAt();
    }

    
/*
    private void OnEnable(InputAction.CallbackContext callbackContext)
    {
        Throw.action.performed += _playerThrow.PerformThrow;
    }

    private void OnDisable(InputAction.CallbackContext callbackContext)
    {
        Throw.action.performed -= _playerThrow.PerformThrow;
    }*/
}
