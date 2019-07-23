////#define  ENABLE_DEBUGLOG
using UnityEngine;

public class EnemyScoreComponent : MonoBehaviour, IEnemyScoreComp
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

    #region EventListener
    void OnEnemyBulletWoond(object o, ArgsBulletWoond e)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GetScoreMAnager().Update_Add_PointsTotal(e.ScoreForHit);
            GameManager.Instance.GetScoreMAnager().Update_Add_PointsCurWave(e.ScoreForHit);

            if (e.LimbCollidersType == BulletPointsType.Head) { GameManager.Instance.GetScoreMAnager().Update_headShotCNT(); }
            if (e.LimbCollidersType == BulletPointsType.Head && e.WasKillshot) { GameManager.Instance.GetScoreMAnager().Update_headShotKillsCNT(); }


        }

    }
    #endregion


}
