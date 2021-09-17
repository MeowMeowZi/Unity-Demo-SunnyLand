using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Collider2D coll;
    protected Animator anim;
    protected new AudioSource audio;
    protected Rigidbody2D rb;

    protected bool isDeath;
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void JumpOn()
    {
        isDeath = true;
        coll.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        audio.Play();
        anim.SetTrigger("Death");
    }
}
