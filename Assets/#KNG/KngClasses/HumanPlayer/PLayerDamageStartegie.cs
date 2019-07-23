using System.Collections;
using UnityEngine;

public class PlayerDamageTimerStrategie : MonoBehaviour, IPLayerDamageStartegie
{

    float CurEnemyHitTime = 0.0f;
    PlayerHudEffectsManagerComponent refHudEffects;

    public void IncurrDamage(TriggersDamageEffects argHT, float argDamage)
    {
#if ENABLE_DEBUGLOG
        Debug.Log("damstrat incurred " + argDamage + " damage");
#endif

        Start_RunEnemyHitCoolDownTimer(argHT, argDamage);
    }

    private Coroutine EnemyHitCoolDownRoutine1;

    void Start_RunEnemyHitCoolDownTimer(TriggersDamageEffects argTrigDamEff, float argDamage)
    {
        if (EnemyHitCoolDownRoutine1 == null)
        {
            //RzPlayerDamageEffectsComponent.Instance.DoHIt();
            CurEnemyHitTime = 0f;
            if (!GameManager.Instance.NODAMAGEON)
                 RzPlayerComponent.Instance.PlayerCurHP -= argDamage;   



            EnemyHitCoolDownRoutine1 = StartCoroutine(RunEnemyHitCoolDownTimer());
        }
        //else {
        //    Debug.Log("there can be only one ");
        //}
    }


    private IEnumerator RunEnemyHitCoolDownTimer()
    {
        do
        {
            CurEnemyHitTime += Time.deltaTime;
            yield return null;
        }
        while (CurEnemyHitTime < RzPlayerComponent.Instance.DamageComp.Get_CooldownTime() && !RzPlayerComponent.Instance.IsPlayerdead());
        EnemyHitCoolDownRoutine1 = null;
    }

    public void InitHudEffects(PlayerHudEffectsManagerComponent argHud)
    {
        refHudEffects = argHud;
    }
}
