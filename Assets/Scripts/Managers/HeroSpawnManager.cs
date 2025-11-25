using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpawnManager : MonoBehaviour
{
    [SerializeField] private int _spawnCost;
    [SerializeField] private UiHeroSpawnPanel _spawnPanel;

    [SerializeField] private UiTextGold _uiTextGold;

    private int _gold;

    // public int Gold { get; set; }

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
        _gold = 1000;
        _spawnPanel.gameObject.SetActive(false);
    }

    public void SpawnHero()
    {
        if (_gold < _spawnCost) return;

        RefreshGold(-_spawnCost);
        Hero hero = GameManager.Instance.ObjectPool.Get<Hero>("Hero");
        hero.SetHeroData(GameManager.Instance.Database.GetHeroData(11001));
        hero.transform.position = GameManager.Instance.SelectedSpawnableTile.transform.position;
        GameManager.Instance.SelectedSpawnableTile.HasHero = true;
        GameManager.Instance.SelectedSpawnableTile = null;
        _spawnPanel.gameObject.SetActive(false);
    }

    public void RefreshGold(int p_amount)
    {
        _gold += p_amount;
        _uiTextGold.RefreshText(_gold);
    }
}
