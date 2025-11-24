using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    [SerializeField] private float _spawnInterval = 0.5f;
    private float _timer = 0;

    [SerializeField] private WayPoint[] _wayPoints;

    private void Update()
    {
        SpawnTimer();
    }

    private void SpawnTimer()
    {
        _timer += Time.deltaTime;
        if (_timer >= _spawnInterval)
        {
            SpawnEnemy();
            _timer = 0;
        }
    }

    private void SpawnEnemy()
    {
        // Enemy enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity).GetComponent<Enemy>();
        Enemy enemy = GameManager.Instance.ObjectPool.Get<Enemy>("Enemy");

        enemy.InitEnemy(_wayPoints);
    }
}
