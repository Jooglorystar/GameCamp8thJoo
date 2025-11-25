using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private ObjectPool _objectPool;

    public ObjectPool ObjectPool => _objectPool;
    public Commander Commander { get; set; }

    public HeroSpawnManager HeroSpawn { get; set; }
    public DatabaseManager Database { get; set; }
    public int RemainEnemyCount { get; set; }
    public bool IsGameOver { get; private set; }

    public SpawnableTile SelectedSpawnableTile { get; set; }

    public static GameManager Instance
    {
        get => _instance;
        private set => _instance = value;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            _objectPool = GetComponent<ObjectPool>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Time.timeScale = 1f;
        IsGameOver = false;
    }

    public void GameOver()
    {
        IsGameOver = true;
        Time.timeScale = 0f;
        Debug.Log("Game Over!");
    }

    public void CheckGameClear()
    {
        Debug.Log($"Remaining Enemies: {RemainEnemyCount}");
        if (RemainEnemyCount <= 0 && !IsGameOver)
        {
            GameClear();
        }
    }

    private void GameClear()
    {
        IsGameOver = true;
        Time.timeScale = 0f;
        Debug.Log("Game Clear!");
    }
}