using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointandPotal : MonoBehaviour
{
    public GameObject departure_obj;
    public GameObject arrival_obj;
    public GameObject SimplePlayerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* if (Input.GetKeyDown(KeyCode.G)) {
            Debug.Log("in update key down log");
        } */
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            departure_obj = collision.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("Player") && Input.GetKey(KeyCode.C) && this.gameObject.name == "s2_potal") {
            if(SimplePlayerController.GetComponent<SimplePlayerController>().curse_count==0) {
                SceneManager.LoadScene("gameClear");
            } else {
                SceneManager.LoadScene("GameOver");
            }
        }
        else if(collision.CompareTag("Player") && Input.GetKey(KeyCode.C)) {
            StartCoroutine( TeleportRoutine() );
            SimplePlayerController.GetComponent<SimplePlayerController>().g_sw=true;
            //Debug.Log("keydown");
        }
    }

    IEnumerator TeleportRoutine() {
        yield return null;
        departure_obj.GetComponent<SimplePlayerController>().g_sw=true;
        yield return new WaitForSeconds(2.0f);

        departure_obj.transform.position = arrival_obj.transform.position;

        yield return new WaitForSeconds(1.0f);
        departure_obj.GetComponent<SimplePlayerController>().g_sw=false;
    }
}

/*
    public Vector3 updown(string pointname) {
        Vector3 position=new Vector3 (0,0,0);

        //stage1 portal
        if (pointname=="point_s1_1f_u") {
            position.x=19.0f; position.y=7.4f; position.z=0.0f;
            return position;
        }
        else if (pointname=="point_s1_2f_d") {
            position.x=21.7f; position.y=-1.0f; position.z=0.0f;
            return position;
        }
        else if (pointname=="point_s1_2f_u") {
            position.x=-8.5f; position.y=15.6f; position.z=0.0f;
            return position;
        }
        else if (pointname=="point_s1_3f_d") {
            position.x=-13.0f; position.y=7.5f; position.z=0.0f;
            return position;
        }

        else if (pointname=="s1_potal") {
            position.x=59.0f; position.y=16.0f; position.z=0.0f;
            return position;
        }

        //stage2 portal
        else if (pointname=="point_s2_3f_d") {
            position.x=84.2f; position.y=7.5f; position.z=0.0f;
            return position;
        }
        else if (pointname=="point_s2_2f_u") {
            position.x=77.3f; position.y=16.0f; position.z=0.0f;
            return position;
        }
        else if (pointname=="point_s2_2f_d") {
            position.x=50.0f; position.y=-0.7f; position.z=0.0f;
            return position;
        }
        else if (pointname=="point_s2_1f_u") {
            position.x=53.0f; position.y=7.5f; position.z=0.0f;
            return position;
        }
        else {
            return position;
        }
    } */
