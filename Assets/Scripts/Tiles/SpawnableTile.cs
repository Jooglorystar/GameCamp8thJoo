using UnityEngine;
using UnityEngine.U2D.Animation;

public class SpawnableTile : MonoBehaviour
{
    [SerializeField] private bool _isFixed;
    private bool _hasHero;
    private BoxCollider2D _collider;
    private SpriteResolver _spriteResolver;

    private void Awake()
    {
        _spriteResolver = GetComponentInChildren<SpriteResolver>();
        _collider = GetComponent<BoxCollider2D>();

        CheckTileState();
    }

    public bool HasHero
    {
        get => _hasHero;
        set => _hasHero = value;
    }

    private void OnMouseUp()
    {
        if (_isFixed)
        {
            ActivatePanel(transform.position);
            GameManager.Instance.SelectedSpawnableTile = this;
        }
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

    public void FixTile()
    {
        _isFixed = true;
        CheckTileState();
    }

    private void CheckTileState()
    {
        if (_isFixed)
        {
            _spriteResolver.SetCategoryAndLabel("Tile", "Spawnable");
        }
        else
        {
            _spriteResolver.SetCategoryAndLabel("Tile", "Fixable");
        }
    }
}
