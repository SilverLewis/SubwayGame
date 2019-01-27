using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public Text peopleTextBox, endingTextBox;
    public Image blackBox;
    public string msg;
    public TrainController train;
    // Start is called before the first frame update

    private void ending()
    {
        peopleTextBox.enabled = false;
        endingTextBox.enabled = true;
        blackBox.gameObject.SetActive(true);
        endingTextBox.text = msg;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (Input.GetKeyDown(KeyCode.Space) && collider.CompareTag("Player") && !train.playerOnBoard)
        {
            print("endGame");
            ending();
        }

    }
}
