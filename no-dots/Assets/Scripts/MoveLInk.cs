using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MoveLink : MonoBehaviour
{
    public Vector2 targetPos;
    public float speed;
    private bool targetReached;
    
    // Update is called once per frame
    void Update()
    {
        if (targetReached) return;
        
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPos.x, targetPos.y, 0), speed);
    }
}
