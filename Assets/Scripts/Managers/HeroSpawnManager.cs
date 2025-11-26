using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpawnManager : MonoBehaviour
{
    [SerializeField] private int _spawnCost;
    [SerializeField] private UiHeroSpawnPanel _spawnPanel;

    [SerializeField] private UiTextGold _uiTextGold;

    [SerializeField] private int _startGold;
    private int _gold;

    [SerializeField] private List<int> _heroes;

    public int SpawnCost => _spawnCost;

    public UiHeroSpawnPanel SpawnPanel => _spawnPanel;
    private void Awake()
    {
        GameManager.Instance.HeroSpawn = this;
        _spawnPanel = GetComponentInChildren<UiHeroSpawnPanel>();
        Init();
    }

    private void Init()
    {
        _gold = _startGold;
        RefreshGold(0);
        _spawnPanel.gameObject.SetActive(false);
    }

    public void SpawnHero()
    {
        if (_gold < _spawnCost) return;

        RefreshGold(-_spawnCost);
        Hero hero = GameManager.Instance.ObjectPool.Get<Hero>("Hero");
        hero.SetHeroData(GameManager.Instance.Database.GetLevel1HeroData());
        hero.transform.position = GameManager.Instance.SelectedSpawnableTile.transform.position;
        GameManager.Instance.SelectedSpawnableTile.HasHero = true;
        GameManager.Instance.SelectedSpawnableTile = null;
        _spawnPanel.gameObject.SetActive(false);
    }

    public void UpGradeHero(Hero p_hero)
    {
        if(p_hero == null) return;

        // p_hero.SetHeroData(GameManager.Instance.Database.GetHeroData(12001));
    }

    public void RefreshGold(int p_amount)
    {
        _gold += p_amount;
        _uiTextGold.RefreshText(_gold);
    }
}