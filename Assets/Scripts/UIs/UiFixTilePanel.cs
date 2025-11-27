using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiFixTilePanel : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.FixPanel = this;
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
        Vector2 screenPos = Camera.main.WorldToScreenPoint(p_worldPos);

        gameObject.transform.position = screenPos;
        gameObject.SetActive(true);
    }

    public void FixTile()
    {
        GameManager.Instance.SelectedSpawnableTile.FixTile();
        gameObject.SetActive(false);
        GameManager.Instance.SelectedSpawnableTile = null;
    }
}
