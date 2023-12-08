using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public string CurrentColor;
    public GameObject MyOwner;

    [SerializeField] private Material flamesMat;
    [SerializeField] private Material iceMat;
    [SerializeField] private GameObject flamesParticules;
    [SerializeField] private GameObject iceParticules;
    [SerializeField] private GameObject flamesLight;
    [SerializeField] private GameObject iceLight;
    //[SerializeField] private GameObject impactIceParticules_1;
    [SerializeField] private GameObject impactIceParticules_2;
   //[SerializeField] private GameObject impactFlamesParticules_1;
    [SerializeField] private GameObject impactFlamesParticules_2;
    
    private string[] _colorList = new string[] { "bleu", "rouge" };

    private void Start()
    {
        CurrentColor = MyOwner.transform.GetComponent<PlayerControler>().CurrentColor;
    }
    private void Update()
    {
        if (CurrentColor == "bleu")
        {
            transform.GetComponent<Renderer>().material = iceMat;
            flamesParticules.SetActive(false);
            iceParticules.SetActive(true);
            flamesLight.SetActive(false);
            iceLight.SetActive(true);
        }

        if (CurrentColor == "rouge")
        {
            transform.GetComponent<Renderer>().material = flamesMat;
            flamesParticules.SetActive(true);
            iceParticules.SetActive(false);
            flamesLight.SetActive(true);
            iceLight.SetActive(false);
        }
    }
    public void SwitchBallColor()
    {
        if (CurrentColor == "bleu") CurrentColor = "rouge";
        else if (CurrentColor == "rouge") CurrentColor = "bleu";
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        CollideImpactEffect();
        if (other.transform.CompareTag("Player"))
        {
            if (CurrentColor != other.transform.GetComponent<PlayerControler>().CurrentColor && other.transform.GetComponent<PlayerRecover>().isRecovering == false)
            {
                other.transform.GetComponent<Rigidbody2D>().velocity += transform.GetComponent<Rigidbody2D>().velocity;
                transform.GetComponent<Rigidbody2D>().velocity *= -1;
                other.transform.GetComponent<PlayerControler>().Health -= 1;
                other.transform.GetComponent<PlayerRecover>().isRecovering = true;
                //Debug.Log(other.transform.GetComponent<PlayerControler>().Health);
            }
            if(!other.transform.GetComponent<PlayerControler>().HandedBall && CurrentColor == other.transform.GetComponent<PlayerControler>().CurrentColor)
            {
                other.transform.GetComponent<PlayerControler>().HandedBall = true;
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
            var o = Instantiate(impactIceParticules_2);
            o.transform.position = transform.position;
        }
        if (CurrentColor == "rouge")
        {
            var o = Instantiate(impactFlamesParticules_2);
            o.transform.position = transform.position;
        }
    }
}
