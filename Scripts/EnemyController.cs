using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Bullet bullet;
    public float enemyHealth = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHealth<=0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="bullet")
        {
            enemyHealth -= bullet.leftBulletDemage;
            Destroy(other.gameObject);
        }
    }
}
