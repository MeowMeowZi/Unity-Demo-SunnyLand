using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected Animator anim;
    protected new AudioSource audio;
    protected virtual void Start()
    {
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
