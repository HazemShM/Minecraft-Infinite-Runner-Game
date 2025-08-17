
using System.Collections.Generic;
using UnityEngine;

public class SpawnGround : MonoBehaviour
{
    // public GameObject [] emptyAreasPrefabs;
    [SerializeField] private GameObject [] groundPrefabs ;
    [SerializeField] private GameObject wallPrefab;
    private int spawnDistance= 18;


    // public bool generate = true;
    private void Start()
    {
        // generate = true; 
    }
    private void OnTriggerEnter(Collider collider){
        if (collider.gameObject.CompareTag("Destroy") )
        {
            Destroy(collider.transform.parent.gameObject);
        }
        if (collider.gameObject.CompareTag("Spawn"))
        {   
            if(collider.transform.position.x==-0.5){
                // print("I am in middle");
                Vector3 position = collider.transform.parent.position;
                List<int> generatedIndexes = new List<int>();
                // int obstacleCount = 0;
                // int maxObstacles = 2;
                // for (int i = 0; i < 3; i++){
                //     int random = GetRandomBlock(); 
                //     if (random == 2){
                //         if (obstacleCount < maxObstacles){
                //             generatedIndexes.Add(random);
                //             obstacleCount++;
                //         }
                //         else{
                //             random = GetRandomNonObstacle();
                //             generatedIndexes.Add(random);
                //         }
                //     }else{
                //         generatedIndexes.Add(random);
                //     }
                // }
                for (int i = 0; i < 3; i++){
                    int random = GetRandomBlock(); 
                    generatedIndexes.Add(random);
                }
                Instantiate(groundPrefabs[generatedIndexes[0]] ,new Vector3(position.x-1,position.y,position.z+spawnDistance+1), Quaternion.identity);
                Instantiate(groundPrefabs[generatedIndexes[1]],new Vector3(position.x,position.y,position.z+spawnDistance+1), Quaternion.identity);
                Instantiate(groundPrefabs[generatedIndexes[2]],new Vector3(position.x-2,position.y,position.z+spawnDistance+1), Quaternion.identity);

            }
        }
        if (collider.gameObject.CompareTag("SpawnWall"))
        {   
            Vector3 position = collider.transform.parent.position;
            // print(position.x);
            if(position.x >= 0.5f){
                Instantiate(wallPrefab, new Vector3(0.5f,position.y,position.z+spawnDistance), Quaternion.identity); 
            }else if(position.x<=-3.5f){
                Instantiate(wallPrefab, new Vector3(-3.5f,position.y,position.z+spawnDistance), Quaternion.identity);

            }
            // Instantiate(blocks[0],new Vector3(0,0,0),Quaternion.identity);
        }
    }
    private int GetRandomNonObstacle()
    {
        int random;
        do{
            random = GetRandomBlock();
        }while (random == 2);
        return random;
    }

    // private int GetRandomBlock()
    // {   
    //     float randomValue = Random.Range(0f, 1f);
    //     if (randomValue < normalBlockWeight){
    //         return normalBlockIndex;
    //     }
    //     int randomIndex = Random.Range(0, groundPrefabs.Length);
    //     while (randomIndex == normalBlockIndex){
    //         randomIndex = Random.Range(0, groundPrefabs.Length);
    //     }

    //     return randomIndex;
    // }
    private int GetRandomBlock()
    {   

        float normalBlockWeight = 0.6f;   // 60% chance for a normal block
        float suppliesBlockWeight = 0.01f;    // 10% chance for a boost block
        // float otherBlockWeight = 0.3f;    // 30% chance for any other block
        int suppliesBlockIndex = 4;
        int normalBlockIndex = 0; 
        float randomValue = Random.Range(0f, 1f);

        if (randomValue < normalBlockWeight){
            return normalBlockIndex;
        }
        else if (randomValue < normalBlockWeight + suppliesBlockWeight){
            return suppliesBlockIndex;
        }else{
            int randomIndex = Random.Range(0, groundPrefabs.Length);
            while (randomIndex == normalBlockIndex || randomIndex == suppliesBlockIndex){
                randomIndex = Random.Range(0, groundPrefabs.Length);
            }
            return randomIndex;
        }
    }

     
    
}
