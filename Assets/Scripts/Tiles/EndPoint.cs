using UnityEngine;

public class EndPoint : MonoBehaviour, IDamagable
{
    public void TakeDamage(int p_damage)
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
