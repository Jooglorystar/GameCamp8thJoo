using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    [SerializeField] private HeroData[] _heroDatas;

    private Dictionary<int, HeroData> _heroDataDict;

    private void Awake()
    {
        GameManager.Instance.Database = this;
        Init();
    }

    private void Init()
    {
        _heroDataDict = new Dictionary<int, HeroData>();

        for (int i = 0; i < _heroDatas.Length; i++)
        {
            _heroDataDict.Add(_heroDatas[i].heroId, _heroDatas[i]);
        }
    }

    public HeroData GetHeroData(int p_heroId)
    {
        HeroData data = _heroDataDict.TryGetValue(p_heroId, out HeroData heroData) ? heroData : null;
        return data;
    }
}
