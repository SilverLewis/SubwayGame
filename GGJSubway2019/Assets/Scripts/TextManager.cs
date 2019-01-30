using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextManager : MonoBehaviour
{
    private float startTextTime;
    public float timer;
    public float fadetimer;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        startTextTime = -100;
    }
    
    void Update()
    {
        if (startTextTime + timer > Time.time)
        {
            print("here: " + (startTextTime + timer) + ":" + Time.time);
            Color tmp = text.color;
            tmp.a = 1;
            text.color = tmp;
        }
        else if (text.color.a>0)
        {
            print("here");
            Color tmp = text.color;
            tmp.a = tmp.a - Time.deltaTime * fadetimer;
            text.color = tmp;
        }        
    }

    public void NewText(string msg) {
        text.text = msg;
        startTextTime = Time.time;
        print("Starting text time is:"+startTextTime);
    }

}
