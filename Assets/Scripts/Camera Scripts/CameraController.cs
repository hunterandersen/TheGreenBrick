using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    private Vector3 desiredPosition;
    private Vector3 REF_VELOCITY = Vector3.zero;
    public Vector3 offset;

    private bool lookingBehind;

    // Start is called before the first frame update
    void Start()
    {
        lookingBehind = false;
        /*
        Vector3 desiredPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref REF_VELOCITY, .25f);
        */
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            lookingBehind = true;
        }
        else if (Input.GetKeyUp("b"))
        {
            lookingBehind = false;
        }

    }

    void FixedUpdate()
    {
        if (lookingBehind)
        {
            desiredPosition = target.position + target.transform.forward*-8f + offset;
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref REF_VELOCITY, 0.0f);
            transform.LookAt(target.position - target.forward*20f);
        }
        else
        {
            desiredPosition = target.position + target.transform.forward*-8f + offset;
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref REF_VELOCITY, .25f);
            transform.LookAt(target.position + target.forward*5f);
        }
        
    }
}
