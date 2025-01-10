using System;
using System.Collections;
using System.Collections.Generic;
using MapObjects;
using MapReset;
using UnityEngine;

public class StickFallBall : ResetableObject
{
    private void Start()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer != 6) return;
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    public override void ResetStatus()
    {
        base.ResetStatus();
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }
}
