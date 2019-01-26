using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{

    public GameObject nextStop;
    private bool travelling, slowingDown;
    private Rigidbody2D rb;

    public float MaxSpeed;
    public float Accel;

    void Start()
    {
        travelling = true;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (travelling)
            Travelling();
        if(Input.GetKeyDown(KeyCode.Space))
            StartCoroutine("Fade");
    }


    void Travelling() {

        float distance = Mathf.Abs(Vector3.Distance(gameObject.transform.position, nextStop.transform.position));
        if ((distance <= Mathf.Abs(Mathf.Pow(rb.velocity.x,2)) / (2*Accel)) || slowingDown)
        {
            slowingDown = true;
            rb.velocity += new Vector2(Accel * Time.deltaTime, 0);

            //this should be going negitive
            if (rb.velocity.x > 0)
                rb.velocity = new Vector2(0, rb.velocity.y);
        }

        else if (Mathf.Abs(rb.velocity.x) < MaxSpeed)
        {
            rb.velocity -= new Vector2(Accel * Time.deltaTime, 0);

            if (Mathf.Sign(rb.velocity.x) > MaxSpeed)
                rb.velocity = new Vector2(MaxSpeed, rb.velocity.y);
        }

        //safety ending
        if (gameObject.transform.position.x < nextStop.transform.position.x) {
            gameObject.transform.position = nextStop.transform.position;
            travelling = false;
            rb.velocity = new Vector2(0, 0);
            print("Arrived at station");
        }
    }

    IEnumerator Fade()
    {
        for (float f = 1f; f >= -.05; f -= 0.05f)
        {
            Color tmp = GetComponent<SpriteRenderer>().color;
            tmp.a = f;
            GetComponent<SpriteRenderer>().color = tmp;
            yield return null;
        }
    }
}
