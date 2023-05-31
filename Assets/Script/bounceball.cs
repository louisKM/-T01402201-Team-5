using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounceball : MonoBehaviour
{
    
    public float bounceball_speed=2.5f; // 이동속도
    Vector3 pos; //현재위치
    Vector2 first_pos;
    Vector2 now_pos;
    float delta = 5.0f; // 좌(우)로 이동가능한 (x)최대값

    void Start()
    { 
        pos = transform.position;
        first_pos=transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        now_pos=transform.position;
        Vector3 v = pos;
        float acceleration = delta * Mathf.Abs(Mathf.Sin(Time.time * bounceball_speed));
        // 좌우 이동의 최대치 및 반전 처리를 이렇게 한줄에 멋있게 하네요.
        v.y += acceleration;

        transform.position = v;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        SimplePlayerController controller = other.GetComponent<SimplePlayerController>();
        if (first_pos.y <= now_pos.y  &&  now_pos.y < first_pos.y+2) {
            if (controller != null)
            {
                controller.ChangeHealth(-1);
                //Debug.Log(first_pos+" "+now_pos);
            }
        }
    }
}
