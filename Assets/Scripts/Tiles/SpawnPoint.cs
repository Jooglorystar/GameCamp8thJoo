using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private float _spawnInterval = 0.5f;
    [SerializeField] private float _waveInterval = 15f;

    private float _timer = 0;

    [SerializeField] private WaveData[] _enemyWaves;
    [SerializeField] private WayPoint[] _wayPoints;

    private int _currentWaveIndex = 0;
    private bool _isWaveRunning = false;

    private WaitForSeconds _waveWait;
    private WaitForSeconds _spawnWait;



    private void Awake()
    {
        _currentWaveIndex = 0;
        _waveWait = new WaitForSeconds(_waveInterval);
        _spawnWait = new WaitForSeconds(_spawnInterval);
    }
    private void Start()
    {
        StartCoroutine(WaveCoroutine());
    }

    private IEnumerator WaveCoroutine()
    {
        yield return _waveWait;

        GameManager.Instance.RemainEnemyCount = GetTotalEnemyCountInAllWaves();
        while (_currentWaveIndex < _enemyWaves.Length)
        {
            _isWaveRunning = true;
            WaveData wave = _enemyWaves[_currentWaveIndex];
            Debug.Log($"{_currentWaveIndex + 1} Wave starts");
            foreach (EnemyWave enemyWave in wave.enemyWaves)
            {
                for (int i = 0; i < enemyWave.enemyCount; i++)
                {
                    SpawnEnemy();
                    yield return _spawnWait;
                }
            }
            _isWaveRunning = false;
            Debug.Log($"{_currentWaveIndex + 1} Wave ends");

            yield return _waveWait;

            _currentWaveIndex++;
        }
        Debug.Log("All waves completed!");
    }

    private int GetTotalEnemyCountInAllWaves()
    {
        int allEnemyCountInWaves = 0;
        for (int i = 0; i < _enemyWaves.Length; i++)
        {
            allEnemyCountInWaves += _enemyWaves[i].AllEnemyCountInWave;
        }
        return allEnemyCountInWaves;
    }

    private void SpawnEnemy()
    {
        Enemy enemy = GameManager.Instance.ObjectPool.Get<Enemy>("Enemy");

        enemy.InitEnemy(_wayPoints);
    }
}

[System.Serializable]
public class EnemyWave
{
    public int enemyCount;
}

[System.Serializable]
public class WaveData
{
    private int _allEnemyCount;
    public int AllEnemyCountInWave
    {
        get
        {
            if (_allEnemyCount <= 0)
            {
                _allEnemyCount = 0;
                for (int i = 0; i < enemyWaves.Length; i++)
                {
                    _allEnemyCount += enemyWaves[i].enemyCount;
                }
            }
            return _allEnemyCount;
        }
    }

    public EnemyWave[] enemyWaves;
}