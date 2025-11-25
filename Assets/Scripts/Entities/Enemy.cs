using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private EnemyData _data;
    [SerializeField] private UiHealthBar _healthBar;

    private WayPoint[] _wayPoints;
    private int _totalWayPoints = 0;
    private int _currentWayPointIndex = 0;

    public int EnemyHealth { get; private set; }

    public bool IsDead => EnemyHealth <= 0;

    private bool _isMoving;

    private float _attackTimer = 0f;

    private void Awake()
    {
        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        if (_healthBar == null)
        {
            _healthBar = GetComponentInChildren<UiHealthBar>();
        }
    }

    private void Update()
    {
        if (!_isMoving)
        {
            AttackTimer();
        }
    }

    public void InitEnemy(WayPoint[] p_wayPoints)
    {
        EnemyHealth = _data.maxHealth;
        _currentWayPointIndex = 0;
        SetWayPoints(p_wayPoints);
        StartCoroutine(OnMove());
        _healthBar.SetValue(EnemyHealth);
        _attackTimer = 0f;
    }

    private void AttackTimer()
    {
        _attackTimer += Time.deltaTime;
        if (_attackTimer >= _data.attackInterval)
        {
            Attack();
            _attackTimer = 0f;
        }
    }

    private void Attack()
    {
        GameManager.Instance.Commander.TakeDamage(_data.attack);
    }

    public void TakeDamage(int p_damage)
    {
        EnemyHealth -= p_damage;
        _healthBar.RefreshHealthBar(EnemyHealth);
        if (IsDead)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager.Instance.RemainEnemyCount--;
        gameObject.SetActive(false);
        GameManager.Instance.CheckGameClear();
    }

    private void SetWayPoints(WayPoint[] p_wayPoints)
    {
        _totalWayPoints = p_wayPoints.Length;
        _wayPoints = new WayPoint[_totalWayPoints];
        _wayPoints = p_wayPoints;

        transform.position = _wayPoints[0].transform.position;
    }

    private void Move(Vector3 p_direction)
    {
        _rb.velocity = p_direction * _data.speed;
    }

    private IEnumerator OnMove()
    {
        GoToNextWayPoint();

        while (_currentWayPointIndex < _totalWayPoints)
        {
            if (Vector3.Distance(transform.position, _wayPoints[_currentWayPointIndex].transform.position) < 0.02f * _data.speed)
            {
                GoToNextWayPoint();
            }
            yield return null;
        }
    }

    private void GoToNextWayPoint()
    {
        if (_currentWayPointIndex < _totalWayPoints - 1)
        {
            transform.position = _wayPoints[_currentWayPointIndex].transform.position;
            _currentWayPointIndex++;
            Vector3 direction = (_wayPoints[_currentWayPointIndex].transform.position - transform.position).normalized;

            Move(direction);
            _isMoving = true;
        }
        else
        {
            Move(Vector3.zero);
            _isMoving = false;
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}