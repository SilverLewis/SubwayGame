﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{
   

    private Vector3 nextStop;
    private int stopCount;
    private bool travelling, slowingDown, doorOpen, arrived, switched;
    private Rigidbody2D rb;


    public GameObject DoorHolder;
    public Transform[] AllStops;
    public bool playerOnBoard;
    public float fadeRate, MaxSpeed, Accel, doorTimer, waitTimer;

    void Start()
    {
       
        switched = true;
        stopCount = 1;
        nextStop = AllStops[stopCount].position;
        travelling = false;
        StartCoroutine("FadeOut");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (travelling)
            Travelling();

    }

    void Travelling() {
        
        float distance = Mathf.Abs(Vector3.Distance(gameObject.transform.position, nextStop));
        if ((distance <= Mathf.Abs(Mathf.Pow(rb.velocity.x, 2)) / (2 * Accel)) || slowingDown)
        {
            slowingDown = true;
            rb.velocity += new Vector2(Accel * Time.deltaTime, 0);

            

                //this should be going negitive
           if (rb.velocity.x > 0)
            {
                rb.velocity = new Vector2(-.075f, rb.velocity.y);
            }
                
        }

        else if (Mathf.Abs(rb.velocity.x) < MaxSpeed)
        {
            rb.velocity -= new Vector2(Accel * Time.deltaTime, 0);

            if (Mathf.Sign(rb.velocity.x) > MaxSpeed)
                rb.velocity = new Vector2(MaxSpeed, rb.velocity.y);
        }

            

        //safety ending
        if ((gameObject.transform.position.x-.001f) <= nextStop.x) {
            ArrivedAtStation();
        }
    }

    void ArrivedAtStation()
    {
        if(!arrived)
            StartCoroutine("AtStation");

        gameObject.transform.position = nextStop;

        slowingDown = false;
        rb.velocity = new Vector2(0, 0);
    }

    IEnumerator AtStation()
    {
        
        arrived = true;
        StartCoroutine("DoorTimer");//might change to a door method
        yield return new WaitForSeconds(doorTimer);
        travelling = false;
        for (float f = 0; f < waitTimer && playerOnBoard; f += .1f)
        {
            yield return new WaitForSeconds(.1f);
        }

        if (playerOnBoard && stopCount < AllStops.Length) {
            travelling = true;
            StartCoroutine("DoorTimer");//might change to a door method
            yield return new WaitForSeconds(doorTimer);
            MoveTrain();
        }
    }

    IEnumerator FadeOut()
    {
        playerOnBoard = false;
        for (float f = 1f; f >= 0; f -= fadeRate * Time.deltaTime)
        {
            Color tmp = GetComponent<SpriteRenderer>().color;
            tmp.a = f;
            GetComponent<SpriteRenderer>().color = tmp;
            for (int i = 1; i < transform.childCount; i++)
            {
                for (int j = 0; j < transform.GetChild(i).childCount; j++)
                {
                    tmp = transform.GetChild(i).GetChild(j).GetComponent<SpriteRenderer>().color;
                    tmp.a = f;
                    transform.GetChild(i).GetChild(j).GetComponent<SpriteRenderer>().color = tmp;
                }
            }

            tmp = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
            tmp.a = 1-f;
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = tmp;
            //yield return null;
            yield return null;
        }
        foreach (playerText child in transform.GetComponentsInChildren<playerText>())
        {
            child.enabled = false;
        }
    }

    IEnumerator FadeIn()
    {
        playerOnBoard = true;
        switched = false;
        for (float f = 0f; f <= 1; f += fadeRate * Time.deltaTime)
        {
            Color tmp = GetComponent<SpriteRenderer>().color;
            tmp.a = f;
            GetComponent<SpriteRenderer>().color = tmp;

            for (int i = 1; i < transform.childCount; i++)
            {
                for (int j = 0; j < transform.GetChild(i).childCount; j++) {
                    tmp = transform.GetChild(i).GetChild(j).GetComponent<SpriteRenderer>().color;
                    tmp.a = f;
                    transform.GetChild(i).GetChild(j).GetComponent<SpriteRenderer>().color = tmp;
                }
            }




            tmp = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
            tmp.a = 1 - f;
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = tmp;
            foreach (playerText child in transform.GetComponentsInChildren<playerText>())
            {
                child.enabled = true;
            }
            yield return null;
        }

        StartCoroutine("DoorTimer");//might change to a door method
        yield return new WaitForSeconds(doorTimer);

        switched = true;
        MoveTrain();
    }

    //prob should include audio+animation and a bool to see which animation to play
    IEnumerator DoorTimer()
    {
        if (stopCount < AllStops.Length || doorOpen) { 
            for (int i = 0; i < DoorHolder.transform.childCount; i++) {
                DoorHolder.transform.GetChild(i).GetComponent<Animator>().SetTrigger("FlipState");
            }
        

        yield return new WaitForSeconds(doorTimer);
        doorOpen = !doorOpen;
        }
    }

    void MoveTrain()
    {
        if (stopCount < AllStops.Length)
        {
            arrived = false;
            travelling = true;
            nextStop = AllStops[stopCount].position;

            nextStop += new Vector3(-1.9f, 0);
            stopCount++;
        }
    }

    void ExitTrain() {
        StartCoroutine("FadeOut");
        playerOnBoard = false;
    }

    void OnTriggerStay2D(Collider2D collider)
    { 
        if (Input.GetButtonUp("Vertical") && !travelling && collider.CompareTag("Player")&&playerOnBoard&&switched) {
            print("exited train");
            ExitTrain();
        }
        if (Input.GetButtonUp("Vertical") && !travelling && collider.CompareTag("Player") && !playerOnBoard&& 
            transform.GetChild(0).GetComponent<SpriteRenderer>().color.a >=.9f && switched)
        {
            StartCoroutine("FadeIn");
            playerOnBoard = true;
        }
    }
}
