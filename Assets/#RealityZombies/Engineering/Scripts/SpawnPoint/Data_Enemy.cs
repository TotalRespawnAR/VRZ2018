using UnityEngine;

public class Data_Enemy
{

    private int _timeStamp;
    private int _id;
    private KngEnemyName _modelName;
    private int _health;
    private int _hitStrength;
    private ARZombieypes _ztype_STD; //tandard patrole eater 
    private Vector3 _initialRotEuler;
    private KNode _spawnKnode;
    private SeekSpeed _levelAgro;


    public int TimeStamp
    {
        get
        {
            return _timeStamp;
        }

        private set
        {
            _timeStamp = value;
        }
    }

    public int ID
    {
        get
        {
            return _id;
        }

        set
        {
            _id = value;
        }
    }

    public KngEnemyName ModelName
    {
        get
        {
            return _modelName;
        }

        private set
        {
            _modelName = value;
        }
    }

    public int HitPoints
    {
        get
        {
            return _health;
        }

        private set
        {
            _health = value;
        }
    }

    public ARZombieypes Ztype_STD
    {
        get
        {
            return _ztype_STD;
        }

        private set
        {
            _ztype_STD = value;
        }
    }


    public Vector3 InitialRotEuler
    {
        get
        {
            return _initialRotEuler;
        }

        set
        {
            _initialRotEuler = value;
        }
    }

    public KNode SpawnKnode
    {
        get
        {
            return _spawnKnode;
        }

        set
        {
            _spawnKnode = value;
        }
    }


    public int HitStrength
    {
        get
        {
            return _hitStrength;
        }

        set
        {
            _hitStrength = value;
        }
    }

    public SeekSpeed LevelSeekSpeed
    {
        get
        {
            return _levelAgro;
        }

        set
        {
            _levelAgro = value;
        }
    }

    public Data_Enemy(int timeStamp, int id, KngEnemyName modelName, ARZombieypes ztype_STD, int argHealth, int argStrength, SeekSpeed argLevelAgr, Vector3 initialRotEuler, KNode spawnKnode)
    {
        TimeStamp = timeStamp;
        ModelName = modelName;
        HitPoints = argHealth;
        Ztype_STD = ztype_STD;
        HitStrength = argStrength;
        LevelSeekSpeed = argLevelAgr;

        ID = id;
        InitialRotEuler = initialRotEuler;
        SpawnKnode = spawnKnode;

        if (ztype_STD == ARZombieypes.TankFighter || ztype_STD == ARZombieypes.STD_BOSS || ztype_STD == ARZombieypes.BIGBOSS || ztype_STD == ARZombieypes.BOSS_1 || ztype_STD == ARZombieypes.BOSS_2)
        {
            HitStrength = argStrength +1;
        }

    }

    //Data_EnemyObj
    public Data_Enemy(int timeStamp, KngEnemyName modelName, ARZombieypes ztype_STD, int argHealth, int argStrength, SeekSpeed argLevelAgr)
    {
        TimeStamp = timeStamp;
        ModelName = modelName;
        HitPoints = argHealth;
        Ztype_STD = ztype_STD;
        HitStrength = argStrength;
        LevelSeekSpeed = argLevelAgr;

        ID = -1;
        InitialRotEuler = new Vector3(0, 0, 0);
        SpawnKnode = null;

        if (ztype_STD == ARZombieypes.TankFighter || ztype_STD == ARZombieypes.STD_BOSS || ztype_STD == ARZombieypes.BIGBOSS || ztype_STD == ARZombieypes.BOSS_1 || ztype_STD == ARZombieypes.BOSS_2)
        {
            HitStrength = argStrength * 2;
        }
    }

}
