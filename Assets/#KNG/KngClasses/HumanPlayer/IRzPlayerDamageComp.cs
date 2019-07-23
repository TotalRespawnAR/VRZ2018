public interface IRzPlayerDamageComp
{

    void TakeEnemyHit(TriggersDamageEffects ht, float argFloatdamage);

    // void TakeEnemyHit(swingEnumVal sev, float argFloatdamage);
    void RunUpdateDamage();
    void PlayHudEffect(TriggersDamageEffects argDamageTrig);
    int Get_NumberofConcurrentDamages();
    float Get_CooldownTime();
    float Get_InitialHP();

}
