using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcurse : MonoBehaviour
{
    public static UIcurse instance { get; private set; }

    public Image[] ballImage;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }

    public void UpdateBallImages(int cursecount)
    {
        // Iterate over all life images
        for (int i = 0; i < ballImage.Length; i++)
        {
            // If this life image is within the number of current health, enable it. Otherwise, disable it.
            ballImage[i].enabled = i < cursecount;
        }
    }
}
