using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] float spawnTimer;
    [SerializeField] float spawnTime;
    [SerializeField] GameObject enemy;
    [SerializeField] private Vector3 minPosition;
    [SerializeField] private Vector3 maxPosition;

    private void Start()
    {
        spawnTimer = 0.0f;
    }

    private void Update()
    {
        Spawn();

        if (spawnTimer <= spawnTime)
        {
            spawnTimer += Time.deltaTime;   //タイマーがspawnTimeを超えていないならタイマーを進める
        }
    }

    private void Spawn()
    {
        if (spawnTimer >= spawnTime)
        {
            float x = Random.Range(minPosition.x, maxPosition.x);
            float y = Random.Range(minPosition.y, maxPosition.y);
            float z = Random.Range(minPosition.z, maxPosition.z);

            Vector3 randomPos = new Vector3(x, y, z);

            Instantiate(enemy, randomPos, Quaternion.identity);
            spawnTimer = 0.0f;  //タイマーがspawnTimeを超えたら0に戻す
        }
    }
}