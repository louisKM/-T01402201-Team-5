using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIhealthBar : MonoBehaviour
{

    public static UIhealthBar instance { get; private set; }
    
    public Image[] lifeImage;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }
        
    public void UpdateLifeImages(int currentHealth)
    {
        // Iterate over all life images
        for (int i = 0; i < lifeImage.Length; i++)
        {
            // If this life image is within the number of current health, enable it. Otherwise, disable it.
            lifeImage[i].enabled = i < currentHealth;
        }
    }
}
