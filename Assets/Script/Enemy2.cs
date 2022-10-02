using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Transform target = LevelManager.Instance.GetTarget();
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 targ = new Vector2(target.position.x, target.position.y);
        Vector2 dir = (targ - pos).normalized;
        rb.velocity = dir * speed;
    }
}
