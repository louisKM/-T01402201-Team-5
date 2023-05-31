using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McD : MonoBehaviour
{
    Animator animator;
    bool onrange=false;
    public float coolTime=3.0f;
    private float curTime;
    public Transform pos;
    public Vector2 boxSize;
    int mcd_health=2;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(curTime <= 0) {
            if(onrange) { //사거리 오브젝트를 플레이어가 밟았다?
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach (Collider2D collider in collider2Ds) {
                    if (collider.tag == "Player") {
                        collider.GetComponent<SimplePlayerController>().ChangeHealth(-1);
                    }
                }
                animator.SetTrigger("atk");
                curTime=coolTime;
            }
        }
        else {
            curTime -= Time.deltaTime;
        }

        if (mcd_health<=0) {
            Destroy(gameObject);
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

    public void hit(int damege) {
        mcd_health=mcd_health-damege;
        //Debug.Log("McD health :"+mcd_health+"/ 2");
    }
}
