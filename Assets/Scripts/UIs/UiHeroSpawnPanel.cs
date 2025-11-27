using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiHeroSpawnPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _costText;

    private void Start()
    {
        _costText.text = GameManager.Instance.HeroSpawn.SpawnCost.ToString();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            gameObject.SetActive(false);
        }
    }

    public void ActivatePanel(Vector3 p_worldPos)
    {
        if (GameManager.Instance.IsGameOver) return;

        Vector2 screenPos = Camera.main.WorldToScreenPoint(p_worldPos);

        gameObject.transform.position = screenPos;
        gameObject.SetActive(true);
    }
}
