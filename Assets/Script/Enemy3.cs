using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Enemy
{
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        float angle = transform.rotation.eulerAngles.z + 90;
        direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        transform.position += direction * speed * Time.deltaTime;
    }
}
