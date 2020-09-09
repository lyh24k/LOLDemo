using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("=====Key Setting=====")]
    public string keyUp = "w";
    public string keyDown = "s";
    public string keyLeft = "a";
    public string keyRight = "d";
    public string keyAttack2 = "q";
    //public string keyLockon = KeyCode.Space;

    [Header("=====Output signals=====")]
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec;
    public float Cup;
    public float Cright;

    public bool attack1;
    private bool lastattack1;

    public bool attack2;
    private bool lastattack2;

    public bool lockon = false;

    [Header("=====Others=====")]
    public bool inputEnabled = true;

    private float targetDup;
    private float targetDright;
    private float velocityDup;
    private float velocityDright;
    private bool isRotated = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            lockon = true;
        }
        if(Input.GetMouseButtonDown(1))
        {
            isRotated = true;
        }
        if(Input.GetMouseButtonUp(1))
        {
            Cright = 0;
            Cup = 0;
            isRotated = false;
        }
        if(isRotated)
        {
            Cright = Input.GetAxis("Mouse X");
            Cup = Input.GetAxis("Mouse Y");
        }
        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);

        if (!inputEnabled)
        {
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

        Vector2 tempVec = Square2Circle(new Vector2(Dright, Dup));
        float Dright2 = tempVec.x;
        float Dup2 = tempVec.y;

        Dmag = Mathf.Sqrt(Dup2 * Dup2 + Dright2 * Dright2);
        Dvec = Dright2 * transform.right + Dup2 * transform.forward;

        bool newattack1 = Input.GetMouseButtonDown(0);
        if(newattack1!=lastattack1&&newattack1==true)
        {
            attack1 = true;
        }
        else
        {
            attack1 = false;
        }
        lastattack1 = newattack1;

        bool newattack2 = Input.GetKey(keyAttack2);
        if (newattack2 != lastattack2 && newattack2 == true)
        {
            attack2 = true;
        }
        else
        {
            attack2 = false;
        }
        lastattack2 = newattack2;
    }
    //斜方向1倍速移动
    private Vector2 Square2Circle(Vector2 input)
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);
        return output;
    }

}
