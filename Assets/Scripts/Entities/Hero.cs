using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    // Gizmo 설정
    [SerializeField] private Color _gizmoColor = Color.red;
    [SerializeField] private bool _drawGizmoWhenSelected = true;

    [SerializeField] private HeroData _data;
    [SerializeField] private Image _upgradeIcon;

    private SpriteRenderer _sprite;

    private IDamagable _target;

    private float _timer;

    private bool _canUpgrade;

    private void Awake()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _upgradeIcon.gameObject.SetActive(false);
    }

    private void Update()
    {
        AttackTimer();
    }

    public void SetHeroData(HeroData p_heroData)
    {
        _data = p_heroData;
        _sprite.color = _data.heroColer;
    }


    public void ActivateUpgradeIcon()
    {
        _upgradeIcon.gameObject.SetActive(true);
    }

    private void AttackTimer()
    {
        _target = FindTarget();

        if (_target == null)
        {
            _timer = 0f;
            return;
        }
        _timer += Time.deltaTime;

        if (_timer >= _data.attackInterval)
        {
            Attack();
            _timer = 0f;
        }
    }

    private void Attack()
    {
        if (_target == null) return;

        Projectile projectile = GameManager.Instance.ObjectPool.Get<Projectile>("Projectile");
        projectile.Shoot(_data.attack, _data.attackSpeed, transform, _target);
    }

    private IDamagable FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _data.attackRange, _data.attackLayer);

        if (hits.Length == 0)
            return null;

        float minDist = float.MaxValue;
        IDamagable nearest = null;

        foreach (var hit in hits)
        {
            float dist = Vector2.Distance(transform.position, hit.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = hit.GetComponent<IDamagable>();
            }
        }

        return nearest;
    }

    // 범위 확인용
    private void OnDrawGizmosSelected()
    {
        if (!_drawGizmoWhenSelected || _data == null) return;
        Gizmos.color = _gizmoColor;
        Gizmos.DrawWireSphere(transform.position, _data.attackRange);
    }
}
