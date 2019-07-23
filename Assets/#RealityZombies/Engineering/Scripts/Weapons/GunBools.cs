public class GunBools
{

    private bool _bIsRealoading;
    public bool ThisGunIsReloading
    {
        get { return _bIsRealoading; }
        set { _bIsRealoading = value; }
    }

    private bool _bCAnAcceptCLip;
    public bool CanAcceptNewClip
    {
        get { return _bCAnAcceptCLip; }
        set { _bCAnAcceptCLip = value; }
    }

    public GunBools(bool isreleading, bool canaccept)
    {
        _bIsRealoading = isreleading;
        _bCAnAcceptCLip = canaccept;
    }
}
