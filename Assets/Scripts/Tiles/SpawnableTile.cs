using UnityEngine;

public class SpawnableTile : MonoBehaviour
{
    private bool _hasHero;
    private BoxCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    public bool HasHero
    {
        get => _hasHero;
        set => _hasHero = value;
    }

    private void OnMouseUp()
    {
        ActivatePanel(transform.position);
        GameManager.Instance.SelectedSpawnableTile = this;
    }

    private void ActivatePanel(Vector3 p_position)
    {
        if (_hasHero || GameManager.Instance.IsGameOver) return;

        GameManager.Instance.HeroSpawn.SpawnPanel.ActivatePanel(p_position);
    }

    public void Spawn()
    {
        _hasHero = true;
        _collider.enabled = false;
    }

    public void Despawn()
    {
        _hasHero = false;
        _collider.enabled = true;
    }
}
