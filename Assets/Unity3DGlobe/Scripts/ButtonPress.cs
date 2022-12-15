using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour
{
    public Button A;
    public Button B;

    int A_BrainScore = 0;
    int B_BrainScore = 0;

    void Start()
    {
        ;
    }

    public void PressAButton()

    {
        if (A)
            //why can't I load this function in the OnClick() of button A?

        {
            A_BrainScore = A_BrainScore + 1;
            Debug.Log(A_BrainScore);
        }
    }

    public void PressBButton()
    {
        if (B)
        {
            B_BrainScore = B_BrainScore + 1;
            Debug.Log(B_BrainScore);
        }
    }
}

