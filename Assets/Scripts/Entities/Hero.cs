using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private HeroData _data;

    [SerializeField] private Projectile _projectile;

    // Gizmo 설정
    [SerializeField] private Color _gizmoColor = Color.red;
    [SerializeField] private bool _drawGizmoWhenSelected = true;

    private IDamagable _target;

    private float _timer;

    private void Update()
    {
        AttackTimer();
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

        // Projectile projectile = Instantiate(_projectile, transform);
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
