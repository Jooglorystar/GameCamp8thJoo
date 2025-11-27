using System;
using System.Collections.Generic;
using UnityEngine;

public class MineralManager : MonoBehaviour
{
    private List<Miner> _miners;

    [SerializeField] private int _baseMinerCost = 49;
    [SerializeField] private int _maxMinerCount = 20;

    [SerializeField] private UiMinerCount _uiMinerCount;

    [SerializeField] private Transform _rightBasePos;
    [SerializeField] private Transform _leftBasePos;
    [SerializeField] private Transform _rightMineralPos;
    [SerializeField] private Transform _LeftMineralPos;

    public Transform RightBasePos => _rightBasePos;
    public Transform LeftBasePos => _leftBasePos;
    public Transform RightMineralPos => _rightMineralPos;
    public Transform LeftMineralPos => _LeftMineralPos;
    public int FinalMinerCost
    {
        get
        {
            return _baseMinerCost + (_miners.Count * 2);
        }
    }

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
        Mineing();
    }

    private void Mineing()
    {
        for (int i = 0; i < _miners.Count; i++)
        {
            _miners[i].MineTimer();
        }
    }

    private void Init()
    {
        _miners.Clear();
        AddMiner();
        _uiMinerCount.InitInfo(_miners.Count, _maxMinerCount);
    }

    private void AddMiner()
    {
        Miner miner = new Miner(GameManager.Instance.ObjectPool.Get<MinerObject>("Miner"), _miners.Count);
        _miners.Add(miner);
        _uiMinerCount.RefreshCount(_miners.Count);
    }

    public void BuyMiner()
    {
        if (_miners.Count < _maxMinerCount && GameManager.Instance.Gold >= FinalMinerCost)
        {
            AddMiner();
            GameManager.Instance.RefreshGold(-FinalMinerCost);
        }
    }
}

public class Miner
{
    public int position;
    private float _mineInterval;

    private float _timer;

    private MinerObject _obj;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p_position">Â¦¼ö=¿ÞÂÊ, È¦¼ö=¿À¸¥ÂÊ</param>
    public Miner(MinerObject p_obj, int p_position)
    {
        _mineInterval = 7;
        _timer = 0;
        _obj = p_obj;
        if (p_position % 2 == 0)
        {
            p_obj.SetRoute(GameManager.Instance.MineralManager.LeftBasePos, GameManager.Instance.MineralManager.LeftMineralPos);
        }
        else
        {
            p_obj.SetRoute(GameManager.Instance.MineralManager.RightBasePos, GameManager.Instance.MineralManager.RightMineralPos);
        }
    }

    private void Mine()
    {
        GameManager.Instance.RefreshMineral(1);
    }

    public void MineTimer()
    {
        _timer += Time.deltaTime;
        _obj.RefreshVisual(_timer / _mineInterval);

        if (_timer >= _mineInterval)
        {
            Mine();
            _obj.RefreshVisual(0f);
            _timer = 0f;
        }
    }
}
