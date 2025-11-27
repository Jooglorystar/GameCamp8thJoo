using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    [SerializeField] private HeroData[] _heroDatas;

    private Dictionary<int, HeroData> _heroDataDict;

    private List<int> _level0HeroIds = new List<int>();
    private List<int> _level1HeroIds = new List<int>();
    private List<int> _level2HeroIds = new List<int>();

    public HeroData[] HeroDatas => _heroDatas;

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
            SetHeroIdList();
        }
    }

    private void SetHeroIdList()
    {
        foreach (var heroData in _heroDatas)
        {
            if (heroData.level == 0)
            {
                _level0HeroIds.Add(heroData.heroId);
            }
            else if (heroData.level == 1)
            {
                _level1HeroIds.Add(heroData.heroId);
            }
            else if (heroData.level == 2)
            {
                _level2HeroIds.Add(heroData.heroId);
            }
        }
    }

    public HeroData GetHeroData(int p_heroId)
    {
        HeroData data = _heroDataDict.TryGetValue(p_heroId, out HeroData heroData) ? heroData : null;
        return data;
    }

    public HeroData GetLevel0HeroData()
    {
        return GetHeroData(_level0HeroIds[Random.Range(0,_level0HeroIds.Count)]);
    }

    public HeroData GetLevel1HeroData()
    {
        return GetHeroData(_level1HeroIds[Random.Range(0,_level1HeroIds.Count)]);
    }

    public HeroData GetLevel2HeroData()
    {
        return GetHeroData(_level2HeroIds[Random.Range(0,_level2HeroIds.Count)]);
    }
}
