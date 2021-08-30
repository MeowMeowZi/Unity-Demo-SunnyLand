using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public LayerMask ground;
    public Collider2D coll, headColl;
    public Text cherryNumber, gemNumber;
    public AudioSource jumpAduio, hurtAduio, cherryAduio, gemAudio, bgmAduio;
    public Transform headPoint;
    public int cherry = 0;
    public int gem = 0;
    public float speed;
    public float jumpForce;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isJump = false;
    private bool isHurt = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Crouch();
        SwitchAnim();
    }

    void FixedUpdate()
    {
        if (!isHurt)
        {
            Movement();
        }
    }

    // 移动
    void Movement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float faceDircetion = Input.GetAxisRaw("Horizontal");

        // 移动
        if (horizontalMove != 0)
        {
            rb.velocity = new Vector2(horizontalMove * speed * Time.deltaTime, rb.velocity.y);
            anim.SetFloat("Running", Mathf.Abs(horizontalMove));
        }

        // 转向
        if (faceDircetion != 0)
        {
            transform.localScale = new Vector2(faceDircetion, transform.localScale.y);
        }

        // 跳跃
        if (Input.GetButton("Jump") && !isJump)
        {
            isJump = true;
            jumpAduio.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
            anim.SetBool("Jumping", true);
        }
    }

    // 动作切换
    void SwitchAnim()
    {
        if (anim.GetBool("Jumping") && rb.velocity.y < 0)
        {
            anim.SetBool("Jumping", false);
            anim.SetBool("Falling", true);
        }
        else if (coll.IsTouchingLayers(ground) && anim.GetBool("Falling"))
        {
            isJump = false;
            anim.SetBool("Falling", false);
        }
        else if (isHurt && Mathf.Abs(rb.velocity.x) < 0.2f)
        {
            isHurt = false;
            anim.SetBool("Hurt", false);
            anim.SetFloat("Running", 0f);
        }
        else if (rb.velocity.y < -2.0f && !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("Falling", true);
        }
    }


    // 收集奖励
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cherry"))
        {
            cherryAduio.Play();
            Destroy(collision.gameObject);
            cherry++;
            cherryNumber.text = cherry.ToString();
        }

        if (collision.CompareTag("Gem"))
        {
            gemAudio.Play();
            Destroy(collision.gameObject);
            gem++;
            gemNumber.text = gem.ToString();
        }

        if (collision.CompareTag("DeadLine"))
        {
            bgmAduio.Stop();
            Invoke("DeadLine", 2.0f);
        }
    }

    // 消灭敌人
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if(anim.GetBool("Falling"))
            {
                enemy.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce) * Time.deltaTime;
                anim.SetBool("Falling", false);
                anim.SetBool("Jumping", true);
            }
            else
            {
                if (transform.position.x < collision.transform.position.x)
                {
                    rb.velocity = new Vector2(-300, jumpForce) * Time.deltaTime;
                }
                else if (transform.position.x > collision.transform.position.x)
                {
                    rb.velocity = new Vector2(300, jumpForce) * Time.deltaTime;
                }
                isHurt = true;
                hurtAduio.Play();
                anim.SetBool("Hurt", true);
            }
        }
    }

    // 下蹲
    void Crouch()
    {
        if (!Physics2D.OverlapCircle(headPoint.position, 0.1f, ground))
        {
            if (Input.GetButton("Crouch"))
            {
                anim.SetBool("Crouch", true);
                headColl.enabled = false;
            }
            else
            {
                anim.SetBool("Crouch", false);
                headColl.enabled = true;
            }
        }
    }

    void DeadLine()
    {
        bgmAduio.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
