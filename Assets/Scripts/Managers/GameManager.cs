using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private ObjectPool _objectPool;
    [SerializeField] private UiGameOver _gameoverPanel;

    [SerializeField] private UiTextGold _uiTextGold;
    [SerializeField] private UiTextMineral _uiTextMineral;

    [SerializeField] private int _startGold;
    [SerializeField] private int _startMineral;
    public ObjectPool ObjectPool => _objectPool;
    public Commander Commander { get; set; }

    public HeroSpawnManager HeroSpawn { get; set; }
    public DatabaseManager Database { get; set; }
    public MineralManager MineralManager { get; set; }
    public int RemainAllEnemyCount { get; set; }
    public bool IsGameOver { get; private set; }

    public SpawnableTile SelectedSpawnableTile { get; set; }

    public Hero SelectedHero { get; set; }

    public UiFixTilePanel FixPanel { get; set; }

    public int Gold { get; set; }
    public int Mineral { get; set; }

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
        Gold = _startGold;
        RefreshGold(0);
        Mineral = _startMineral;
        RefreshMineral(0);
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
    public void RefreshGold(int p_amount)
    {
        Gold += p_amount;
        _uiTextGold.RefreshText(Gold);
    }
    public void RefreshMineral(int p_amount)
    {
        Mineral += p_amount;
        _uiTextMineral.RefreshText(Mineral);
    }

    private void GameClear()
    {
        IsGameOver = true;
        Time.timeScale = 0f;
        _gameoverPanel.GameClear();
    }
}