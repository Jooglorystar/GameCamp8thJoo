using System;
using System.Collections.Generic;
using UnityEngine;

public class MineralManager : MonoBehaviour
{
    private List<Miner> _miners;

    [SerializeField] private int _startMineral;
    [SerializeField] private int _baseMinerCost = 49;
    [SerializeField] private int _maxMinerCount = 20;

    [SerializeField] private UiMinerCount _uiMinerCount;

    private event Action OnMine;

    private void Awake()
    {
        GameManager.Instance.MineralManager = this;
        _miners = new List<Miner>();
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        OnMine?.Invoke();
    }

    private void Init()
    {
        GameManager.Instance.RefreshMineral(_startMineral);
        _miners.Clear();
        AddMiner();
        _uiMinerCount.InitInfo(_miners.Count, _maxMinerCount);
    }

    private void AddMiner()
    {
        Miner miner = new Miner();
        _miners.Add(miner);
        OnMine += miner.MineTimer;
        _uiMinerCount.RefreshCount(_miners.Count);
    }

    public void BuyMiner()
    {
        AddMiner();
        GameManager.Instance.RefreshGold(-GetMinerCost());
    }

    private int GetMinerCost()
    {
        return _baseMinerCost + (_miners.Count * 2);
    }
}

public class Miner
{
    public int position;
    private float _mineInterval;

    private float _timer;

    public Miner()
    {
        _mineInterval = 7;
        _timer = 0;
    }

    private void Mine()
    {
        GameManager.Instance.RefreshMineral(1);
    }

    public void MineTimer()
    {
        _timer += Time.deltaTime;

        if (_timer >= _mineInterval)
        {
            Mine();
            _timer = 0f;
        }
    }
}
