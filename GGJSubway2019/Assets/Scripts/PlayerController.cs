using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    private Rigidbody2D rb;
    private float previousMovement;

    void Start()
    {
        previousMovement = 0;
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        float movement = Input.GetAxis("Horizontal");
        print ( movement);
        bool stillPressing = true;

        if (Mathf.Abs(previousMovement) > Mathf.Abs(movement) && Mathf.Sign(previousMovement) == Mathf.Sign(movement))
        {
            stillPressing = false;
        }

        previousMovement = movement;

        if (Mathf.Abs(movement) > 0 &&stillPressing){
            movement = Mathf.Sign(movement)*1;        
            rb.velocity = new Vector2(movement * moveSpeed, 0);
        }
        else
            rb.velocity = new Vector2(0, 0);




    }
}
