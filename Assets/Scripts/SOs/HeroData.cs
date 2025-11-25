using UnityEngine;

[CreateAssetMenu(fileName = "_HeroData", menuName = "SO/HeroData", order = 0)]
public class HeroData : ScriptableObject
{
    public int heroId;
    public int level;
    public int attack;
    public float attackInterval;
    public float attackRange;
    public float attackSpeed;
    public Color heroColer;
    public LayerMask attackLayer;
}