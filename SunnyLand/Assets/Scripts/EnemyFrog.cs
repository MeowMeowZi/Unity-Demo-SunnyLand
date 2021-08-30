using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrog : Enemy
{
    public Transform leftTransform, rightTransform;
    public LayerMask ground;
    private Rigidbody2D rb;
    private Collider2D coll;
    public float speed, jumpFouce;
    private float leftPoint, rightPoint;
    private bool faceLeft = true;
    private bool isJump = false;
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        transform.DetachChildren();
        leftPoint = leftTransform.position.x;
        rightPoint = rightTransform.position.x;
        Destroy(leftTransform.gameObject);
        Destroy(rightTransform.gameObject);
    }

    void FixedUpdate()
    {
        FaceDirection();
        SwitchAnim();
    }

    // 移动
    void Movement()
    {
        isJump = true;
        if (faceLeft)
        {
            rb.velocity = new Vector2(-speed, jumpFouce);
        }
        else
        {
            rb.velocity = new Vector2(speed, jumpFouce);
        }
    }

    // 敌人方向
    void FaceDirection()
    {
        if (coll.IsTouchingLayers(ground))
        {
            if (transform.position.x < leftPoint)
            {
                transform.localScale = new Vector2(-1, 1);
                faceLeft = false;
            }
            else if (transform.position.x > rightPoint)
            {
                transform.localScale = new Vector2(1, 1);
                faceLeft = true;
            }
        }
    }

    // 切换动画
    void SwitchAnim()
    {
        if (isJump)
        {
            isJump = false;
            anim.SetBool("Jumping", true);
        }
        else if (anim.GetBool("Jumping") && rb.velocity.y < 0f)
        {
            anim.SetBool("Jumping", false);
            anim.SetBool("Falling", true);
        }
        else if (anim.GetBool("Falling") && coll.IsTouchingLayers(ground))
        {
            anim.SetBool("Falling", false);
        }
    }
}
