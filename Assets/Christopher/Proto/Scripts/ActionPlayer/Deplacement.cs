using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Deplacement : MonoBehaviour
{
    private Rigidbody2D _rb;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = transform.GetComponent<Rigidbody2D>();
    }
    public void PerformMovement(Vector2 dep, float speed)
    {
        float xmove = dep.x * speed * Time.deltaTime;//* -1;
        float zmove = dep.y * speed * Time.deltaTime;
        dep = new Vector2(xmove, zmove);
        _rb.AddForce(dep, ForceMode2D.Impulse);
    }
    
}
