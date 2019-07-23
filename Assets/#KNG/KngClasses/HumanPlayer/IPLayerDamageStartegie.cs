public interface IPLayerDamageStartegie
{

    void IncurrDamage(TriggersDamageEffects argHT, float argDamage);

    void InitHudEffects(PlayerHudEffectsManagerComponent argHud);
}
