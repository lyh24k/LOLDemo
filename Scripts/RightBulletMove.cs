using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightBulletMove : MonoBehaviour
{
    public float rightbulletspeed = 5.0f;
    public float rightbulletLifeTime = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0,0,1) * Time.deltaTime * rightbulletspeed);
        DestroyMe();
    }
    void DestroyMe()
    {
        Destroy(gameObject, rightbulletLifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
