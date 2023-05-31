using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class player : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public float player_Speed = 60f;
    public int player_max_health=3;
    public int player_current_health;
    public int curse_count=3;
    bool q_sw=false; public bool g_sw=false; bool w_sw=false;
    Vector3 player_first_position = new Vector3(-11,-1,0);
    Vector2 lookDirection = new Vector2(1,0);
    int int_look_Direction=1;
    string obj_N;
    string obj_tag;
    Vector2 w_middle_position; //꼭지점
    Vector2 w_last_position; //w키 입력 이후 이동될 목표지점 좌표
    int w_para=0;
    float jump_height=2.0f; //점프 높이
    float jump_weith=5.0f; //점프거리
    float w_cooltime=0.8f; //w쿨타임
    float w_timer=0.0f; //w쿨타임 타이머
    float g_cooltime=3.0f; //g쿨타임
    float g_timer=0.0f; //g쿨타임 타이머
    bool w_cool_sw=false; //w 쿨 스위치
    public Vector2 player_pos;

    //저주해제
    public float displayTime = 2.0f;
    public GameObject dialogBox;
    float timerDisplay;

    public GameObject gameManager;
    //public UIhealthBar healthBar;

    Animator animator;

    void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player_current_health = player_max_health;

        dialogBox.SetActive(false);
        timerDisplay = -1.0f;

        rigidbody2d.MovePosition(player_first_position);
    }

    async void Update() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 position = rigidbody2d.position; //현재위치 백터
        player_pos=position;
        float Speed=player_Speed*7;
        float JumpSpeed = player_Speed;

        Vector2 move = new Vector2(horizontal, vertical);
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }//lookDirection 을 위한 vector 랑 normalize

        if (horizontal==0) {
            animator.SetBool("isMoving", false);
        }
        else if(horizontal < 0 && !w_sw) {
            animator.SetInteger ("direction", -1);
            animator.SetBool("isMoving", true);
            int_look_Direction=-1;
        }
        else if (horizontal > 0 && !w_sw) {
            animator.SetInteger ("direction", 1);
            animator.SetBool("isMoving", true);
            int_look_Direction=1;
        }

        //q키 상호작용
        if (Input.GetKeyDown(KeyCode.Q)) { 
            animator.SetBool("attacking",true);
            RaycastHit2D obj_hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 2.0f, LayerMask.GetMask("react_q_obj"));
            if (obj_hit.collider != null) {
                obj_N=obj_hit.collider.gameObject.name;
                obj_tag = obj_hit.collider.tag;
            } else {;}

            if (obj_tag == "balls" ) {
                if (obj_hit.collider != null) {
                    //Debug.Log("keydown Q : " + obj_hit.collider.gameObject);
                    balls judge_ball = obj_hit.collider.GetComponent<balls>();
                    if (judge_ball != null)
                    {
                        if (judge_ball.hit(obj_N)) {
                            curse_count = curse_count - 1;
                            if (curse_count==0) {
                                DisplayDialog();
                            }
                        } else {
                            ChangeHealth(-1);
                        }
                    } else { ;}
                }
            }
            else if (obj_tag == "greeny") {
                if (obj_hit.collider != null) {
                    //Debug.Log("keydown Q : " + obj_hit.collider.gameObject);
                    greeny judge_greeny = obj_hit.collider.GetComponent<greeny>();
                    judge_greeny.hit(obj_N);
                }
            }
            else if (obj_tag == "McD") {
                if (obj_hit.collider != null) {
                    //Debug.Log("keydown Q : " + obj_hit.collider.gameObject);
                    McD judge_mcd = obj_hit.collider.GetComponent<McD>();
                    judge_mcd.hit(1);
                }
            }
            else {;}
        } else {
            animator.SetBool("attacking", false);
        }

        //w key down
        if (Input.GetKeyDown(KeyCode.W)) {
            if (w_timer <= 0) {
                w_last_position.x=position.x+(jump_weith*int_look_Direction); w_last_position.y=position.y;
                w_middle_position.x=position.x+((jump_weith*int_look_Direction)/2); w_middle_position.y=position.y+jump_height;
                w_sw=true;
                w_para=1;
                w_timer=w_cooltime; w_cool_sw=true;
            }
        }

        if (w_cool_sw) {
            w_timer-=Time.deltaTime;
            if (w_timer<=0) {
                w_cool_sw=false;
            }
        }

        if(w_para==1) {
            transform.position=Vector2.MoveTowards(position,w_middle_position,JumpSpeed*1.25f*Time.deltaTime);
            if (position.y==w_middle_position.y) {w_para=-1;}
        } else if (w_para==-1) {
            transform.position=Vector2.MoveTowards(position,w_last_position, JumpSpeed * 1.25f*Time.deltaTime);
            if (position==w_last_position) {w_para=0; w_sw=false;}
        }
            
        if(!q_sw && !w_sw && !g_sw) { //이동이 이루어지는 곳
            position.x = position.x + (Speed * horizontal * Time.deltaTime);
        }

        //저주해제 띄우기
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                dialogBox.SetActive(false);
            }
        }

        //r키 입력 리셋
        if (Input.GetKeyDown(KeyCode.R)) {
            gameManager.GetComponent<gameManager>().ReStart();
            //Debug.Log("restart");
        }

        //게임오버
        if (player_current_health <= 0) {
            gameManager.GetComponent<gameManager>().GameOver();
        }

        if (g_sw) {
            g_timer-=Time.deltaTime;
            if (g_timer<=0) {
                g_timer=g_cooltime;
                g_sw=false;
            }
        }
    
        if(w_para==0) {rigidbody2d.MovePosition(position);}
    }//update end

    public void ChangeHealth(int amount)
    {
        if (!g_sw) {
            player_current_health = Mathf.Clamp(player_current_health + amount, 0, player_max_health);
            UIhealthBar.instance.UpdateLifeImages(player_current_health);     
        }
    }

    public void falling()
    {
        player_current_health = 0;
    }

    private void atk_start() {
        //player_Speed=0;
        q_sw=true;
       // Debug.Log("atk_start()");
    }

    private void atk_end() {
        q_sw=false;
        //Debug.Log("atk_end()");
    }

    public void DisplayDialog()
    {
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
    }

}//class end