using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{

    [Header("Private Vars")]
    private float minXSpawn;
    private float minZSpawn;
    private float maxXSpawn;
    private float maxZSpawn;
    private float playerCheckpoint;
    private bool isPaused;
    
    [Header("Public World Options")]
    public float spawnDistance;
    public float nearestSpawn;
    public float numWalls;
    public float numCubes;
    public float numCylinders;
    public int numCoins;
    public float gameWinDistance;
    
    [Header("Player Controllers")]
    public PlayerController playerController;
    private Transform player;

    [Header("Enemy Controllers")]
    public WallController wallController;
    public CubeController cubeController;
    public SphereController sphereController;
    public CylinderController cylinderController;
    
    [Header("Powerup Controllers")]
    public CoinController coinController;

    [Header("Collections")]
    private List<CylinderController> cylinders = new List<CylinderController>();

    // Start is called before the first frame update
    void Start()
    {
        playerCheckpoint = 0f;
        player = playerController.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("p"))
        {
            pauseToggle();
            playerController.pauseToggle();
        }
        if (!isPaused)
        {
            if (player.position.z > gameWinDistance)
            {
                pauseToggle();
                //playerController.pauseToggle();
                playerController.gameOver(true);//Player Wins!
            }
            else if (player.position.z > playerCheckpoint)
            {
                minXSpawn = player.position.x - spawnDistance;
                minZSpawn = player.position.z + nearestSpawn;
                maxXSpawn = player.position.x + spawnDistance;
                maxZSpawn = player.position.z + (spawnDistance+nearestSpawn);

                spawnEnemies(minXSpawn, minZSpawn, maxXSpawn, maxZSpawn);
                spawnCoins(minXSpawn, minZSpawn-(nearestSpawn/2), maxXSpawn, maxZSpawn-nearestSpawn, numCoins);
                playerCheckpoint += 50f;
                numWalls++;
                if(numWalls%5==0)
                {
                    //spawnDistance -= 10f;
                    numCubes += 6;
                    numCylinders+=2;
                }
            }
        }

    }

    public void spawnEnemies(float minX, float minZ, float maxX, float maxZ)
    {
        Vector3 spawnPoint;
        for (int x = 0; x < numWalls; x++)
        {
            spawnPoint = new Vector3(Random.Range(minX, maxX), 0f, Random.Range(minZ, maxZ));
            WallController wc = Instantiate(wallController, spawnPoint, Quaternion.identity);
            //walls.Add(wc);
        }
        for (int x = 0; x < numCubes; x++)
        {
            spawnPoint = new Vector3(Random.Range(minX, maxX), 0f, Random.Range(minZ, maxZ));
            CubeController cc = Instantiate(cubeController, spawnPoint, Quaternion.identity);
        }

        for (int x = 0; x < numCylinders; x++)
        {
            spawnPoint = new Vector3(Random.Range(minX, maxX), 0f, Random.Range(minZ, maxZ));
            CylinderController cylC = Instantiate(cylinderController, spawnPoint, Quaternion.Euler(90f, 0f, 0f));
            cylinders.Add(cylC);
        }

        spawnPoint = new Vector3(player.position.x, 0f, Random.Range(minZ+(minZ/10), maxZ));
        SphereController sc = Instantiate(sphereController, spawnPoint, Quaternion.identity);

    }

    public void spawnHorizWall(float spawnDistanceZ)
    {
        Vector3 spawnPoint;
        spawnPoint = new Vector3(player.position.x, 0f, player.position.z + spawnDistanceZ);
        CubeController cc = Instantiate(cubeController, spawnPoint, Quaternion.identity);
        cc.transform.localScale = new Vector3(cc.transform.localScale.x*6f, cc.transform.localScale.y, cc.transform.localScale.z);

    }

    public void spawnCoins(float minX, float minZ, float maxX, float maxZ, int coinsToSpawn)
    {
        Vector3 spawnPoint;
        for (int i = 0; i < coinsToSpawn; i++)
        {
            spawnPoint = new Vector3(Random.Range(minX, maxX), 0f, Random.Range(minZ, maxZ));
            CoinController cc = Instantiate(coinController, spawnPoint, Quaternion.identity);
        }
    }

    public void pauseToggle()
    {
        isPaused = !isPaused;
        for(int i = 0; i < cylinders.Count; i++)
        {
            if(cylinders[i] != null)
            {
                cylinders[i].pauseToggle();
            }
        }
    }
}
