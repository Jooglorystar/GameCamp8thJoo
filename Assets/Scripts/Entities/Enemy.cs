using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private EnemyData _data;
    [SerializeField] private UiHealthBar _healthBar;

    [SerializeField] private Vector3 _direction;

    private WayPoint[] _wayPoints;
    private int _totalWayPoints = 0;
    private int _currentWayPointIndex = 0;

    public int EnemyHealth { get; private set; }

    private void Awake()
    {
        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        if(_healthBar == null)
        {
            _healthBar = GetComponentInChildren<UiHealthBar>();
        }
    }

    public void InitEnemy(WayPoint[] p_wayPoints)
    {
        EnemyHealth = _data.maxHealth;
        _currentWayPointIndex = 0;
        SetWayPoints(p_wayPoints);
        StartCoroutine(OnMove());
        _healthBar.SetValue(EnemyHealth);
        _healthBar.RefreshHealthBar(EnemyHealth);
    }

    public void TakeDamage(int p_damage)
    {
        EnemyHealth -= p_damage;
        _healthBar.RefreshHealthBar(EnemyHealth);
        if (EnemyHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
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
        }
        else
        {
            Move(Vector3.zero);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}