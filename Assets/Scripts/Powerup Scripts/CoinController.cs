using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{

    private float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        //transform.Rotate(Vector3.right, 90f);
        rotationSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up, rotationSpeed);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().addCoin();
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider collider)
    {

    }
}
