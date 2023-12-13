using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Christopher.Proto.Scripts
{
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
        public GameObject Projectile;//datacom

        //[SerializeField] private List<Animator> animList;
        [SerializeField] private InputActionReference Move, Throw, ChangeColor, ChangeSelect, Dash;
        [SerializeField] private CharacterDisplay characterDisplay;
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float moveSpeed;
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
    

        //public AnimatorOverrideController AnimatorOverrideController;
    
        private void Awake()
        {
            //animator.runtimeAnimatorController = AnimatorOverrideController;
            _visé = transform.GetChild(0).transform;
            int randomColorIndex = Random.Range(0, _colorList.Length);
            CurrentColor = _colorList[randomColorIndex];
            _rb = transform.GetComponent<Rigidbody2D>();
        }
        void Start()
        {
            animator.runtimeAnimatorController = characterDisplay.CharacterAnimatorSelection(MyDwarf, CurrentColor);
            _currentDashRecoveringTime = dashRecoveringTime;
            _timeDash = dashDistanceTime;
            _currentThrowingPower = thriwingPower;
            _currentSpeed = moveSpeed;
            _currentCooldownDash = CooldownDash;
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
            HelperByChris.Fliper(_mouvementValue.x,-0.05f,spriteRenderer);
            if (_mouvementValue == Vector2.zero)
            {
                animator.SetBool("moving up",false);
                animator.SetBool("moving down",false);
                animator.SetBool("moving horizontal",false);
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
            
            var viséPosition = _visé.position;
            switch (_mouvementValue.x )
            {
                case <-0.1f :
                    viséPosition.x = transform.position.x - 1;
                    _orientation.x = -1;
                    animator.SetBool("moving horizontal",true);
                    break;
                case >0.1f:
                    viséPosition.x = transform.position.x + 1;
                    _orientation.x = 1;
                    animator.SetBool("moving horizontal",true);
                    break;
                default:
                    viséPosition.x = transform.position.x;
                    _orientation.x = 0;
                    animator.SetBool("moving horizontal",false);
                    break;
            }
            switch (_mouvementValue.y )
            {
                case <-0.1f :
                    viséPosition.y = transform.position.y - 1;
                    _orientation.y = -1;
                    animator.SetBool("moving down",true);
                    break;
                case >0.1f:
                    viséPosition.y = transform.position.y + 1;
                    _orientation.y = 1;
                    animator.SetBool("moving up",true);
                    break;
                default:
                    viséPosition.y = transform.position.y;
                    _orientation.y = 0;
                    animator.SetBool("moving down",false);
                    animator.SetBool("moving up",false);
                    break;
            }

            if (_mouvementValue.x == 0 && _mouvementValue.y == 0)
            {
                viséPosition.x = transform.position.x + 1;
                _orientation.x = 1;
            }
        }
        private void PerformThrow()
        {
            animator.SetBool("throwing",true);
            HandedBall = false;
            var o = Instantiate(Projectile);
            MyBalls.Add(o);
            o.transform.position = _visé.position;
            o.transform.GetComponent<Rigidbody2D>().AddForce( _orientation * _currentThrowingPower, ForceMode2D.Impulse);
            o.transform.GetComponent<Ball>().MyOwner = gameObject;
            o.transform.GetComponent<Ball>().CurrentColor = CurrentColor;
            animator.SetBool("throwing",false);
        }
        private void SwitchColor()
        {
            if (CurrentColor == "bleu") CurrentColor = "rouge";
            else if (CurrentColor == "rouge") CurrentColor = "bleu";
            animator.runtimeAnimatorController = characterDisplay.CharacterAnimatorSelection(MyDwarf, CurrentColor);
        }

        private void PerformDash()
        {
            animator.SetBool("dashing",true);
            _dashing = true;
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
}
