using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEagle : Enemy
{
    public Transform topTransform, buttonTransform;
    public float speed;
    private float topPoint, buttonPoint;
    private bool topFly = true;

    protected override void Start()
    {
        base.Start();
        transform.DetachChildren();
        topPoint = topTransform.position.y;
        buttonPoint = buttonTransform.position.y;
        Destroy(topTransform.gameObject);
        Destroy(buttonTransform.gameObject);
    }

    void FixedUpdate()
    {
        if (!isDeath)
        {
            Movement();
        }
    }

    void Movement()
    {
        if (topFly)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
            if (transform.position.y > topPoint)
            {
                topFly = false;
            }
        }
        else if (!topFly)
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
            if (transform.position.y < buttonPoint)
            {
                topFly = true;
            }
        }
    }


}
