using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public Text peopleTextBox, endingTextBox;
    public Image blackBox;
    public string msg;
    public float timer = 10;
    public TrainController train;
    // Start is called before the first frame update

    private void ending()
    {
        peopleTextBox.enabled = false;
        endingTextBox.enabled = true;
        blackBox.enabled = true;
        endingTextBox.text = msg;


        StartCoroutine("NewGame");
    }

    IEnumerator NewGame()
    {
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene("Main");
    }


    void OnTriggerStay2D(Collider2D collider)
    {
        if (Input.GetButtonDown("Vertical") && collider.CompareTag("Player") && !train.playerOnBoard)
        {
            print("endGame");
            ending();
        }

    }
}
