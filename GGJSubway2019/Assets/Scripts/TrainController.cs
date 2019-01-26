using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{
   

    private Transform nextStop;
    private int stopCount;
    private bool travelling, slowingDown;
    private Rigidbody2D rb;

    public Transform[] AllStops;
    public bool playerOnBoard;
    public float fadeRate, MaxSpeed, Accel, doorTimer, waitTimer;

    void Start()
    {
        playerOnBoard = true;
        rb = GetComponent<Rigidbody2D>();
        EnterTrain();
    }

    void Update()
    {
        if (travelling)
            Travelling();


        if (Input.GetKeyDown(KeyCode.E))
            MoveTrain();
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine("FadeIn");
        if (Input.GetKeyDown(KeyCode.Q))
            StartCoroutine("FadeOut");
    }

    void Travelling() {

        float distance = Mathf.Abs(Vector3.Distance(gameObject.transform.position, nextStop.transform.position));
        if ((distance <= Mathf.Abs(Mathf.Pow(rb.velocity.x, 2)) / (2 * Accel)) || slowingDown)
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
        if ((gameObject.transform.position.x-.1f) <= nextStop.transform.position.x) {
            ArrivedAtStation();
        }
    }

    void ArrivedAtStation()
    {
        StartCoroutine("AtStation");

        gameObject.transform.position = nextStop.transform.position;
        travelling = false;
        slowingDown = false;
        rb.velocity = new Vector2(0, 0);
        print("Arrived at station");
    }

    IEnumerator AtStation()
    {
        StartCoroutine("DoorTimer");//might change to a door method
        yield return new WaitForSeconds(doorTimer); 

        for (float f = 0; f < waitTimer && playerOnBoard; f += .1f)
        {
            yield return new WaitForSeconds(.1f);
        }

        if (playerOnBoard) {
            print("onboard");
            StartCoroutine("DoorTimer");//might change to a door method
            yield return new WaitForSeconds(doorTimer);
            print("door closed");
            MoveTrain();
        }
    }

    IEnumerator FadeOut()
    {
        for (float f = 1f; f >= 0; f -= fadeRate * Time.deltaTime)
        {
            Color tmp = GetComponent<SpriteRenderer>().color;
            tmp.a = f;
            GetComponent<SpriteRenderer>().color = tmp;
            yield return null;
        }
        TrainEnabled(false);
        playerOnBoard = false;
    }

    IEnumerator FadeIn()
    {
        playerOnBoard = true;
        for (float f = 0f; f <= 1; f += fadeRate * Time.deltaTime)
        {
            Color tmp = GetComponent<SpriteRenderer>().color;
            tmp.a = f;
            GetComponent<SpriteRenderer>().color = tmp;
            yield return null;
        }
        TrainEnabled(true);

        StartCoroutine("DoorTimer");//might change to a door method
        yield return new WaitForSeconds(doorTimer);

        MoveTrain();
    }

    void TrainEnabled(bool enable) {
    }

    //this method might not be needed and just do FadeIn
    void EnterTrain()
    {
        StartCoroutine("FadeIn");
    }

    //prob should include audio+animation and a bool to see which animation to play
    IEnumerator DoorTimer()
    {
        //maybe disable player movement here
        yield return new WaitForSeconds(doorTimer);
    }

    void MoveTrain()
    {
        if (stopCount < AllStops.Length)
        {
            travelling = true;
            nextStop = AllStops[stopCount];
            stopCount++;
        }
    }

    void ExitTrain() {
        StartCoroutine("FadeOut");
        travelling = false;
        playerOnBoard = false;
    }

}
