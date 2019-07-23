using System;
using UnityEngine;

public class ArgsBulletWoond : EventArgs
{
    Vector3 _hitLocation;
    Vector3 _dirrection;
    int _damage;
    BulletPointsType _LimbCollidersType;
    int _scoreForHit;
    bool _usePhysics; //beh can decide , hen radoll applies physics o hip or what ever bone.. or hbeh should jus tmake a 3 direection he pass that as separate event . actually no , keep i tall in Arsbulle , hat way that one event will affect all componens ino ne frame
    int _curHpforHealthbarAndScoreSender; //good to know if this was a killing blow . dont wait for the dead event
    bool _wasKillshot;
    GunType _guntype;

    public ArgsBulletWoond(GunType argGuntype, Vector3 hitLocation, Vector3 dirrection, int damage, BulletPointsType LimbCollidersType, int scoreForHit, bool usePhysics, int curHpforHealthbarAndScoreSender, bool argWasKill)
    {
        _guntype = argGuntype;
        _hitLocation = hitLocation;
        _dirrection = dirrection;
        _damage = damage;
        _LimbCollidersType = LimbCollidersType;
        _scoreForHit = scoreForHit;
        _usePhysics = usePhysics;
        _curHpforHealthbarAndScoreSender = curHpforHealthbarAndScoreSender;
        _wasKillshot = argWasKill;
    }

    public Vector3 HitLocation
    {
        get
        {
            return _hitLocation;
        }

        set
        {
            _hitLocation = value;
        }
    }

    public int Damage
    {
        get
        {
            return _damage;
        }

        set
        {
            _damage = value;
        }
    }

    public BulletPointsType LimbCollidersType
    {
        get
        {
            return _LimbCollidersType;
        }

        set
        {
            _LimbCollidersType = value;
        }
    }

    public Vector3 Dirrection
    {
        get
        {
            return _dirrection;
        }

        set
        {
            _dirrection = value;
        }
    }

    public int ScoreForHit
    {
        get
        {
            return _scoreForHit;
        }

        set
        {
            _scoreForHit = value;
        }
    }

    public bool UsePhysics
    {
        get
        {
            return _usePhysics;
        }

        set
        {
            _usePhysics = value;
        }
    }

    public int CurHpforHealthbarAndScoreSender
    {
        get
        {
            return _curHpforHealthbarAndScoreSender;
        }

        set
        {
            _curHpforHealthbarAndScoreSender = value;
        }
    }

    public GunType Guntype
    {
        get
        {
            return _guntype;
        }

        set
        {
            _guntype = value;
        }
    }

    public bool WasKillshot
    {
        get
        {
            return _wasKillshot;
        }

        set
        {
            _wasKillshot = value;
        }
    }
    // public ArgsBullet() { }
}
