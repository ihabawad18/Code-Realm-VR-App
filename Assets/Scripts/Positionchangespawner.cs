using UnityEngine;
using System.Collections;

public class PositionChangeSpawner : MonoBehaviour
{
    private Vector3 lastPosition;
    private Quaternion lastRotation;  // To store the last rotation
    private bool isWaiting = false;
    private bool hasSpawned = false;
    private bool isReady = false;
    private float movementThreshold = 0.1f;  // Movement threshold to avoid minor inaccuracies
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        StartCoroutine(DelayInitialization());
    }

    void Update()
    {
        if (isReady && !isWaiting && !hasSpawned)
        {
            // Check if the position has significantly changed
            if (Vector3.Distance(transform.position, lastPosition) > movementThreshold)
            {
                // Capture the initial state at the moment of movement
                Vector3 positionToSpawn = lastPosition;
                Quaternion rotationToSpawn = lastRotation;

                Debug.Log($"Movement detected. Spawning will occur at initial position {positionToSpawn} with initial rotation {rotationToSpawn.eulerAngles}");

                // Start the coroutine to spawn after a delay
                StartCoroutine(SpawnAfterDelay(positionToSpawn, rotationToSpawn));
                hasSpawned = true;  // Prevent further spawns
            }
        }
    }

    private IEnumerator DelayInitialization()
    {
        yield return new WaitForSeconds(1);  // Wait before starting checks
        lastPosition = transform.position;  // Set the initial last known position
        lastRotation = transform.rotation;  // Set the initial last known rotation

        Debug.Log($"Initialization complete. Last position: {lastPosition}, Last rotation: {lastRotation.eulerAngles}");

        isReady = true;  // Allow updates and checks to start
    }

    private IEnumerator SpawnAfterDelay(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        isWaiting = true;
        Debug.Log($"Spawning will commence in 2 seconds at position: {spawnPosition}, rotation: {spawnRotation.eulerAngles}");
        yield return new WaitForSeconds(2);  // Wait before spawning

        // Instantiate a duplicate of this GameObject
        Instantiate(this.gameObject, spawnPosition, spawnRotation);
        Debug.Log($"Spawned object at position: {spawnPosition}, rotation: {spawnRotation.eulerAngles}");

        isWaiting = false;
    }
}
