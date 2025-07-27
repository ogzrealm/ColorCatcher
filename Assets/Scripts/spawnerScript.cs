using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class spawnerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;
    float posY = 7f;
    public bool canSpawnBall=true;
    float randomPosX = 8f;
    public float ballSpawnRate;
    [SerializeField]
    GameObject bomb;

    void Start()
    {
        StartCoroutine(ballSpawn());
        StartCoroutine(bombSpawn());    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    IEnumerator ballSpawn ()
    {
        while (canSpawnBall) 
        {
            
            GameObject spawner=Instantiate(ball,new Vector3(Random.Range(-randomPosX,randomPosX),posY,0),Quaternion.identity);
            spawner.transform.SetParent(transform);
            yield return new WaitForSeconds(ballSpawnRate);

        }
    }

    IEnumerator bombSpawn ()
    {
        while (canSpawnBall)
        {
            float randomBombSpawnRate = Random.Range(5f, 10f);
            yield return new WaitForSeconds(randomBombSpawnRate);
            GameObject bombSpawner = Instantiate(bomb, new Vector3(Random.Range(-randomPosX, randomPosX), posY, 0), Quaternion.identity);
            bombSpawner.transform.SetParent(transform);
            
        }
    }

    
}
