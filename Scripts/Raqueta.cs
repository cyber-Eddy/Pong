using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raqueta : MonoBehaviour
{
    public float velocidad = 50.0f;
    private Rigidbody2D rb;
    Vector2 input;

    //ejes horizontal y vertical
    public string ejeH, ejeV;
    public float max, min;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        input.x = Input.GetAxis(ejeH);
        input.y = Input.GetAxis(ejeV);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = input * velocidad;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, min, max), transform.position.y, transform.position.z);
    }
}
