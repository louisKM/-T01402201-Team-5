using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangeBox : MonoBehaviour
{
    public GameObject McD;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        SimplePlayerController controller = other.GetComponent<SimplePlayerController>();
        
        if (controller != null)
        {
            McD.GetComponent<McD>().onRange();
        }
    }
}
