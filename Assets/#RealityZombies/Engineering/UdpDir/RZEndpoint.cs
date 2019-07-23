public class RZEndpoint   {
    private string _IpStr;

    public string IPstr
    {
        get { return _IpStr; }
        private set { _IpStr = value; }
    }

    private string _portStr;
    public string PortStr
    {
        get { return _portStr; }
       private set { _portStr = value; }
    }


    public RZEndpoint() { }
    public RZEndpoint(string argIP, string argPort) {
        _IpStr = argIP;
        _portStr = argPort;
    }
}
