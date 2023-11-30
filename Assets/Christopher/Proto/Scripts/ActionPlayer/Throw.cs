using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Throw : MonoBehaviour
{
    public Transform Orientation;
    //public Vector3 Orientation;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float power;

    private float _currentPower;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentPower = power;
    }

    // Update is called once per frame
    public void PerformThrow(Vector2 dir)
    {
        var o = Instantiate(projectile);
        o.transform.position = Orientation.position;
        o.transform.GetComponent<Rigidbody2D>().AddForce(dir * _currentPower, ForceMode2D.Impulse);
    }
}
