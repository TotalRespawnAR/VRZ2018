using UnityEngine;


//no t needed , can just play woth cooldowns
public class PlayerMultiDamageStrategie : MonoBehaviour, IPLayerDamageStartegie
{

    private Coroutine[] EnemyHitCoolDownRoutines;

    public void IncurrDamage(TriggersDamageEffects argHT, float argDamage)
    {
        throw new System.NotImplementedException();
    }

    public void InitHudEffects(PlayerHudEffectsManagerComponent argHud)
    {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start()
    {
        EnemyHitCoolDownRoutines = new Coroutine[RzPlayerComponent.Instance.DamageComp.Get_NumberofConcurrentDamages()];

    }

}
