using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private EnemyData _data;

    [SerializeField] private Vector3 _direction;
    
    public int EnemyHealth { get; private set; }

    private void Awake()
    {
        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody2D>();
        }
    }

    private void Update()
    {
        Move(_direction);
    }

    public void InitEnemy()
    {
        EnemyHealth = _data.maxHealth;
    }

    public void SetDirection(Vector2 p_direction)
    {
        _direction = p_direction;
    }

    private void Move(Vector2 p_direction)
    {
        _rb.velocity = p_direction * _data.speed;
    }
}