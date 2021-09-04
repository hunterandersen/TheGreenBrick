using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{

    Rigidbody rb;
    private float xInput;
    private float slowedStartTime;
    private bool movingForward;//might be unnecessary. I think the player is always moving forward.
    private bool isPaused;
    private bool movingLeft;
    private bool movingRight;
    [SerializeField] private int slowed;
    [SerializeField] private float slowedIntensity;
    [SerializeField] private int coinCount;
    [SerializeField] private Vector3 xVelocity;
    private Vector3 fowardVelocity;
    public float playerSpeed;
    public float forwardSpeed;
    public float slowDuration;
    public GameObject deathPlane;
    private Rigidbody deathPlaneRB;
    public TMP_Text centerScreenText;
    public TMP_Text topLeftText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        deathPlaneRB = deathPlane.GetComponent<Rigidbody>();
        movingForward = true;
        isPaused = false;
        slowedIntensity = 0;
        topLeftText.SetText("<color=yellow>Coins: </color>");
        deathPlaneRB.position = transform.position - new Vector3(0f, 0f, 20f);//start the deathPlane 20f behind the player;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPaused)
        {
            xInput = Input.GetAxisRaw("Horizontal");

            if(xInput != 0)
            {
                xVelocity = transform.right*playerSpeed*xInput;
                if(slowed>0)
                {
                    xVelocity *= slowedIntensity;
                    if(Time.time > slowedStartTime + slowDuration)
                    {
                        slowed--;
                        updateSlowedIntensity(slowed);
                        if(slowed > 0)
                        {
                            slowedStartTime = Time.time;
                        }
                        else{
                            slowedStartTime = 0;
                        }
                    }
                }
                movingLeft = xInput<0;
                movingRight = xInput>0;
            }
            else
            {
                xVelocity = Vector3.zero;
                movingLeft = false;
                movingRight = false;
            }
            
            if(movingForward)
            {
                fowardVelocity = transform.forward*forwardSpeed;
            }
            else
            {
                fowardVelocity = Vector3.zero;
            }

            rb.velocity = xVelocity + fowardVelocity;//move the player

            deathPlaneRB.velocity = rb.velocity;//move the trailing death plane to remove stuff behind the player
            
        }
        else{
            rb.velocity = Vector3.zero;
            deathPlaneRB.velocity = Vector3.zero;
        }
        
    }

    public void OnCollisionEnter(Collision col)
    {
        
    }

    public void slowDown()
    {
        if (slowed < 3)
        {
            slowed++;
        }
        updateSlowedIntensity(slowed);
        if(slowedStartTime==0)
        {
            slowedStartTime = Time.time;
        }
    }

    public void addCoin()
    {
        if(slowed > 0)
        {
            slowed--;
        }
        else if(coinCount >= 10)
        {
            coinCount = 10;
        }
        else
        {
            coinCount++;
            forwardSpeed++;
            playerSpeed+=(playerSpeed/10f);
            string text = "<color=yellow>Coins: " + coinCount + "</color>";
            topLeftText.SetText(text);
        }

    }

    private void updateSlowedIntensity(int a)
    {
        switch(a)
        {
            case 1: slowedIntensity = .8f; break;
            case 2: slowedIntensity = .7f; break;
            case 3: slowedIntensity = .5f; break;
            default: slowedIntensity = 0f; break;
        }
    }

    public void pauseToggle()
    {
        isPaused = !isPaused;
        string endText = "";
        if(isPaused)
        {
            endText = "<color=#ff00ffff>PAUSED</color>";
        }
        else
        {
            endText = "";
        }
        centerScreenText.SetText(endText);
    }

    public void gameOver(bool playerWon)
    {
        string endText = "";
        isPaused = true;
        if (playerWon)
        {
            endText = "<color=green>Player Won!</color>";
        }
        else
        {
            endText = "<color=red>Player Lost!</color>";
        }
        centerScreenText.SetText(endText);
    }
}
