using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyTypes;
    public int enemiesToSpawnAtOnce;
    public int enemiesRemaining;
    public float timer;
    private float counter;
    public Vector2 vector2;
    public float spawnRadius;

    void Update()
    {
        if (enemiesRemaining == 0)
        {
            return;
        }
        counter += Time.deltaTime;
        if (counter > timer)
        {
            counter = 0;
            float randomX = Random.Range(0, spawnRadius);
            float randomY = Random.Range(0, spawnRadius);
            Vector3 target = new Vector3(randomX, randomY,0);
            for (int i = 0; i < enemiesToSpawnAtOnce; i++)
            {
                Instantiate(enemyTypes[Random.Range(0, enemyTypes.Length)], target, Quaternion.identity);
                enemiesRemaining--;
            }
        }
    }
}
