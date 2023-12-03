using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

public class PlayerControler : MonoBehaviour
{
    public int Health = 3;
    public string CurrentColor;
    [SerializeField] private InputActionReference Move, Throw, ChangeColor, ChangeSelect;
    [SerializeField] private float moveSpeed;
    private string[] _colorList = new string[] { "bleu", "rouge" };
    private Transform _pivot;
    private Transform _visé;
    private Quaternion _ThrowOrientation;
    private Deplacement _playerDeplacement;
    private Throw _playerThrow;
    private Rotation _playerRotation;
    private float _currentSpeed;
    private Vector2 _mouvementValue = Vector2.zero;
    [FormerlySerializedAs("_handedBall")] public bool HandedBall = true;
    private bool _throw = false;
    private bool _changeSelect = false;
    private bool _changeColor = false;
    private bool _armorSelected;
    private List<GameObject> _myBalls;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _playerRotation = transform.GetComponent<Rotation>();
        _playerDeplacement = transform.GetComponent<Deplacement>();
        _playerThrow = transform.GetComponent<Throw>();
        _playerThrow.Orientation = _visé;
        _pivot = transform.GetChild(0).transform;
        _visé = _pivot.GetChild(0).transform;
        int randomColorIndex = Random.Range(0, _colorList.Length);
        CurrentColor = _colorList[randomColorIndex];
    }

    // Start is called before the first frame update
    void Start()
    {
        
        _currentSpeed = moveSpeed;
        _rb = transform.GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext Move)
    {
        _mouvementValue = Move.action.ReadValue<Vector2>(); 
    }
    public void OnThrow(InputAction.CallbackContext Throw)
    {
        _throw = Throw.action.IsPressed();
    }
    public void OnChangeSelect(InputAction.CallbackContext ChangeSelect)
    {
        //_changeSelect = ChangeSelect.action.ReadValue<bool>();
        _changeSelect = ChangeSelect.action.triggered;
    }
    public void OnChangeColor(InputAction.CallbackContext ChangeColor)
    { 
        //_changeColor = ChangeColor.action.ReadValue<bool>();
        _changeColor = ChangeColor.action.triggered;
    }
    private void Update()
    {
        if(CurrentColor == "bleu")transform.GetComponent<Renderer>().material.color = Color.blue;
        if(CurrentColor == "rouge")transform.GetComponent<Renderer>().material.color = Color.red;
        _playerDeplacement.PerformMovement(_mouvementValue, _currentSpeed);
        _playerRotation.LookAt(_mouvementValue,_visé);
        if (_throw && HandedBall)
        {
            _playerThrow.PerformThrow(_mouvementValue,_myBalls);
            HandedBall = false;
        }

        if (_changeSelect)
        {
            _armorSelected = !_armorSelected;
        }

        if (_changeColor && _armorSelected)
        {
            SwitchArmorColor();
        }
        if (_changeColor && !_armorSelected)
        {
            for (int i = 0; i < _myBalls.Count - 1; i++)
            {
                _myBalls[i].transform.GetComponent<Ball>().SwitchBallColor();
            }
        }
    }
    
    public void SwitchArmorColor()
    {
        for (int i = 0; i < _colorList.Length - 1; i++)
        {
            if (CurrentColor == _colorList[i])
            {
                if (i == _colorList.Length - 1) CurrentColor = _colorList[0];
                else CurrentColor = _colorList[i + 1];
            }
        }
    }
 
}
