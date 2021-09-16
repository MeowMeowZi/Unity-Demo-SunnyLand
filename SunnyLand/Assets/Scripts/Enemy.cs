using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Collider2D coll;
    protected Animator anim;
    protected new AudioSource audio;
    protected virtual void Start()
    {
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
        audio.Play();
        anim.SetTrigger("Death");
    }
}
