using UnityEngine;

public class BulletsManager : MonoBehaviour
{
    public static BulletsManager Instance;
    Bullet[] BulletsFiredInthisShot;
    EnemiesManager m_em;
    private void Awake()
    {
        if (Instance == null)
        {
            m_em = GetComponent<EnemiesManager>();
            Instance = this;
        }
        else
            Destroy(gameObject);
    }

    public void SetuNewShot(Bullet[] argBullets)
    {
        if (argBullets != null)
        {
            int arralen = argBullets.Length;
            if (arralen > 0)
            {
                for (int b = 0; b < arralen; b++)
                {
                    argBullets[b].NewStartWithAngle();

                }
            }
        }
    }

    public void InstantiateBullet(int Numberofbullets, GameObject ChamberegdBullet, Vector3 BarrelPoeistion, Quaternion rot)
    {

        BulletsFiredInthisShot = new Bullet[Numberofbullets];
        for (int b = 0; b < Numberofbullets; b++)
        {

            GameObject bulletobj = Instantiate(ChamberegdBullet, BarrelPoeistion, rot);
            bulletobj.name = "___BULET";
            BulletsFiredInthisShot[b] = bulletobj.GetComponent<Bullet>();
            BulletsFiredInthisShot[b].Is_CountsTowardsPoints = true;
            BulletsFiredInthisShot[b].NewStartWithAngle();
            BulletsFiredInthisShot[b].NewTraileAngled(rot, BarrelPoeistion);

        }




    }
    public void GiveEnemyBulletArray(int EnemyID) { }



}
