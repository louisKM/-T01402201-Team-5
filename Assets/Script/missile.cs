using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missile : MonoBehaviour
{
    public float speed=2.5f; // 이동속도
    public int direction;
    Vector2 position; //현재위치
    public Vector2 first_position;

    public float start_timer=2;
    public GameObject missile_range;

    // Start is called before the first frame update
    void Start()
    {
        transform.position=first_position;
    }

    // Update is called once per frame
    void Update()
    {

        if(start_timer>0) {
            position=first_position;
            start_timer-=Time.deltaTime;
        } else {
            position.x=position.x + speed*direction*Time.deltaTime;
        }

        float cur_mis_x = position.x;
        float mis_range_x  = missile_range.transform.position.x;
        if ( cur_mis_x <= mis_range_x+0.5 && cur_mis_x >= mis_range_x-0.5) { 
            reset_position(); 
        } //위치 리셋
        
        transform.position=position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        SimplePlayerController controller = other.GetComponent<SimplePlayerController>();

        if (controller != null)
        {
            reset_position();
            keyboard.Instance.StartPattern((res) => cb_changeHealth(controller, res));// 키보드 패턴 시작
        }
    }

    public void reset_position() {
        position=first_position;
    }

    void cb_changeHealth(SimplePlayerController controller, e_KeyBord_State result)
    {
        if (controller == null)
            return;

        if (result == e_KeyBord_State.FAIL)
        {
            controller.ChangeHealth(-1);
        }
        keyboard.Instance.StopPattern();
    }

}
