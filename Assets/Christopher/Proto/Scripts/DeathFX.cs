using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFX : MonoBehaviour
{
    private Material _deathFX;
    // Start is called before the first frame update
    void Start()
    {
        _deathFX = transform.GetComponent<SpriteRenderer>().material;
    }

  
    void Update()
    {
       // _deathFX.shader.= transform.GetComponent<SpriteRenderer>().sprite;
    }
}
