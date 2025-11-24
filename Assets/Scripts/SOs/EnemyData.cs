using UnityEngine;

[CreateAssetMenu(fileName = "_enemyData", menuName = "SO/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    public int maxHealth;
    public float speed;
    public int attack;
}
