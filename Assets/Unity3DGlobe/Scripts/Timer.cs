using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float TargetTime = 5.0f;
    private GameObject PopUp;
    private GameObject Slider;


    void Start()
    {
        //find end screen pop up
        PopUp = GameObject.Find("popup");
        PopUp.gameObject.SetActive(false);

        //find slider
        Slider = GameObject.Find("Slider");
        Slider.gameObject.SetActive(true);

    }

    void Update()
    {

       TargetTime -= Time.deltaTime;

        if (TargetTime <= 0.0f)
        {
            TimerEnded();
        }

    }

    void TimerEnded()
    {
        PopUp.gameObject.SetActive(true);
        Slider.gameObject.SetActive(false);
        //Debug.Log(TargetTime);
    }


}
