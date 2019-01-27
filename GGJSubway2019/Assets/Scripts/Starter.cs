using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Starter : MonoBehaviour
{
    public PlayerController player;
    public Image image;
    public Text text;

    void Start()
    {
        player.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown) {
            print("here");
            player.enabled = true;
            image.enabled = false;
            text.enabled = false;
            gameObject.SetActive(false);
        }
    }
}
