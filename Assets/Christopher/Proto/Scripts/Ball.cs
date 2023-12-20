using Michael.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Christopher.Proto.Scripts
{
    public class Ball : MonoBehaviour
    {
        public string CurrentColor;
        public GameObject MyOwner;

        [SerializeField] [Range(0, 1)] public float ballSpeedSlowingImpactFactor = 0.75f;
        [SerializeField] private Material flamesMat;
        [SerializeField] private Material iceMat;
        [SerializeField] private GameObject flamesEffects;
        [SerializeField] private GameObject iceEffects;
        [SerializeField] private GameObject throwIceParticules;
        [SerializeField] private GameObject impactIceParticules;
        [SerializeField] private GameObject throwFlamesParticules;
        [SerializeField] private GameObject impactFlamesParticules;
        [SerializeField] private AudioSource src;
        [SerializeField] private AudioClip[] sfx;// 0:feu ; 1:glace
    
        private string[] _colorList = new string[] { "bleu", "rouge" };

        
        private void Start()
        {
            CurrentColor = MyOwner.transform.GetComponent<PlayerControler>().CurrentColor;
            AudioAttributor();
            OnThrow();
        }
        private void Update()
        {
            if (CurrentColor == "bleu")
            {
                transform.GetComponent<Renderer>().material = iceMat;
                flamesEffects.SetActive(false);
                iceEffects.SetActive(true);
            }

            if (CurrentColor == "rouge")
            {
                transform.GetComponent<Renderer>().material = flamesMat;
                flamesEffects.SetActive(true);
                iceEffects.SetActive(false);
            }
            AudioAttributor();
        }
        public void SwitchBallColor()
        {
            if ( CurrentColor == "bleu") CurrentColor = "rouge";
            else if (CurrentColor == "rouge") CurrentColor = "bleu";
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
        
            CollideImpactEffect();
            if (other.transform.CompareTag("Player"))
            {
                transform.GetComponent<Rigidbody2D>().velocity *= ballSpeedSlowingImpactFactor;
                if (CurrentColor != other.transform.GetComponent<PlayerControler>().CurrentColor && other.transform.GetComponent<CharacterRecover>().isRecovering == false) // blessable?
                {
                    other.transform.GetComponent<Rigidbody2D>().velocity += transform.GetComponent<Rigidbody2D>().velocity;
                    transform.GetComponent<Rigidbody2D>().velocity *= -1;
                    other.transform.GetComponent<PlayerData>().Health -= 1;
                    other.transform.GetComponent<CharacterRecover>().isRecovering = true;
                    
                    //Debug.Log(other.transform.GetComponent<PlayerControler>().Health);
                }
                if(!other.transform.GetComponent<PlayerControler>().HandedBall && CurrentColor == other.transform.GetComponent<PlayerControler>().CurrentColor && MyOwner.transform.GetComponent<PlayerControler>().MyBalls != null) // absorbe?
                {
                    other.transform.GetComponent<PlayerControler>().HandedBall = true;
                    other.transform.GetComponent<CharacterDisplay>().BallAbsorbed(CurrentColor);
                    for (int i = 0; i < MyOwner.transform.GetComponent<PlayerControler>().MyBalls.Count; i++)
                    {
                        if (MyOwner.transform.GetComponent<PlayerControler>().MyBalls[i] == gameObject)
                        {
                            transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                            MyOwner.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                            MyOwner.transform.GetComponent<PlayerControler>().MyBalls.Remove
                                (MyOwner.transform.GetComponent<PlayerControler>().MyBalls[i]);
                        }
                    }
                    Destroy(gameObject);
                }
            }
            if (other.transform.CompareTag("Wall"))
            {
                Debug.Log("Wall!");
            }
        }

        private void CollideImpactEffect()
        {
            if (CurrentColor == "bleu")
            {
                var o = Instantiate(impactIceParticules);
                o.transform.position = transform.position;
            }
            if (CurrentColor == "rouge")
            {
                var o = Instantiate(impactFlamesParticules);
                o.transform.position = transform.position;
            }
        }

        private void OnThrow()
        {
            if (CurrentColor == "bleu")
            {
                var o = Instantiate(throwIceParticules);
                o.transform.position = transform.position;
            }
            else if (CurrentColor == "rouge")
            {
                var o = Instantiate(throwFlamesParticules);
                o.transform.position = transform.position;
            }
        }

        private void AudioAttributor()
        {
            if (CurrentColor == "bleu")
            {
                src.clip = sfx[1];
                src.Play();
            }
            if (CurrentColor == "rouge")
            {
                src.clip = sfx[0];
                src.Play();
            }
        }
    }
}
