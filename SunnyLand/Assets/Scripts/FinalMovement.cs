using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public float speed, jumpForce, hurtForce;
    public Collider2D coll, headColl;
    public Transform groundCheck, headCheck;
    public AudioSource jumpAudio, hurtAudio, cherryAudio, gemAudio, bgmAudio;
    public Text cherryNumber, gemNumber;
    public LayerMask ground;

    private bool isGround, isHead, isCrouch, isHurt;
    private bool jumpPressed;
    private int jumpCount;
    private int cherry = 0, gem = 0;
    private float speedTemp;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        speedTemp = speed - 2;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }

        if (Input.GetButton("Crouch"))
        {
            isCrouch = true;
        }
        else
        {
            isCrouch = false;
        }
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        isHead = Physics2D.OverlapCircle(headCheck.position, 0.1f, ground);

        if (!isHurt)
        {
            GroundMovement();
        }
        Jump();
        Crouch();
        SwitchAnim();
    }

    // 地面移动
    void GroundMovement()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);

        if (horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }
    }

    // 跳跃
    void Jump()
    {
        if (isGround)
        {
            jumpCount = 2;
        }
        
        if (jumpPressed && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
    }

    // 下蹲
    void Crouch()
    {
        if (!isHead)
        {
            if (isCrouch)
            {
                headColl.enabled = false;
                speed = speedTemp;
            }
            else
            {
                headColl.enabled = true;
                speed = speedTemp + 2;
            }
        }
    }

    // 动画切换
    void SwitchAnim()
    {
        anim.SetFloat("Running", Mathf.Abs(rb.velocity.x)); 

        if (isGround && !isHurt)
        {
            anim.SetBool("Falling", false);
        }
        else if (!isGround && rb.velocity.y > 0 && !isHurt)
        {
            anim.SetBool("Falling", false);
            anim.SetBool("Jumping", true);
        }
        else if (isHurt && Mathf.Abs(rb.velocity.x) < 0.2f)
        {
            isHurt = false;
            anim.SetBool("Hurt", false);
            anim.SetFloat("Running", 0f);
        }
        else if (rb.velocity.y < 0)
        {
            anim.SetBool("Jumping", false);
            anim.SetBool("Falling", true);
        }

        if (!isHead)
        {
            if (isCrouch)
            {
                anim.SetBool("Crouch", true);
            }
            else
            {
                anim.SetBool("Crouch", false);
            }
        }
    }

    // 收集物品
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cherry"))
        {
            cherryAudio.Play();
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
            bgmAudio.Stop();
            Invoke("DeadLine", 2.0f);
        }
    }

    // 消灭敌人
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (anim.GetBool("Falling"))
            {
                enemy.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                anim.SetBool("Falling", false);
                anim.SetBool("Jumping", true);
            }
            else
            {
                if (transform.position.x < collision.transform.position.x)
                {
                    rb.velocity = new Vector2(-5, jumpForce);
                }
                else if (transform.position.x > collision.transform.position.x)
                {
                    rb.velocity = new Vector2(5, jumpForce);
                }
                isHurt = true;
                hurtAudio.Play();
                anim.SetBool("Hurt", true);
            }
        }
    }

    void DeadLine()
    {
        bgmAudio.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
