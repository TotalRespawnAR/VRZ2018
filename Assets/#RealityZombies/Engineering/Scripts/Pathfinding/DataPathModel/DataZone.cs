// @Author Nabil Lamriben ©2017
using System.Collections.Generic;

public class DataZone {  
    List<DataPath> pathsToThisZone;

    private string _nameOfZone;
    public string NameOfZone
    {
        get { return _nameOfZone; }
        set { _nameOfZone = value; }
    }

    public DataZone() { pathsToThisZone = new List<DataPath>(); }
    public DataZone(string argName)  { _nameOfZone= argName; pathsToThisZone = new List<DataPath>(); }

    public List<DataPath> GetPAths() { return this.pathsToThisZone; }
    public void AddToPaths(DataPath argdataPath) { pathsToThisZone.Add(argdataPath); }

}
