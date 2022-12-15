using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.


public class TimeScrub : MonoBehaviour
{
    //access datavisualizer GameObject
    public DataVisualizer ChangeData;
    //access slider GameObject
    public Slider SliderInstance;
    private object TimeStamp1;


    public void Start()
    {
        //define slider value range
        SliderInstance.minValue = 0;
        SliderInstance.maxValue = 2;
        SliderInstance.wholeNumbers = true;
        SliderInstance.value = 0; 
    }

    // Invoked when the value of the slider changes.
    public void ValueChange()
    {
        if (SliderInstance.value == 0)
        {
            // select TimeStamp 1 in index from ActivateSeries
            ChangeData = FindObjectOfType<DataVisualizer>();
            ChangeData.ActivateSeries(0);
            Debug.Log(SliderInstance.value);
        }

        else if (SliderInstance.value == 1)
        {
            // select TimeStamp 1 in index from ActivateSeries
            ChangeData = FindObjectOfType<DataVisualizer>();
            ChangeData.ActivateSeries(1);
            Debug.Log(SliderInstance.value);
        }
        

    }
  
    //public void DataScrub () 
    //{
        // store the datavislaulizer in a varaible
       // ChangeData = FindObjectOfType<DataVisualizer>();

        // a function can't be stored as an object? How to fix this?
        // TimeStamp1 = 
        //ChangeData.ActivateSeries(0);
       


        //begin ForEachLoop
        //object[] dataset = new object[3];

        //dataset[0] = ChangeData.ActivateSeries(0);

        //dataset[1] = ChangeData.ActivateSeries(1)

        //dataset[2] = ChangeData.ActivateSeries(2);

        //foreach (int item in dataset)
        //{
            //print (item);
        //}
    //}
}

