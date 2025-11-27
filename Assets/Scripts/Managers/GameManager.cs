using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private ObjectPool _objectPool;
    [SerializeField] private UiGameOver _gameoverPanel;

    public ObjectPool ObjectPool => _objectPool;
    public Commander Commander { get; set; }

    public HeroSpawnManager HeroSpawn { get; set; }
    public DatabaseManager Database { get; set; }
    public int RemainAllEnemyCount { get; set; }
    public bool IsGameOver { get; private set; }

    public SpawnableTile SelectedSpawnableTile { get; set; }

    public Hero SelectedHero { get; set; }

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
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        _gameoverPanel.gameObject.SetActive(false);
        IsGameOver = false;
    }

    public void GameOver()
    {
        IsGameOver = true;
        Time.timeScale = 0f;
        _gameoverPanel.GameOver();
    }

    public void CheckGameClear()
    {
        Debug.Log($"Remaining Enemies: {RemainAllEnemyCount}");
        if (RemainAllEnemyCount <= 0 && !IsGameOver)
        {
            GameClear();
        }
    }

    private void GameClear()
    {
        IsGameOver = true;
        Time.timeScale = 0f;
        _gameoverPanel.GameClear();
    }
}