using UnityEngine;

public class EnemyEffectsComponent : MonoBehaviour, IEnemyEffectsCompo
{
    #region Private vars
    IEnemyEntityComp m_ieec;
    #endregion
    #region MonoMethods
    void Awake()
    {
        m_ieec = GetComponent<IEnemyEntityComp>();
    }
    void OnEnable()
    {
        m_ieec.OnBulletWoundInflicted += OnEnemyBulletWoond;
    }
    void OnDisable()
    {
        m_ieec.OnBulletWoundInflicted -= OnEnemyBulletWoond;
    }

    void Start()
    {
        if (m_ieec == null) { Debug.Log("ScoreComponet Didnot find IEBC!!!"); }
    }
    #endregion

    int i1234 = 0;
    #region EventListener
    void OnEnemyBulletWoond(object o, ArgsBulletWoond e)
    {

        // Debug.Log("my guntype is " + e.Guntype.ToString() + "damage " + e.Damage);
        if (GameManager.Instance != null)
        {
            if (e.LimbCollidersType == BulletPointsType.Head)
            {
                //i1234++;
                //if (i1234 > 4) i1234 = 1;

                GameManager.Instance.GetStreakManager().CreateCappedStreakObject(e.HitLocation);
                // GameManager.Instance.EffectBlood(m_ieec.Get_MyHEADtrans().position, i1234);
            }
            else
                GameManager.Instance.EffectShartd(e.HitLocation, e.Guntype, (e.Damage > 1));
        }
    }
    #endregion
}
