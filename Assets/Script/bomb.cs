using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    Animator animator;
    bool onrange=false;
    public Transform pos;
    public Vector2 boxSize;
    GameObject bombN; string range_name; string bomb_name;
    public float limTime=2.0f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(onrange) { //사거리 오브젝트를 플레이어가 밟았다?
            animator.SetTrigger("onrange");
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
            if (limTime<=0) {
                foreach (Collider2D collider in collider2Ds) {
                    if (collider.tag == "Player") {
                        collider.GetComponent<SimplePlayerController>().ChangeHealth(-1);
                    }
                }
                explode(bomb_name);
            } else {
                limTime-=Time.deltaTime;
            }
        }
    }

    public void onRange() {
        onrange=true;
        //Debug.Log("Player is within range");
    }

    private void OnDrawGizmos() {
        Gizmos.color=Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }

    private void explode(string bombname) {
        bombN=GameObject.Find(bombname);
        Destroy(bombN);
        //Debug.Log("explode() run"+bombname);
    }

    public void bombsGannaExp(string rangename) {
        if (rangename=="Bomb_range_1") bomb_name="bomb_1";
        else if (rangename=="Bomb_range_2") bomb_name="bomb_2";
        else if (rangename=="Bomb_range_3") bomb_name="bomb_3";
        //Debug.Log("rangename : "+rangename+" bomb_name : "+bomb_name);
    }
}
