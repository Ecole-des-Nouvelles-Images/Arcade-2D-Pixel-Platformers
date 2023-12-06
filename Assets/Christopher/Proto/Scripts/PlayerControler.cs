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
    public bool HandedBall;
    public int PlayerNumber;
    public int RoundCount;
    [FormerlySerializedAs("_myBalls")] public List<GameObject> MyBalls = new List<GameObject>();
    
    [SerializeField] private InputActionReference Move, Throw, ChangeColor, ChangeSelect, Dash;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float thriwingPower;
    [SerializeField] private float Cooldown = 5f;
    [SerializeField] private float dashPower = 2f;
    [SerializeField] private float dashTime = 1.5f;
    
    private string[] _colorList = new string[] { "bleu", "rouge" };
    private Transform _visé;
    private float _currentSpeed;
    private Vector2 _mouvementValue /*= Vector2.zero*/;
    private Vector2 _orientation;
    private bool _armorSelected = true;
    private float _currentThrowingPower;
    private Rigidbody2D _rb;
    private float _currentCooldown ;
    private bool _dashEnabled = true;
    private bool _dashIsPossible = true;
    private bool _dashing;
    private float Xmove;
    private float Zmove;
    private float _timeDash;
    private void Awake()
    {
        _visé = transform.GetChild(0).transform;
        int randomColorIndex = Random.Range(0, _colorList.Length);
        CurrentColor = _colorList[randomColorIndex];
        _rb = transform.GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        _timeDash = dashTime;
        //HandedBall = true;
        _currentThrowingPower = thriwingPower;
        _currentSpeed = moveSpeed;
        
        GameManager.AddPlayerList.Invoke(gameObject);
    }
    public void OnMove(InputAction.CallbackContext Move)
    {
        if(!_dashing) _mouvementValue = Move.action.ReadValue<Vector2>();
        //Debug.Log(_mouvementValue);
    }
    public void OnThrow(InputAction.CallbackContext Throw)
    {
        if (!Throw.started || !HandedBall) return;
        PerformThrow();
        
    }
    public void OnChangeSelect(InputAction.CallbackContext ChangeSelect)
    {
        if (ChangeSelect.started)
        {
            _armorSelected = !_armorSelected;
        }
        //Debug.Log(_armorSelected);
    }
    public void OnChangeColor(InputAction.CallbackContext ChangeColor)
    { 
        if (ChangeColor.started && _armorSelected)
        {
            SwitchColor();
        }
        if (ChangeColor.started && !_armorSelected && MyBalls.Count > 0)
        {
            for (int i = 0; i < MyBalls.Count; i++)
            {
                MyBalls[i].transform.GetComponent<Ball>().SwitchBallColor();
            }
        }
    }
    public void OnDash(InputAction.CallbackContext Dash)
    {
        if (Dash.started && _dashEnabled)
        {
            PerformDash();
        }
    }

    private void FixedUpdate()
    {
        PerformDepalecement();
        //PerformDepalecement2();
    }

    private void Update()
    {
        //if(_mouvementValue == Vector2.zero)_rb.velocity = Vector2.zero;
        if (_currentCooldown > 0 && !_dashEnabled)
        {
            _currentCooldown -= Time.deltaTime;
            if (_currentCooldown <= 0) _dashEnabled = true;
        }

        if (Health <= 0)
        {
            GameManager.RemovePlayerList.Invoke(gameObject);
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
        if(CurrentColor == "bleu")transform.GetComponent<Renderer>().material.color = Color.blue;
        if(CurrentColor == "rouge")transform.GetComponent<Renderer>().material.color = Color.red;
        LookAt();
        DashPerforming();
    }
    private void PerformDepalecement()
    {
        Xmove = _mouvementValue.x * _currentSpeed * Time.deltaTime;//* -1;
        Zmove = _mouvementValue.y * _currentSpeed * Time.deltaTime;
        Vector2 dep = new Vector2(Xmove, Zmove);
        _rb.velocity = dep;
    }
    private void PerformDepalecement2()
    {
        float xmove = _mouvementValue.x * _currentSpeed * Time.deltaTime;//* -1;
        float zmove = _mouvementValue.y * _currentSpeed * Time.deltaTime;
        _mouvementValue = new Vector2(xmove, zmove);
        transform.Translate(_mouvementValue);
    }
    private void LookAt()
    {
        //Debug.Log(_mouvementValue);
        //visé en mode gros dégueulasse
        if (_mouvementValue.x > 0.1f && _mouvementValue.y > 0.1f) //diag haut droit
        {
            _visé.position = new Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z);
        }

        if (_mouvementValue.x < -0.1f && _mouvementValue.y < -0.1f) //diag bas gauche
        {
            _visé.position = new Vector3(transform.position.x - 1, transform.position.y - 1, transform.position.z);
        }

        if (_mouvementValue.x < -0.1f && _mouvementValue.y > 0.1f) //diag haut gauche
        {
            _visé.position = new Vector3(transform.position.x - 1, transform.position.y + 1, transform.position.z);
        }

        if (_mouvementValue.x > 0.1f && _mouvementValue.y < -0.1f) //diag bas droit
        {
            _visé.position = new Vector3(transform.position.x + 1, transform.position.y - 1, transform.position.z);
        }

        if (_mouvementValue.y < 0.1 && _mouvementValue.y > -0.1 && _mouvementValue.x > 0.1f) //droit
        {
            _visé.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        }

        if (_mouvementValue.y < 0.1 && _mouvementValue.y > -0.1 && _mouvementValue.x < -0.1f) //gauche
        {
            _visé.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        }

        if (_mouvementValue.x < 0.1 && _mouvementValue.x > -0.1 && _mouvementValue.y > 0.1f) //haut
        {
            _visé.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        }

        if (_mouvementValue.x < 0.1 && _mouvementValue.x > -0.1 && _mouvementValue.y < -0.1f) //bas
        {
            _visé.position = new Vector3(transform.position.x , transform.position.y - 1, transform.position.z);
        }
    }
    private void PerformThrow()
    {
        HandedBall = false;
        var o = Instantiate(projectile);
        MyBalls.Add(o);
        o.transform.position = _visé.position;
        o.transform.GetComponent<Rigidbody2D>().AddForce( _rb.velocity * _currentThrowingPower, ForceMode2D.Impulse);
        o.transform.GetComponent<Ball>().MyOwner = gameObject;
        o.transform.GetComponent<Ball>().CurrentColor = CurrentColor;
    }
    private void SwitchColor()
    {
        if (CurrentColor == "bleu") CurrentColor = "rouge";
        else if (CurrentColor == "rouge") CurrentColor = "bleu";
    }

    private void PerformDash()
    {/*
        if (_dashIsPossible)
        {
            Vector3 dep = new Vector2(Xmove, Zmove);
            transform.position += dep*dashPower/10;
            //_rb.AddForce(_rb.velocity*dashPower,ForceMode2D.Impulse);
            _dashEnabled = false;
            _currentCooldown = Cooldown;
        }*/
        if (_dashIsPossible && !_dashing)
        {
            _dashing = true;
            //Vector3 dep = new Vector2(Xmove, Zmove);
            //_rb.velocity = Vector2.zero;
            _rb.AddForce(_rb.velocity*dashPower,ForceMode2D.Impulse);
        }
    }

    private void DashPerforming()
    {
        if (_dashing && _timeDash > 0) _timeDash -= Time.deltaTime;
        if (_timeDash <= 0)
        {
            _dashEnabled = false;
            _currentCooldown = Cooldown;
            _dashing = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Wall")) _dashIsPossible = false;
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.CompareTag("Wall")) _dashIsPossible = true;
    }
}
