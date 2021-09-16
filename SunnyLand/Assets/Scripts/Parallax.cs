using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cam;
    public float moveRate;
    public bool lockY;
    private float startPointX, startPointY;


    void Start()
    {
        startPointX = cam.position.x;
        startPointY = cam.position.y;
    }

    void Update()
    {
        if (lockY)
        {
            transform.position = new Vector2(startPointX + cam.position.x * moveRate, transform.position.y);
        }   
        else
        {
            transform.position = new Vector2(startPointX + cam.position.x * moveRate, startPointY + cam.position.y * moveRate);
        }
    }
}
