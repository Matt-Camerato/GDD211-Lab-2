using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceSpawnSystem : MonoBehaviour
{
    [SerializeField] private GameObject producePrefab;

    [SerializeField] private List<Sprite> Fruits = new List<Sprite>();
    [SerializeField] private List<Sprite> Veggies = new List<Sprite>();

    private float spawnCooldown = 1;
    private float spawnRate = 8;

    void Update()
    {
        if (UI.strikes < 3)
        {
            if (spawnCooldown == 0)
            {
                SpawnProduce();
                spawnCooldown = spawnRate;
                if (spawnRate > 1)
                {
                    spawnRate -= 0.5f;
                }
            }
            else
            {
                spawnCooldown -= Time.deltaTime;
                if (spawnCooldown < 0)
                {
                    spawnCooldown = 0;
                }
            }
        }
    }

    private void SpawnProduce()
    {
        //first determine what to spawn
        Sprite currentProduce;
        bool isFruit = Random.Range(0, 2) == 1;

        if (isFruit)
        {
            currentProduce = Fruits[Random.Range(0, Fruits.Count)];
        }
        else
        {
            currentProduce = Veggies[Random.Range(0, Veggies.Count)];
        }
        
        //next, determine which conveyor to spawn produce at
        int conveyor = Random.Range(-1, 2);

        //lastly, spawn produce and set proper sprite
        GameObject newProduce = Instantiate(producePrefab, new Vector3(4 * conveyor, 8, 0), Quaternion.identity);
        newProduce.GetComponent<SpriteRenderer>().sprite = currentProduce;
        newProduce.GetComponent<ProduceObject>().isFruit = isFruit;
    }
}
