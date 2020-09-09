using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public PlayerInput pi;
    public ActorController gunmodel;
    public GameObject leftbullet = null;
    public GameObject leftGunPosition = null;
    public GameObject rightbullet = null;
    public GameObject rightGunPosition = null;
    public float leftBulletDemage = 2.0f;
    public float rightBulletDemage = 10.0f;
    //public float reload = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pi.attack1)
        {
            shoot();
        }
        if(pi.attack2)
        {
            Invoke("shoot2", 0.2f);
        }
    }

    private void shoot()
    {
        GameObject lbullet = Instantiate(leftbullet, leftGunPosition.transform.position, gunmodel.model.transform.rotation);
       // GameObject rbullet = Instantiate(rightbullet, rightGunPosition.transform.position, gunmodel.model.transform.rotation);
    }
    private void shoot2()
    {
        //GameObject lbullet = Instantiate(leftbullet, leftGunPosition.transform.position, gunmodel.model.transform.rotation);
        GameObject rbullet = Instantiate(rightbullet, rightGunPosition.transform.position, gunmodel.model.transform.rotation);
    }
}
