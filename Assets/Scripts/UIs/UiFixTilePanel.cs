using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiFixTilePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textFixCost;
    
    private int _baseFixCost = 50;
    private int _fixCost = 10;
    private int _fixedCount;

    private void Awake()
    {
        GameManager.Instance.FixPanel = this;
        _fixedCount = 0;
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            gameObject.SetActive(false);
            GameManager.Instance.SelectedSpawnableTile = null;
        }
    }

    public void ActivatePanel(Vector3 p_worldPos)
    {
        if (GameManager.Instance.IsGameOver) return;

        Vector2 screenPos = Camera.main.WorldToScreenPoint(p_worldPos);

        _textFixCost.text = GetFixCost().ToString();
        gameObject.transform.position = screenPos;
        gameObject.SetActive(true);
    }

    public void FixTile()
    {
        if(CanFix())
        {
            GameManager.Instance.RefreshMineral(-GetFixCost());
            GameManager.Instance.SelectedSpawnableTile.FixTile();
            _fixedCount++;
            gameObject.SetActive(false);
            GameManager.Instance.SelectedSpawnableTile = null;
        }
    }

    private bool CanFix()
    {
        if(GameManager.Instance.Mineral >= GetFixCost())
        {
            return true;
        }
        return false;
    }

    private int GetFixCost()
    {
        return _baseFixCost + (_fixCost * _fixedCount);
    }
}
