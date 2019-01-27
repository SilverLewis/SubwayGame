using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class playerText : MonoBehaviour
{
    public Text text;
    public bool station;
    public float textTimer, fadeRate;
    public string msg;

    void OnTriggerStay2D(Collider2D collider)
    {
        if (Input.GetKeyDown(KeyCode.Space) && collider.CompareTag("Player"))
        {
            StartCoroutine("DisplayText");
        }
    }

    IEnumerator DisplayText()
    {
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = 1;
        GetComponent<SpriteRenderer>().color = tmp;
        text.text = msg;



        yield return new WaitForSeconds(textTimer);
        if (text.text==msg) {
            for (float f = 1f; f >= 0; f -= fadeRate * Time.deltaTime)
            {
                tmp = GetComponent<SpriteRenderer>().color;
                tmp.a = f;
                GetComponent<SpriteRenderer>().color = tmp;
                yield return null;
            }
        }
    }
}
