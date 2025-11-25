using UnityEngine;

public class Commander : MonoBehaviour, IDamagable
{
    [SerializeField] private UiHealthBar _healthBar;
    public int Health { get; private set; }
    public bool IsDead => Health <= 0;

    private void Awake()
    {
        if (_healthBar == null)
        {
            _healthBar = GetComponentInChildren<UiHealthBar>();
        }
    }

    private void Start()
    {
        InitCommander();
        GameManager.Instance.Commander = this;
    }

    public void InitCommander()
    {
        Health = 100;
        _healthBar.SetValue(Health);
    }

    public void TakeDamage(int p_damage)
    {
        Health -= p_damage;
        _healthBar.RefreshHealthBar(Health);
        if (IsDead)
        {
            GameManager.Instance.GameOver();
        }
    }
}
