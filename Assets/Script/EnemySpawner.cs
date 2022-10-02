using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval;
    [SerializeField] private GameObject monster;
    
    private float timeElapsed = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > spawnInterval)
        {
            Transform target = LevelManager.Instance.GetTarget();
            Vector3 dir = target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            GameObject newEnemy = Instantiate(monster, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
            LevelManager.Instance.enemies.Add(newEnemy.GetComponent<Enemy>());
            timeElapsed = 0;
        }
    }
}
