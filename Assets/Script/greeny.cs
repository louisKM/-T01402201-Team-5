using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greeny : MonoBehaviour
{
    public float speed= 5.0f;
    public bool horizontal=true;
    public float changeTime = 5.0f;
    Rigidbody2D rg2d;
    float timer;
    int direction = 1;
    private bool isAttacking=false;

    Animator animator;

    float atk_cooltime=3.0f;
    float atk_timer=1.0f;

    bool dir_for_player=false;
    float time_keepdir=5.0f;

    public Transform sight_pos;
    public Vector2 sight_boxSize;
    public Transform right_hit_pos;
    public Vector2 right_hit_boxSize;
    public Transform left_hit_pos;
    public Vector2 left_hit_boxSize;
    float stop_timer=4.0f;

    public Vector2 greeny_first_pos;

    GameObject greeny_obj;
    
    // Start is called before the first frame update
    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rg2d.MovePosition(greeny_first_pos);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = rg2d.position;
        SimplePlayerController call = GameObject.Find("Wizard").GetComponent<SimplePlayerController>();
        Vector2 player_pos = call.player_pos;

        if(stop_timer<=0) {
            timer -= Time.deltaTime;
            if (dir_for_player) {
                if (player_pos.x - position.x <=0) {
                    direction = -1;
                    animator.SetBool("isLeft", true);
                    time_keepdir-=Time.deltaTime;
                    if (time_keepdir<=0) {
                        dir_for_player=false;
                    }
                } else {
                    direction = 1;
                    animator.SetBool("isLeft", false);
                    time_keepdir-=Time.deltaTime;
                    if (time_keepdir<=0) {
                        dir_for_player=false;
                    }
                }
            } else {
                if (timer < 0)
                {
                    direction = -direction;
                    timer = changeTime;
                    if (direction>0) {
                        animator.SetBool("isLeft", false);
                    } else {
                        animator.SetBool("isLeft", true);
                    }
                }
            }
        } else { 
            position = greeny_first_pos;
            stop_timer-=Time.deltaTime;
        }

        if (!isAttacking)
        {
            position.x = position.x + Time.deltaTime * speed * direction;
        }

        Collider2D[] sight_collider2Ds = Physics2D.OverlapBoxAll(sight_pos.position, sight_boxSize, 0);
        foreach (Collider2D collider in sight_collider2Ds) {
            if (collider.tag == "Player") {
                //Debug.Log("player is within greenys sight");
                dir_for_player=true;
                if (atk_timer<=0) {
                    //Debug.Log("atk_timer <= 0");
                    if (player_pos.x > position.x) {
                        animator.SetBool("right_atk",true);
                        isAttacking=true;
                        animator.SetBool("isAttacking", true);
                        atk_timer=atk_cooltime;
                    } else {
                        animator.SetBool("left_atk",true);
                        isAttacking=true;
                        animator.SetBool("isAttacking", true);
                        atk_timer=atk_cooltime;
                    }
                } else {
                    //Debug.Log("atk_timer-=time.deltatime");
                    atk_timer=atk_timer-Time.deltaTime;
                    animator.SetBool("right_atk",false);
                    animator.SetBool("left_atk",false);
                }
            }
        }
        
        rg2d.MovePosition(position);
    }

    private void OnDrawGizmos() {
        Gizmos.color=Color.blue;
        Gizmos.DrawWireCube(sight_pos.position, sight_boxSize);
        Gizmos.DrawWireCube(right_hit_pos.position, right_hit_boxSize);
        Gizmos.DrawWireCube(left_hit_pos.position, left_hit_boxSize);
    }

    private void atk_end() {
        isAttacking=false;
        animator.SetBool("isAttacking", false);
        //Debug.Log("atk_end()");
    }

    private void left_atk_hit() {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(left_hit_pos.position, left_hit_boxSize, 0);
        foreach (Collider2D collider in collider2Ds) {
            if (collider.tag == "Player") {
                collider.GetComponent<SimplePlayerController>().ChangeHealth(-1);
            }
        }
    }

    private void right_atk_hit() {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(right_hit_pos.position, right_hit_boxSize, 0);
        foreach (Collider2D collider in collider2Ds) {
            if (collider.tag == "Player") {
                collider.GetComponent<SimplePlayerController>().ChangeHealth(-1);
            }
        }
    }

    public void hit(string greeny_name) {
        greeny_obj=GameObject.Find(greeny_name);
        Destroy(greeny_obj);
    }
}
