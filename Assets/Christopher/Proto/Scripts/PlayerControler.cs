using System;
using System.Collections.Generic;
using Michael.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Christopher.Proto.Scripts
{
    public class PlayerControler : MonoBehaviour
    {
        public string CurrentColor;
        public bool HandedBall;
        public List<GameObject> MyBalls = new List<GameObject>();
        public GameObject Projectile;
        public float CurrentCooldownColorChange;
        //[SerializeField] private List<Animator> animList;
        [SerializeField] private InputActionReference Move, Throw, ChangeColor, ChangeSelect, Dash;
        [SerializeField] private CharacterDisplay characterDisplay;
        //[SerializeField] private CharacterOrientation characterOrientation;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float thriwingPower;
        public float CooldownColorChange = 3f;
       // [SerializeField] private float CooldownArmorColorChange = 3f;
        [SerializeField] private float CooldownDash = 5f;
        [SerializeField] private float dashRecoveringTime = 1f;
        [SerializeField] private float dashPower = 2f;
        [SerializeField] private float dashDistanceTime = 2f;
        [SerializeField] private AudioSource ThrowSound;
        
        private string[] _colorList = new string[] { "bleu", "rouge" };
        private Transform _visé;
        private float _currentSpeed;
        private Vector2 _mouvementValue;
        private Vector2 _orientation;
        public bool _armorSelected = true;
        private float _currentThrowingPower;
        private Rigidbody2D _rb;                    
        private float _currentCooldownDash;
        private float _currentDashRecoveringTime;
        private bool _dashEnabled = true;
        private bool _dashing;
        private float Xmove;
        private float Zmove;
        private float _timeDash;
        
        //private float _currentCooldownBallColorChange;
    
        private void Awake()
        {
            _visé = transform.GetChild(0).transform;
            int randomColorIndex = Random.Range(0, _colorList.Length);
            CurrentColor = _colorList[randomColorIndex];
            _rb = transform.GetComponent<Rigidbody2D>();
            //_currentCooldownColorChange = CooldownColorChange;
            //_currentCooldownBallColorChange = CooldownBallColorChange;
            
        }
        void Start()
        {
            Invoke(nameof(configureAnimator),0.2f);
            _currentDashRecoveringTime = dashRecoveringTime;
            _timeDash = dashDistanceTime;
            _currentThrowingPower = thriwingPower;
            _currentSpeed = moveSpeed;
            _currentCooldownDash = CooldownDash;
           
        }

        void configureAnimator()
        {
            animator.runtimeAnimatorController = characterDisplay.CharacterAnimatorSelection(playerData.characterChoice, CurrentColor);
            
        }
        
        
        public void OnMove(InputAction.CallbackContext Move)
        {
            if (!PauseControl.IsPaused && CountDownController.CanPlay)
            {
                _mouvementValue = Move.action.ReadValue<Vector2>();  
            }
          
        }
        public void OnThrow(InputAction.CallbackContext Throw)
        {
            if (!Throw.started || !HandedBall && !PauseControl.IsPaused) return;
            PerformThrow();
        }
        public void OnChangeSelect(InputAction.CallbackContext ChangeSelect)
        {
            if (ChangeSelect.started && !PauseControl.IsPaused && CountDownController.CanPlay)
            {
                _armorSelected = !_armorSelected;
            }
        }
        public void OnChangeColor(InputAction.CallbackContext ChangeColor)
        { 
            if (ChangeColor.started && _armorSelected && CurrentCooldownColorChange <= 0 && !PauseControl.IsPaused && CountDownController.CanPlay)
            {
                SwitchColor();
                CurrentCooldownColorChange = CooldownColorChange;
            }
            if (ChangeColor.started && !_armorSelected && MyBalls.Count > 0 && CurrentCooldownColorChange <= 0 && !PauseControl.IsPaused)
            {
                for (int i = 0; i < MyBalls.Count; i++)
                {
                    MyBalls[i].transform.GetComponent<Ball>().SwitchBallColor();
                }

                CurrentCooldownColorChange = CooldownColorChange;
            }
        }
        public void OnDash(InputAction.CallbackContext Dash)
        {
            if (Dash.started && _dashEnabled && !PauseControl.IsPaused && CountDownController.CanPlay)
            {
                PerformDash();
            }
        }

        private void FixedUpdate()
        {
            PerformDepalecement();
        }

        private void Update()
        {
            if (!CountDownController.CanPlay)
            {
                animator.speed = 0f;
            }
            else
            {
                animator.speed = TimeManager.Instance.timeScale;
            }
            if (!PauseControl.IsPaused && CountDownController.CanPlay)
            {
                HelperByChris.SpriteFliperX(_mouvementValue.x,0,spriteRenderer);
                if (CurrentCooldownColorChange > 0) CurrentCooldownColorChange -= TimeManager.Instance.deltaTime;
                //if (_currentCooldownBallColorChange > 0) _currentCooldownBallColorChange -= TimeManager.Instance.deltaTime;
                if (_mouvementValue == Vector2.zero)
                {
                    animator.SetBool("moving",false);
                }
                if (_currentCooldownDash > 0 && !_dashEnabled)
                {
                    _currentCooldownDash -= TimeManager.Instance.deltaTime;
                    if (_currentCooldownDash <= 0 && !_dashing) _dashEnabled = true;
                }

                if (playerData.Health <= 0)
                {
                    ResetBall();
                    characterDisplay.PlayDeathFX(CurrentColor);
                    gameObject.SetActive(false);
                  
                   
                    GameManager.Instance.PlayerAlive.Remove(gameObject);
                }
                //characterOrientation.SetOrientation(_mouvementValue,_visé,_orientation,animator);
                LookAt();
                RecoverDash(); 
            }
            
        }
        private void PerformDepalecement()
        {
            _currentSpeed = moveSpeed;
            if (_mouvementValue != Vector2.zero && !_dashing && !PauseControl.IsPaused && CountDownController.CanPlay)
            {
                Xmove = _orientation.x * _currentSpeed * TimeManager.Instance.fixedDeltaTime;//* -1;
                Zmove = _orientation.y * _currentSpeed * TimeManager.Instance.fixedDeltaTime;
                Vector2 dep = new Vector2(Xmove, Zmove);
                characterDisplay.PlayWalkingFX();
                transform.Translate(dep);
            }
        }
        private void LookAt()
        {
            if (!PauseControl.IsPaused && CountDownController.CanPlay)
            {
               
                switch (_mouvementValue.x )
                {
                    case <-0.1f :
                        _visé.position = new Vector3(transform.position.x - 1, _visé.position.y, 0);
                        _orientation.x = -1;
                        animator.SetBool("moving",true);
                    
                        break;
                    case >0.1f:
                        _visé.position = new Vector3(transform.position.x + 1, _visé.position.y, 0);
                        _orientation.x = 1;
                        animator.SetBool("moving",true);
                        break;
                    default:
                        _visé.position = new Vector3(transform.position.x, _visé.position.y, 0);
                        _orientation.x = 0;
                   
                        break;
                }
                switch (_mouvementValue.y )
                {
                    case <-0.1f :
                        _visé.position = new Vector3(_visé.position.x, transform.position.y-1, 0);
                        _orientation.y = -1;
                        animator.SetBool("moving",true);
                        break;
                    case >0.1f:
                        _visé.position = new Vector3(_visé.position.x, transform.position.y+1, 0);
                        _orientation.y = 1;
                        animator.SetBool("moving",true);
                        break;
                    default:
                        _visé.position = new Vector3(_visé.position.x, transform.position.y, 0);
                        _orientation.y = 0;
                    
                   
                        break;
                }

                if (_mouvementValue.x == 0 && _mouvementValue.y == 0)
                {
                    _orientation.x = 1;
                } 
            }
        }
        private void PerformThrow()
        {
            if (!PauseControl.IsPaused && CountDownController.CanPlay )
            {
                animator.SetTrigger("throwing");
                ThrowSound.Play();
                HandedBall = false;
                var o = Instantiate(Projectile);
                MyBalls.Add(o);
                o.transform.position = _visé.position;
                o.transform.GetComponent<Rigidbody2D>().AddForce( _orientation * _currentThrowingPower, ForceMode2D.Impulse);
                o.transform.GetComponent<Ball>().MyOwner = gameObject;
                o.transform.GetComponent<Ball>().CurrentColor = CurrentColor;
            }
          
           
        }
        private void SwitchColor()
        {
            if (!PauseControl.IsPaused && CountDownController.CanPlay)
            {
                if (CurrentColor == "bleu")
                {
                    CurrentColor = "rouge";
                    var o = Instantiate(characterDisplay.armorChangeFX[0]);
                    o.transform.position = transform.position;
                    Destroy(o,2);
                }
                else if (CurrentColor == "rouge")
                {
                    CurrentColor = "bleu";
                    var o = Instantiate(characterDisplay.armorChangeFX[1]);
                    o.transform.position = transform.position;
                    Destroy(o,2);
                }
                animator.runtimeAnimatorController = characterDisplay.CharacterAnimatorSelection(playerData.characterChoice, CurrentColor);
                CurrentCooldownColorChange = CooldownColorChange;
            }
            
        }

        private void PerformDash()
        {
            if (!PauseControl.IsPaused && CountDownController.CanPlay)
            {
                characterDisplay.PlayDashFX();
                animator.SetBool("dashing",true);
                _dashing = true;
                _rb.AddForce(_orientation.normalized * dashPower,ForceMode2D.Impulse);
                _currentSpeed = moveSpeed;
                _dashEnabled = false; 
            }
            
        }
        private void RecoverDash()
        {
            if (!PauseControl.IsPaused && CountDownController.CanPlay)
            {
                if (_timeDash > 0 && _dashing) _timeDash -= TimeManager.Instance.deltaTime;
                if (_timeDash <= 0 && _dashing && dashRecoveringTime != 0)
                {
                    if (_currentDashRecoveringTime > 0)
                    {
                        _currentDashRecoveringTime -= TimeManager.Instance.deltaTime;
                        _rb.velocity = Vector2.zero;
                    }
                    else
                    {
                        animator.SetBool("dashing",false);
                        _rb.velocity = Vector2.zero;
                        _currentCooldownDash = CooldownDash;
                        _dashing = false;
                        _timeDash = dashDistanceTime;
                        _currentDashRecoveringTime = dashRecoveringTime;
                    }
                }
            }
            
        }

        public void ResetBall()
        {
            for (int i = 0; i < MyBalls.Count; i++)
            {
                Destroy(MyBalls[i]);
                MyBalls.Remove(MyBalls[0]);
            }
        }
        
        
        
        
    }
}