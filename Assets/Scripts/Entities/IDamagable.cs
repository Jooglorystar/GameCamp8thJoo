public interface IDamagable
{
    bool IsDead { get; }
    void TakeDamage(int p_damage);
}