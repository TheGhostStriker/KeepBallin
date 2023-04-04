using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float spawnDelay = 10f;
    private float firstSpawnDelay = 5f;
    public int maxNumBalls = 7;

    private int numBalls = 0;

    void Start()
    {
        StartCoroutine(SpawnPrefabs());
    }

    IEnumerator SpawnPrefabs()
    {
        // Spawn the first prefab after 5 seconds
        yield return new WaitForSeconds(firstSpawnDelay);
        SpawnPrefab();

        // Spawn subsequent prefabs after a delay
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);

            // If the number of balls is less than the maximum limit, spawn a new ball
            if (numBalls < maxNumBalls)
            {
                SpawnPrefab();
            }
        }
    }

    void SpawnPrefab()
    {
        // Generate a random direction for the prefab to move in
        Vector3 direction = Random.insideUnitCircle.normalized;

        // Instantiate the prefab and give it a random direction
        GameObject newPrefab = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        Rigidbody prefabRigidbody = newPrefab.GetComponent<Rigidbody>();
        prefabRigidbody.AddForce(direction, ForceMode.Impulse);

        // Increment the number of balls
        numBalls++;
    }

    // Method to decrement the number of balls
    public void DecrementNumBalls()
    {
        numBalls--;
    }
}




