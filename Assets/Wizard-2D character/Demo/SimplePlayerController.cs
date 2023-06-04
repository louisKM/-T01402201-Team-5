using UnityEngine;
using UnityEngine.UI;

public class SimplePlayerController : MonoBehaviour
{
    public float movePower = 10f;
    public float jumpPower = 15f; //Set Gravity Scale in Rigidbody2D Component to 5

    private Rigidbody2D rb;
    private Animator anim;
    Vector3 movement;
    private int direction = 1;
    bool isJumping = false;
    private bool alive = true;

    public int player_max_health = 3;
    public int player_current_health;
    public int curse_count = 3;
    bool q_sw = false; 
    public bool g_sw = false; 
    bool w_sw = false;
    Vector3 player_first_position = new Vector3(-15, -1, 0);
    Vector2 lookDirection = new Vector2(1, 0);
    int int_look_Direction = 1;
    string obj_N;
    string obj_tag;
    Vector2 w_middle_position; //꼭지점
    Vector2 w_last_position; //w키 입력 이후 이동될 목표지점 좌표
    int w_para = 0;
    float jump_height = 2.0f; //점프 높이
    float jump_weith = 5.0f; //점프거리
    float w_cooltime = 0.8f; //w쿨타임
    float w_timer = 0.0f; //w쿨타임 타이머
    float g_cooltime = 3.0f; //g쿨타임
    float g_timer = 0.0f; //g쿨타임 타이머
    bool w_cool_sw = false; //w 쿨 스위치
    public Vector2 player_pos;

    //저주해제
    public float displayTime = 2.0f;
    public GameObject dialogBox;
    float timerDisplay;

    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player_current_health = player_max_health;

        dialogBox.SetActive(false);
        timerDisplay = -1.0f;

        rb.MovePosition(player_first_position);
    }

    private void Update()
    {
        player_pos = rb.position;

        Restart();
        if (alive)
        {
            Hurt();
            Die();
            Attack();
            Jump();
            Run();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("isJump", false);
            isJumping = false;
        }
    }

    void Run()
    {
        Vector3 moveVelocity = Vector3.zero;
        anim.SetBool("isRun", false);


        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            direction = -1;
            moveVelocity = Vector3.left;

            transform.localScale = new Vector3(direction, 1, 1);
            if (!anim.GetBool("isJump"))
                anim.SetBool("isRun", true);

        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            direction = 1;
            moveVelocity = Vector3.right;

            transform.localScale = new Vector3(direction, 1, 1);
            if (!anim.GetBool("isJump"))
                anim.SetBool("isRun", true);

        }
        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    void Jump()
    {
        if ((Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0)
        && !anim.GetBool("isJump"))
        {
            isJumping = true;
            anim.SetBool("isJump", true);
            w_last_position.x = player_pos.x + (jump_weith * int_look_Direction); 
            w_last_position.y = player_pos.y;
            w_middle_position.x = player_pos.x + ((jump_weith * int_look_Direction) / 2); 
            w_middle_position.y = player_pos.y + jump_height;
            w_sw = true;
            w_para = 1;
            w_timer = w_cooltime; 
            w_cool_sw = true;

        }
        if (!isJumping)
        {
            return;
        }

        rb.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rb.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetTrigger("attack");
            RaycastHit2D obj_hit = Physics2D.Raycast(rb.position + Vector2.up * 0.2f, lookDirection, 2.0f, LayerMask.GetMask("react_q_obj"));
            if (obj_hit.collider != null)
            {
                obj_N = obj_hit.collider.gameObject.name;
                obj_tag = obj_hit.collider.tag;
            }
            else {; }

            if (obj_tag == "balls")
            {
                if (obj_hit.collider != null)
                {
                    //Debug.Log("keydown Q : " + obj_hit.collider.gameObject);
                    balls judge_ball = obj_hit.collider.GetComponent<balls>();
                    if (judge_ball != null)
                    {
                        if (judge_ball.hit(obj_N))
                        {
                            curse_count = curse_count - 1;
                            if (curse_count == 0)
                            {
                                DisplayDialog();
                            }
                        }
                        else
                        {
                            ChangeHealth(-1);
                            anim.SetTrigger("hurt");
                        }
                    }
                    else {; }
                }
            }
            else if (obj_tag == "greeny")
            {
                if (obj_hit.collider != null)
                {
                    //Debug.Log("keydown Q : " + obj_hit.collider.gameObject);
                    greeny judge_greeny = obj_hit.collider.GetComponent<greeny>();
                    judge_greeny.hit(obj_N);
                }
            }
            else if (obj_tag == "McD")
            {
                if (obj_hit.collider != null)
                {
                    //Debug.Log("keydown Q : " + obj_hit.collider.gameObject);
                    McD judge_mcd = obj_hit.collider.GetComponent<McD>();
                    judge_mcd.hit(1);
                }
            }
            else {; }
        }
    }
    void Hurt()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            anim.SetTrigger("hurt");
            if (direction == 1)
                rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
            else
                rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);
        }
    }
    void Die()
    {
        if (player_current_health <= 0)
        {
            anim.SetTrigger("die");
            alive = false;
            gameManager.GetComponent<gameManager>().GameOver();
        }
        if (transform.position.y < -10)
        {
            gameManager.GetComponent<gameManager>().GameOver();
        }
    }

    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            gameManager.GetComponent<gameManager>().ReStart();
            anim.SetTrigger("idle");
            alive = true;
        }
    }
    public void ChangeHealth(int amount)
    {
        if (!g_sw)
        {
            anim.SetTrigger("hurt");
            if (direction == 1)
                rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
            else
                rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);
            player_current_health = Mathf.Clamp(player_current_health + amount, 0, player_max_health);
            UIhealthBar.instance.UpdateLifeImages(player_current_health);
        }
    }


    public void falling()
    {
        player_current_health = 0;
    }

    private void atk_start()
    {
        //player_Speed=0;
        q_sw = true;
        // Debug.Log("atk_start()");
    }

    private void atk_end()
    {
        q_sw = false;
        //Debug.Log("atk_end()");
    }

    public void DisplayDialog()
    {
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
    }
}