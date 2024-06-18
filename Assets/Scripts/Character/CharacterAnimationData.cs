
using UnityEngine;

[System.Serializable]
public class CharacterAnimationData
{

    [SerializeField] private string idleParam = "isIdle";
    [SerializeField] private string walkParam = "isWalk";
    [SerializeField] private string hitParam = "isHit";
    [SerializeField] private string attackParam = "isAttack";
    [SerializeField] private string deathParam = "isDeath";

    public int IdleHash { get; private set; }
    public int WalkHash { get; private set; }
    public int HitHash { get; private set; }
    public int AttackHash { get; private set; }
    public int DeathHash { get; private set; }

    public void Init()
    {
        IdleHash = Animator.StringToHash(idleParam);
        WalkHash = Animator.StringToHash(walkParam);
        HitHash = Animator.StringToHash(hitParam);
        AttackHash = Animator.StringToHash(attackParam);
        DeathHash = Animator.StringToHash(deathParam);
    }
}
