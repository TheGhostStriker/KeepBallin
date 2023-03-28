using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float spawnDelay = 10f;
    private float firstSpawnDelay = 5f;

    void Start()
    {
        StartCoroutine(SpawnPrefabs());
    }

    IEnumerator SpawnPrefabs()
    {
        // Spawn the first prefab after 5 seconds
        yield return new WaitForSeconds(firstSpawnDelay);
        SpawnPrefab();

        // Spawn subsequent prefabs after 30 seconds
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnPrefab();
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
    }
}




