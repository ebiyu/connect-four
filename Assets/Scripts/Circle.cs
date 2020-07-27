﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
	[System.NonSerialized] public float targetY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.localPosition;
        if(pos.y > targetY){
            pos.y -= 5;
            transform.localPosition = pos;
        }
    }
}
