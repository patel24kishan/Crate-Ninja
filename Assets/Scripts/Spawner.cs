using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    private Collider spawnArea;
    public GameObject[] fruitprefabs;

    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1.25f;

    public float minAngle = -15f;
    public float maxAngle = 15f;

    public float minForce = 18f;
    public float maxForce = 22f;

    public float maxLifeTime = 5f;

    private void Awake()
    {
        spawnArea=GetComponent<Collider>();    
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnRoutine());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(2f);
        while(enabled)
        {
            GameObject fruitSelected = fruitprefabs[Random.Range(0, fruitprefabs.Length)];


            Vector3 fruitPosition = new Vector3();
            fruitPosition.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            fruitPosition.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            fruitPosition.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            Quaternion fruitRotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

            GameObject fruit = Instantiate(fruitSelected, fruitPosition, fruitRotation);

            float force = Random.Range(minForce, maxForce);
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force*Time.deltaTime, ForceMode.Impulse);
            Destroy(fruit, maxLifeTime);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay,maxSpawnDelay));
        }
    }
}
