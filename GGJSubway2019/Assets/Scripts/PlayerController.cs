using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Transform train;
    public float moveSpeed;
    public float trainSize, stationRightSize;

    private Rigidbody2D rb;
    private float previousMovement;
    private float playerXPos;
    private float previousPlayerXPos;

    private Animator anime;

    void Start()
    {
        anime = GetComponent<Animator>();
        playerXPos = 3.1f;
        previousPlayerXPos = playerXPos;
        previousMovement = 0;
        rb = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        transform.position = new Vector3(train.position.x+playerXPos, transform.position.y);
        float movement = Input.GetAxis("Horizontal");
        bool stillPressing = true;

        //determines if u let go 
        if (Mathf.Abs(previousMovement) > Mathf.Abs(movement) && Mathf.Sign(previousMovement) == Mathf.Sign(movement))
        {
            stillPressing = false;
        }

        previousMovement = movement;

        if (Mathf.Abs(movement) > 0 &&stillPressing){
            movement = Mathf.Sign(movement)*1;


            playerXPos +=movement * moveSpeed * Time.deltaTime;

            if ((playerXPos - previousPlayerXPos) > 0)
            {
                if (!anime.GetCurrentAnimatorStateInfo(0).IsName("WalkingRight"))
                {
                    anime.SetTrigger("Right");
                }

            }
            else if ((playerXPos - previousPlayerXPos) < 0)
            {
                if (!anime.GetCurrentAnimatorStateInfo(0).IsName("WalkingLeft"))
                {
                    anime.SetTrigger("Left");
                }
            }
            

            //on train box
            if (train.gameObject.GetComponent<TrainController>().playerOnBoard)
            {
                if (Mathf.Abs(playerXPos) > trainSize)
                    playerXPos = Mathf.Sign(playerXPos) * trainSize;

               

            }
            else
            {
                if (playerXPos<-trainSize-.2f)
                    playerXPos = -trainSize-.2f;
                else if(playerXPos>stationRightSize)
                    playerXPos = stationRightSize;
            }

            previousPlayerXPos = playerXPos;

            //rb.MovePosition(new Vector2(transform.position.x + movement * moveSpeed*Time.deltaTime, transform.position.y));

        }
        else
        {
            anime.SetTrigger("Stopped");
        }


        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
