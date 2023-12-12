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
    public bool IsAlive;//datacom
    public int Health = 3;//datacom
    public string CurrentColor;//datacom
    public bool HandedBall;//datacom
    public int PlayerNumber;//datacom
    public int RoundCount;//datacom
    public List<GameObject> MyBalls = new List<GameObject>();//datacom
    public int MyDwarf;//datacom

    [SerializeField] private List<Animator> animList;
    [SerializeField] private InputActionReference Move, Throw, ChangeColor, ChangeSelect, Dash;
    [SerializeField] private SpriteRenderer[] Dwarf_1;
    [SerializeField] private SpriteRenderer[] Dwarf_2;
    [SerializeField] private SpriteRenderer[] Dwarf_3;
    [SerializeField] private SpriteRenderer[] Dwarf_4;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float thriwingPower;
    [SerializeField] private float CooldownDash = 5f;
    [SerializeField] private float dashRecoveringTime = 1f;
    [SerializeField] private float dashPower = 2f;
    [SerializeField] private float dashDistanceTime = 2f;
    
    private string[] _colorList = new string[] { "bleu", "rouge" };
    private Transform _visé;
    private float _currentSpeed;
    private Vector2 _mouvementValue /*= Vector2.zero*/;
    private Vector2 _orientation;
    private bool _armorSelected = true;
    private float _currentThrowingPower;
    private Rigidbody2D _rb;
    private float _currentCooldownDash;
    private float _currentDashRecoveringTime;
    private bool _dashEnabled = true;
    private bool _dashing;
    private float Xmove;
    private float Zmove;
    private float _timeDash;

    public AnimatorOverrideController AnimatorOverrideController;
    
    private void Awake()
    {
        GetComponent<Animator>().runtimeAnimatorController = AnimatorOverrideController;
        
        
        
        _visé = transform.GetChild(0).transform;
        int randomColorIndex = Random.Range(0, _colorList.Length);
        CurrentColor = _colorList[randomColorIndex];
        _rb = transform.GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        _currentDashRecoveringTime = dashRecoveringTime;
        _timeDash = dashDistanceTime;
        _currentThrowingPower = thriwingPower;
        _currentSpeed = moveSpeed;
        _currentCooldownDash = CooldownDash;
        if (MyDwarf == 0 && CurrentColor == "bleu")Dwarf_1[0].enabled = true;
        if (MyDwarf == 0 && CurrentColor == "rouge")Dwarf_1[1].enabled = true;
        if (MyDwarf == 1 && CurrentColor == "bleu") Dwarf_2[0].enabled = true;
        if (MyDwarf == 1 && CurrentColor == "rouge") Dwarf_2[1].enabled = true;
        if (MyDwarf == 2 && CurrentColor == "bleu")Dwarf_3[0].enabled = true;
        if (MyDwarf == 2 && CurrentColor == "rouge")Dwarf_3[1].enabled = true;
        if (MyDwarf == 3 && CurrentColor == "bleu")Dwarf_4[0].enabled = true;
        if (MyDwarf == 3 && CurrentColor == "rouge") Dwarf_4[1].enabled = true;
    }
    public void OnMove(InputAction.CallbackContext Move)
    {
        _mouvementValue = Move.action.ReadValue<Vector2>();
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
        //PerformDepalecement();
        PerformDepalecement2();
    }

    private void Update()
    {
        if (MyDwarf == 0 && CurrentColor == "bleu")HelperByChris.Fliper(_mouvementValue.x,-0.05f,Dwarf_1[0]);//Dwarf_1[0].enabled = true;
        if (MyDwarf == 0 && CurrentColor == "rouge")HelperByChris.Fliper(_mouvementValue.x,-0.05f,Dwarf_1[1]);//Dwarf_1[1].enabled = true;
        if (MyDwarf == 1 && CurrentColor == "bleu")HelperByChris.Fliper(_mouvementValue.x,-0.05f,Dwarf_1[0]);// Dwarf_2[0].enabled = true;
        if (MyDwarf == 1 && CurrentColor == "rouge")HelperByChris.Fliper(_mouvementValue.x,-0.05f,Dwarf_1[1]); //Dwarf_2[1].enabled = true;
        if (MyDwarf == 2 && CurrentColor == "bleu")HelperByChris.Fliper(_mouvementValue.x,-0.05f,Dwarf_1[0]);//Dwarf_3[0].enabled = true;
        if (MyDwarf == 2 && CurrentColor == "rouge")HelperByChris.Fliper(_mouvementValue.x,-0.05f,Dwarf_1[1]);//Dwarf_3[1].enabled = true;
        if (MyDwarf == 3 && CurrentColor == "bleu")HelperByChris.Fliper(_mouvementValue.x,-0.05f,Dwarf_1[0]);//Dwarf_4[0].enabled = true;
        if (MyDwarf == 3 && CurrentColor == "rouge")HelperByChris.Fliper(_mouvementValue.x,-0.05f,Dwarf_1[1]); //Dwarf_4[1].enabled = true;
        if (_mouvementValue == Vector2.zero)
        {
            for (int i = 0; i < animList.Count; i++)
            {
                animList[i].SetBool("moving up",false);
                animList[i].SetBool("moving down",false);
                animList[i].SetBool("moving horizontal",false);
            }
        }
        if (_currentCooldownDash > 0 && !_dashEnabled)
        {
            _currentCooldownDash -= Time.deltaTime;
            if (_currentCooldownDash <= 0 && !_dashing) _dashEnabled = true;
        }

        if (Health <= 0)
        {
            //GameManager.RemovePlayerList.Invoke(gameObject);
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
        //if(CurrentColor == "bleu")transform.GetComponent<Renderer>().material.color = Color.blue;
        //if(CurrentColor == "rouge")transform.GetComponent<Renderer>().material.color = Color.red;
        LookAt();
        RecoverDash();
    }
    private void PerformDepalecement()
    {
        if (!_dashing)
        {
            Xmove = _visé.position.x * _currentSpeed * Time.deltaTime;//* -1;
            Zmove = _visé.position.y * _currentSpeed * Time.deltaTime;
            Vector2 dep = new Vector2(Xmove, Zmove);
            _rb.velocity = dep;
        }
    }
    private void PerformDepalecement2()
    {
        //flip la térale d'un sprite : if(SpriteRenderer&&Mathf.Abs(_rigidbody2D.velocity.x)>0.5f)SpriteRenderer.flipX = _rigidbody2D.velocity.x<-0.5f;
        if (_mouvementValue != Vector2.zero && !_dashing)
        {
            Xmove = _orientation.x * _currentSpeed * Time.deltaTime;//* -1;
            Zmove = _orientation.y * _currentSpeed * Time.deltaTime;
            Vector2 dep = new Vector2(Xmove, Zmove);
            transform.Translate(dep);
        }
    }
    private void LookAt()
    {
        //Debug.Log(_mouvementValue);
        //visé en mode gros dégueulasse
        if (_mouvementValue.x > 0.1f && _mouvementValue.y > 0.1f) //diag haut droit
        {
            _visé.position = new Vector3(transform.position.x + 1, transform.position.y + 1, transform.position.z);
            _orientation = new Vector2(1, 1);
            for (int i = 0; i < animList.Count; i++)
            {
                animList[i].SetBool("moving up",false);
                animList[i].SetBool("moving down",false);
                animList[i].SetBool("moving horizontal",true);
            }
        }

        if (_mouvementValue.x < -0.1f && _mouvementValue.y < -0.1f) //diag bas gauche
        {
            _visé.position = new Vector3(transform.position.x - 1, transform.position.y - 1, transform.position.z);
            _orientation = new Vector2(-1, -1);
            for (int i = 0; i < animList.Count; i++)
            {
                animList[i].SetBool("moving up",false);
                animList[i].SetBool("moving down",false);
                animList[i].SetBool("moving horizontal",true);
            }
        }

        if (_mouvementValue.x < -0.1f && _mouvementValue.y > 0.1f) //diag haut gauche
        {
            _visé.position = new Vector3(transform.position.x - 1, transform.position.y + 1, transform.position.z);
            _orientation = new Vector2(-1, 1);
            for (int i = 0; i < animList.Count; i++)
            {
                animList[i].SetBool("moving up",false);
                animList[i].SetBool("moving down",false);
                animList[i].SetBool("moving horizontal",true);
            }
        }

        if (_mouvementValue.x > 0.1f && _mouvementValue.y < -0.1f) //diag bas droit
        {
            _visé.position = new Vector3(transform.position.x + 1, transform.position.y - 1, transform.position.z);
            _orientation = new Vector2(1, -1);
            for (int i = 0; i < animList.Count; i++)
            {
                animList[i].SetBool("moving up",false);
                animList[i].SetBool("moving down",false);
                animList[i].SetBool("moving horizontal",true);
            }
        }

        if (_mouvementValue.y < 0.1 && _mouvementValue.y > -0.1 && _mouvementValue.x > 0.1f) //droit
        {
            _visé.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            _orientation = new Vector2(1, 0);
            for (int i = 0; i < animList.Count; i++)
            {
                animList[i].SetBool("moving up",false);
                animList[i].SetBool("moving down",false);
                animList[i].SetBool("moving horizontal",true);
            }
        }

        if (_mouvementValue.y < 0.1 && _mouvementValue.y > -0.1 && _mouvementValue.x < -0.1f) //gauche
        {
            _visé.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            _orientation = new Vector2(-1, 0);
            for (int i = 0; i < animList.Count; i++)
            {
                animList[i].SetBool("moving up",false);
                animList[i].SetBool("moving down",false);
                animList[i].SetBool("moving horizontal",true);
            }
        }

        if (_mouvementValue.x < 0.1 && _mouvementValue.x > -0.1 && _mouvementValue.y > 0.1f) //haut
        {
            _visé.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            _orientation = new Vector2(0, 1);
            for (int i = 0; i < animList.Count; i++)
            {
                animList[i].SetBool("moving up",true);
                animList[i].SetBool("moving down",false);
                animList[i].SetBool("moving horizontal",false);
            }
        }

        if (_mouvementValue.x < 0.1 && _mouvementValue.x > -0.1 && _mouvementValue.y < -0.1f) //bas
        {
            _visé.position = new Vector3(transform.position.x , transform.position.y - 1, transform.position.z);
            _orientation = new Vector2(0, -1);
            for (int i = 0; i < animList.Count; i++)
            {
                animList[i].SetBool("moving up",false);
                animList[i].SetBool("moving down",true);
                animList[i].SetBool("moving horizontal",false);
            }
        }
    }
    private void PerformThrow()
    {
        for (int i = 0; i < animList.Count; i++) {
            animList[i].SetBool("throwing",true);
        }
        HandedBall = false;
        var o = Instantiate(projectile);
        MyBalls.Add(o);
        o.transform.position = _visé.position;
        o.transform.GetComponent<Rigidbody2D>().AddForce( _orientation * _currentThrowingPower, ForceMode2D.Impulse);
        o.transform.GetComponent<Ball>().MyOwner = gameObject;
        o.transform.GetComponent<Ball>().CurrentColor = CurrentColor;
        for (int i = 0; i < animList.Count; i++) {
            animList[i].SetBool("throwing",false);
        }
    }
    private void SwitchColor()
    {
        if (MyDwarf == 0 && CurrentColor == "bleu") {
            CurrentColor = "rouge";
            Dwarf_1[0].enabled = false;
            Dwarf_1[1].enabled = true;
        }
        if (MyDwarf == 0 && CurrentColor == "rouge") {
            CurrentColor = "bleu";
            Dwarf_1[0].enabled = true;
            Dwarf_1[1].enabled = false;
        }

        if (MyDwarf == 1 && CurrentColor == "bleu") {
            CurrentColor = "rouge";
            Dwarf_2[0].enabled = false;
            Dwarf_2[1].enabled = true;
        }

        if (MyDwarf == 1 && CurrentColor == "rouge") {
            CurrentColor = "bleu";
            Dwarf_2[1].enabled = false;
            Dwarf_2[0].enabled = true;
        }

        if (MyDwarf == 2 && CurrentColor == "bleu") {
            CurrentColor = "rouge";
            Dwarf_3[0].enabled = false;
            Dwarf_3[1].enabled = true;
        }

        if (MyDwarf == 2 && CurrentColor == "rouge") {
            CurrentColor = "bleu";
            Dwarf_3[1].enabled = false;
            Dwarf_3[0].enabled = true;
        }

        if (MyDwarf == 3 && CurrentColor == "bleu") {
            CurrentColor = "rouge";
            Dwarf_4[0].enabled = false;
            Dwarf_4[1].enabled = true;
        }

        if (MyDwarf == 3 && CurrentColor == "rouge") {
            CurrentColor = "bleu";
            Dwarf_4[1].enabled = false;
            Dwarf_4[0].enabled = true;
        }
       /* 
        if (CurrentColor == "bleu") CurrentColor = "rouge";
        else if (CurrentColor == "rouge") CurrentColor = "bleu";*/
    }

    private void PerformDash()
    {
        for (int i = 0; i < animList.Count; i++) {
            animList[i].SetBool("dashing",true);
        }
        _dashing = true;
        //_currentSpeed *= dashPower;
        /*Xmove = _orientation.x * dashPower * Time.deltaTime;//* -1;
        Zmove = _orientation.y * dashPower * Time.deltaTime;
        Vector2 dep = new Vector2(Xmove, Zmove);*/
        //_rb.velocity = dep;
        _rb.AddForce(_orientation*dashPower,ForceMode2D.Impulse);
        _currentSpeed = moveSpeed;
        _dashEnabled = false;
        
    }
    private void RecoverDash()
    {
        if (_timeDash > 0 && _dashing) _timeDash -= Time.deltaTime;
        if (_timeDash <= 0 && _dashing && dashRecoveringTime != 0)
        {
            if (_currentDashRecoveringTime > 0)
            {
                _currentDashRecoveringTime -= Time.deltaTime;
                _rb.velocity = Vector2.zero;
            }
            else
            {
                for (int i = 0; i < animList.Count; i++) {
                    animList[i].SetBool("dashing",false);
                }
                _rb.velocity = Vector2.zero;
                _currentCooldownDash = CooldownDash;
                _dashing = false;
                _timeDash = dashDistanceTime;
                _currentDashRecoveringTime = dashRecoveringTime;

            }
            
            
        }
    }
}
