using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlaneController : MonoBehaviour
{

    public WorldController worldController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision col)
    {
        
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Destroy(col.gameObject);
        }
        else if(col.gameObject.CompareTag("Coin"))
        {
            Destroy(col.gameObject);
        }
        else if(col.gameObject.CompareTag("Player"))
        {
            PlayerController pc = col.gameObject.GetComponentInParent<PlayerController>();
            worldController.pauseToggle();
            pc.gameOver(false);
        }
    }

}
