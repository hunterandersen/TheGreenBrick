using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderController : MonoBehaviour
{

    private Rigidbody rb;
    public float minVelocityRange;
    public float maxVelocityRange;
    private WorldController worldController;
    private Vector3 currentVelocity;
    private bool isPaused;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        worldController = FindObjectOfType<WorldController>();
        isPaused = false;

        //Another way to do this would be using smaller numbers for the range, then multiplying by 2 or something, so that the minimum is really minimum*2.
        if (Random.Range(-1, 1) < 0)
        {
            rb.velocity = new Vector3(Random.Range(-maxVelocityRange, -minVelocityRange), 0f, 0f);
        }
        else
        {
            rb.velocity = new Vector3(Random.Range(minVelocityRange, maxVelocityRange), 0f, 0f);
        }
        currentVelocity = rb.velocity;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
            pc.slowDown();
            worldController.spawnHorizWall(25f);//Spawn a horizontal wall 40 units ahead of the player.
            Destroy(this.gameObject);
        }
        else
        {
            rb.velocity = -1f*(currentVelocity);
            currentVelocity = rb.velocity;
        }
    }

    public void pauseToggle()
    {
        isPaused = !isPaused;
        if(isPaused)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            rb.velocity = currentVelocity;
        }
    }

}
