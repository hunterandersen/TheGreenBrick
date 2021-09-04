using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2Controller : MonoBehaviour
{
    public Transform target;
    private Vector3 desiredPosition;
    private Vector3 REF_VELOCITY = Vector3.zero;
    public Vector3 offset;

    private bool rotateMe;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp("l"))
        {
            rotateMe = true;
        }

    }

    void FixedUpdate()
    {
        if (rotateMe)
        {
            transform.Rotate(Vector3.up, 10f*Time.deltaTime);
        }
        else
        {
            desiredPosition = target.position + target.transform.forward*-8f + offset;
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref REF_VELOCITY, .25f);
            transform.LookAt(target.position + target.forward*2f);
        }
        
    }
}
