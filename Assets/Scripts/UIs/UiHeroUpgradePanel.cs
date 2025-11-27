using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiHeroUpgradePanel : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            gameObject.SetActive(false);
            GameManager.Instance.SelectedHero = null;
        }
    }

    public void ActivatePanel(Vector3 p_worldPos)
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(p_worldPos);

        gameObject.transform.position = screenPos;
        gameObject.SetActive(true);
    }

    public void Upgrade()
    {
        if (GameManager.Instance.HeroSpawn.CanUpgradeHero(GameManager.Instance.SelectedHero))
        {
            GameManager.Instance.HeroSpawn.UpgradeHero(GameManager.Instance.SelectedHero);

            gameObject.SetActive(false);
            GameManager.Instance.SelectedHero = null;
            Debug.Log("Upgrade Sucess");
        }
        else
        {
            Debug.Log("Upgrade Fail");
        }
    }
}
