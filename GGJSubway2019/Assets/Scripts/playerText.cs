using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class playerText : MonoBehaviour
{
    public Text text;
    public bool station = false;
    public float textTimer = 4;
    public float fadeRate = 1;
    public string msg;

    void OnTriggerStay2D(Collider2D collider)
    {
        if (Input.GetKeyDown(KeyCode.Space) && collider.CompareTag("Player"))
        {

            print("text1");
            StartCoroutine("DisplayText");
        }
    }

    IEnumerator DisplayText()
    {
        if (text.text != msg || text.color.a<.1f)
        {
            print("text");
            Color tmp = text.color;
            tmp.a = 1;
            text.color = tmp;
            text.text = msg;

            yield return new WaitForSeconds(textTimer);
            if (text.text == msg)
            {
                for (float f = 1f; f >= 0; f -= fadeRate * Time.deltaTime)
                {
                    tmp = text.color;
                    tmp.a = f;
                    text.color = tmp;
                    yield return null;
                }
            }
        }
    }
}
