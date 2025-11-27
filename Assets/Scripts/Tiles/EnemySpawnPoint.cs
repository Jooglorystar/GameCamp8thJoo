using System.Collections;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private float _spawnInterval = 0.5f;
    [SerializeField] private float _waveInterval = 10f;

    [SerializeField] private WaveData[] _waves;
    [SerializeField] private WayPoint[] _wayPoints;

    [SerializeField] private Transform _spawnPosition;

    private int _currentWaveIndex = 0;
    private int _spawnedEnemyCount = 0;

    [SerializeField] private UiWaveTimer _uiWaveTimer;

    private float _waveTimer;
    private float _spawnTimer;

    private bool _spawning;

    private void Awake()
    {
        _currentWaveIndex = 0;
        _uiWaveTimer = GetComponentInChildren<UiWaveTimer>();
        _uiWaveTimer.gameObject.SetActive(false);
    }
    private void Start()
    {
        _waveTimer = _waveInterval;
        GameManager.Instance.RemainAllEnemyCount = GetTotalEnemyCountInAllWaves();
    }

    private void Update()
    {
        WaveTimer();
    }

    private void WaveTimer()
    {
        if (_currentWaveIndex >= _waves.Length) return;


        if (_spawning)
        {
            SpawnTimer();
        }
        else
        {
            if(_uiWaveTimer.gameObject.activeInHierarchy == false)
            {
                _uiWaveTimer.gameObject.SetActive(true);
            }
            _waveTimer -= Time.deltaTime;
            _uiWaveTimer.UpdateTimerUI((_waveTimer / _waveInterval), _waveTimer);
            if (_waveTimer <= 0f)
            {
                _uiWaveTimer.gameObject.SetActive(false);
                _spawnedEnemyCount = 0;
                _spawning = true;
                _spawnTimer = 0;
                Debug.Log($"Start {_currentWaveIndex} Wave");
            }
        }
    }

    private void SpawnTimer()
    {
        if (!_spawning) return;

        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer <= 0f && _spawnedEnemyCount < _waves[_currentWaveIndex].AllEnemyCountInWave)
        {
            SpawnEnemy();
            _spawnedEnemyCount++;
            _spawnTimer = _spawnInterval;
        }

        if(_spawnedEnemyCount >= _waves[_currentWaveIndex].AllEnemyCountInWave)
        {
            Debug.Log($"{_currentWaveIndex} Wave completed");
            _spawning = false;
            _currentWaveIndex++;
            _waveTimer = _waveInterval;
        }
    }

    private int GetTotalEnemyCountInAllWaves()
    {
        int allEnemyCountInWaves = 0;
        for (int i = 0; i < _waves.Length; i++)
        {
            allEnemyCountInWaves += _waves[i].AllEnemyCountInWave;
        }
        return allEnemyCountInWaves;
    }

    private void SpawnEnemy()
    {
        Enemy enemy = GameManager.Instance.ObjectPool.Get<Enemy>("Enemy");
        enemy.transform.position = _spawnPosition.position;

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