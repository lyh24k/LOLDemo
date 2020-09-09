using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public PlayerInput pi;
    public float horizontalSpeed = 100.0f;
    public float verticalSpeed = 50.0f;
    public float cameraDampValue = 0.05f;
    public Image lockDot;
    public bool lockState;

    private GameObject playerHandle;
    private GameObject cameraHandle;
    private float tempEulerX;
    private GameObject model;
    private new GameObject camera;
    private Vector3 cameraDampVec;
    [SerializeField]
    private GameObject lockTarget;

    // Start is called before the first frame update
    void Awake()
    {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        tempEulerX = 20.0f;
        model = playerHandle.GetComponent<ActorController>().model;
        camera = Camera.main.gameObject;
        lockDot.enabled = false;
        lockState = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(lockTarget==null)
        {
            Vector3 tempModelEuler = model.transform.eulerAngles;

            playerHandle.transform.Rotate(Vector3.up, pi.Cright * Time.fixedDeltaTime * horizontalSpeed);
            //cameraHandle.transform.Rotate(Vector3.right, -pi.Cup * Time.deltaTime * verticalSpeed);
            tempEulerX -= pi.Cup * Time.fixedDeltaTime * verticalSpeed;
            tempEulerX = Mathf.Clamp(tempEulerX, -40, 30);
            cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);

            model.transform.eulerAngles = tempModelEuler;
        }
        else
        {
            Vector3 tempForward = lockTarget.transform.position - model.transform.position;
            tempForward.y = 0;
            playerHandle.transform.forward = tempForward;
        }

        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, transform.position, ref cameraDampVec, cameraDampValue);
        camera.transform.eulerAngles = transform.eulerAngles;
    }

    public void Lock()
    {
        //try to lock
        Vector3 modelOrigin1 = model.transform.position;
        Vector3 modelOrigin2 = modelOrigin1 + new Vector3(0, 1, 0);
        Vector3 boxCenter = modelOrigin2 + model.transform.forward * 5.0f;
        //Collider[] cols = Physics.OverlapSphere(model.transform.position, 30.0f, LayerMask.GetMask("Enemy"));
        Collider[] cols = Physics.OverlapBox(boxCenter, new Vector3(0.5f, 0.5f, 5f), model.transform.rotation, LayerMask.GetMask("Enemy"));
        if(cols.Length==0)
        {
            lockTarget = null;
            lockDot.enabled = false;
            lockState = false;
        }
        else
        {
            foreach (var col in cols)
            {
                if(lockTarget==col.gameObject)
                {
                    lockTarget = null;
                    lockDot.enabled = false;
                    lockState = false;
                    
                    break;
                }
                lockTarget = col.gameObject;
                lockDot.enabled = true;
                lockState = true;
                //lockDot.transform.position = new Vector3(lockDot.transform.position.x, lockTarget.transform.position.y, lockTarget.transform.position.z);
                //print(col.name);
                break;
            }
        }
    }

}
