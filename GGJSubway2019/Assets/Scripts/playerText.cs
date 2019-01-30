using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class playerText : MonoBehaviour
{
    public TextManager textManager;
    public bool station = false;
    public float textTimer = 4;
    public float fadeRate = 1;
    public string msg;

    void OnTriggerStay2D(Collider2D collider)
    {
        if (Input.GetButtonDown("Vertical")&& collider.CompareTag("Player"))
        {
            if (enabled)
            {
                textManager.NewText(msg);
            }
        }
    }
}
