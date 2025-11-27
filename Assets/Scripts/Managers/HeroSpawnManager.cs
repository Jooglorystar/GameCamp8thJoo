using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpawnManager : MonoBehaviour
{
    [SerializeField] private int _maxHeroLevel = 2;
    [SerializeField] private int _spawnCost;
    [SerializeField] private UiHeroSpawnPanel _spawnPanel;
    [SerializeField] private UiHeroUpgradePanel _upgradePanel;

    [SerializeField] private int _startGold;

    private Dictionary<int, int> _heroCountPerId;
    private List<Hero> _spawnedHeroes;

    public int SpawnCost => _spawnCost;

    public UiHeroSpawnPanel SpawnPanel => _spawnPanel;
    public UiHeroUpgradePanel UpgradePanel => _upgradePanel;
    private void Awake()
    {
        GameManager.Instance.HeroSpawn = this;
        _spawnPanel = GetComponentInChildren<UiHeroSpawnPanel>();
        _upgradePanel = GetComponentInChildren<UiHeroUpgradePanel>();
        _heroCountPerId = new Dictionary<int, int>();
        _spawnedHeroes = new List<Hero>();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        ResetHeroCounts();
        _spawnPanel.gameObject.SetActive(false);
        _upgradePanel.gameObject.SetActive(false);
    }

    public void SpawnHero()
    {
        if (GameManager.Instance.Gold < _spawnCost) return;

        GameManager.Instance.RefreshGold(-_spawnCost);
        Hero hero = GameManager.Instance.ObjectPool.Get<Hero>("Hero");
        hero.SetHeroData(GameManager.Instance.Database.GetLevel0HeroData(), GameManager.Instance.SelectedSpawnableTile);
        hero.transform.position = GameManager.Instance.SelectedSpawnableTile.transform.position;

        _spawnedHeroes.Add(hero);
        AddHero(hero.Data.heroId);

        GameManager.Instance.SelectedSpawnableTile.Spawn();
        GameManager.Instance.SelectedSpawnableTile = null;

        _spawnPanel.gameObject.SetActive(false);
    }

    public void DespawnHero(Hero p_hero, SpawnableTile p_spawnableTile)
    {
        if (!_spawnedHeroes.Contains(p_hero)) return;

        if (GameManager.Instance.SelectedHero == p_hero)
        {
            GameManager.Instance.SelectedHero = null;
        }
        p_hero.gameObject.SetActive(false);
        _spawnedHeroes.Remove(p_hero);
        p_spawnableTile.Despawn();
        RemoveHero(p_hero.Data.heroId);
    }

    public void UpgradeHero(Hero p_hero)
    {
        if (p_hero == null) return;

        int cachedId = p_hero.Data.heroId;

        _spawnedHeroes.Remove(p_hero);
        RemoveHero(p_hero.Data.heroId);
        switch (p_hero.Data.level)
        {
            case 0:
                p_hero.SetHeroData(GameManager.Instance.Database.GetLevel1HeroData(), p_hero.SpawnableTile);
                break;
            case 1:
                p_hero.SetHeroData(GameManager.Instance.Database.GetLevel2HeroData(), p_hero.SpawnableTile);
                break;
            case 2:
                break;
        }
        AddHero(p_hero.Data.heroId);
        _spawnedHeroes.Add(p_hero);

        foreach (var hero in _spawnedHeroes)
        {
            if (hero.Data.heroId == cachedId)
            {
                DespawnHero(hero, hero.SpawnableTile);
                break;
            }
        }
    }

    public bool CanUpgradeHero(Hero p_hero)
    {
        return _heroCountPerId[p_hero.Data.heroId] >= 2 && p_hero.Data.level < _maxHeroLevel;
    }

    private void ResetHeroCounts()
    {
        _heroCountPerId.Clear();
        for (int i = 0; i < GameManager.Instance.Database.HeroDatas.Length; i++)
        {
            _heroCountPerId.Add(GameManager.Instance.Database.HeroDatas[i].heroId, 0);
        }
    }

    private void AddHero(int p_id)
    {
        if (_heroCountPerId.ContainsKey(p_id))
        {
            _heroCountPerId[p_id]++;
            Debug.Log($"Hero ID: {p_id}, Count: {_heroCountPerId[p_id]}");
            CheckCanUpgrade();
        }

    }

    private void RemoveHero(int p_id)
    {
        if (_heroCountPerId.ContainsKey(p_id))
        {
            _heroCountPerId[p_id]--;
            Debug.Log($"Hero ID: {p_id}, Count: {_heroCountPerId[p_id]}");
            CheckCanUpgrade();
        }
    }

    private void CheckCanUpgrade()
    {
        foreach (var hero in _spawnedHeroes)
        {
            hero.ActivateUpgradeIcon(CanUpgradeHero(hero));
        }
    }
}