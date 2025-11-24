using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int _damage;
    private float _speed;
    private IDamagable _target;

    public void Shoot(int p_damage, float p_speed,Transform p_shooterTransform, IDamagable p_target)
    {
        transform.position = p_shooterTransform.position;
        _damage = p_damage;
        _speed = p_speed;
        _target = p_target;
    }

    private void Update()
    {
        Vector3 direction = ((MonoBehaviour)_target).transform.position - transform.position;
        direction.Normalize();
        transform.position += direction * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            _target.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
