﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public float speed = 1f;
    Vector2 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((new Vector2(-1, 0)) * speed * Time.deltaTime);
        if(transform.position.x < -2.2f) {
            transform.position = startPos;
        }
    }
}
