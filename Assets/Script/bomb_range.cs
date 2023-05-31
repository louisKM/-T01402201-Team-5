using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb_range : MonoBehaviour
{
    public GameObject bomb;
    string bombname;
    bool check;
    // Start is called before the first frame update
    void Start()
    {
        check=true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        SimplePlayerController controller = other.GetComponent<SimplePlayerController>();
        bombname=this.gameObject.name;
        
        if (controller != null && check==true)
        {
            bomb.GetComponent<bomb>().onRange();
            bomb.GetComponent<bomb>().bombsGannaExp(bombname);
            check=false;
        }
    }
}
