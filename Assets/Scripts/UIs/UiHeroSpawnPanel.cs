using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiHeroSpawnPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _costText;

    private void Start()
    {
        _costText.text = GameManager.Instance.HeroSpawn.SpawnCost.ToString();
    }

    public void ActivatePanel(Vector3 p_worldPos)
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(p_worldPos);

        gameObject.transform.position = screenPos;
        gameObject.SetActive(true);
    }
}
