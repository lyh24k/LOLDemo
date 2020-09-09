using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{

    public GameObject model;
    public CameraController camcon;
    public PlayerInput pi;
    public float runningspeed = 2.0f;

    [SerializeField]
    private Animator anim;
    private Rigidbody rigid;
    private Vector3 movingVec;
    private Vector3 thrustVec;
    private bool lockPlanar = false;
    private float lerpTarget;

    // Start is called before the first frame update
    void Awake()
    {
        pi = GetComponent<PlayerInput>();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("forward", pi.Dmag);
        if (pi.lockon)
        {
            camcon.Lock();
            pi.lockon = false;
        }
        if (pi.attack1&&camcon.lockState == false)
        {
            anim.SetTrigger("attack1");
        }
        if (pi.attack2&& camcon.lockState == false)
        {
            anim.SetTrigger("attack2");
        }

        if (camcon.lockState == true)
        {
            if(pi.attack1)
            {
                anim.SetTrigger("attack1");
            }
            if(pi.attack2)
            {
                anim.SetTrigger("attack2");
            }
            if (!pi.attack1 && !pi.attack2)
            {
                if (pi.inputEnabled == false)
                {
                    Vector3 targetForward = Vector3.Slerp(model.transform.forward, transform.forward, 0.2f);
                    model.transform.forward = targetForward;
                    if (lockPlanar == false)
                    {
                        movingVec = pi.Dmag * model.transform.forward * runningspeed;
                    }
                }
                else
                {
                    if (pi.Dmag > 0.1f)
                    {
                        Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.2f);
                        model.transform.forward = targetForward;
                    }
                    if (lockPlanar == false)
                    {
                        movingVec = pi.Dmag * model.transform.forward * runningspeed;
                    }
                }
            }
            //pi.inputEnabled = false;
            //if (lockPlanar == false)
            //{
               // movingVec = pi.Dvec  * runningspeed;
            //}
        }
        else if(camcon.lockState == false)
        {
            if (pi.Dmag > 0.1f)
            {
                Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.2f);
                model.transform.forward = targetForward;
            }
            if (lockPlanar == false)
            {
                movingVec = pi.Dmag * model.transform.forward * runningspeed;
            }
        }
    }

    private void FixedUpdate()
    {
        //rigid.position += movingVec * Time.fixedDeltaTime;
        rigid.velocity = new Vector3(movingVec.x, rigid.velocity.y, movingVec.z) + thrustVec;
        thrustVec = Vector3.zero;
    }

    public void OnAttack1Enter()
    {
        pi.inputEnabled = false;
        lerpTarget = 1.0f;
        //print(pi.attack1);

    }
    public void OnActtack2Enter()
    {
        pi.inputEnabled = false;
        lerpTarget = 1.0f;
        //anim.SetLayerWeight(anim.GetLayerIndex("attack"), 1.0f);
    }
    public void OnAttack1Update()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("attack"), Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("attack")), lerpTarget, 0.4f));
        //thrustVec = model.transform.forward * anim.GetFloat("Attack1Veclocity");
        //print(anim.GetFloat("Attack1Veclocity"));
    }
    public void OnAttack2Update()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("attack"), Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("attack")), lerpTarget, 0.2f));
        //thrustVec = model.transform.forward * anim.GetFloat("Attack1Veclocity");
        //print(anim.GetFloat("Attack1Veclocity"));
    }

    public void OnAttackIdleEnter()
    {
        pi.inputEnabled = true;
        //anim.SetLayerWeight(anim.GetLayerIndex("attack"), 0.0f);
        lerpTarget = 0f;
       
    }

    public void OnAttackIdleUpdate()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("attack"), Mathf.Lerp(anim.GetLayerWeight(anim.GetLayerIndex("attack")), lerpTarget, 0.2f));
    }
}
