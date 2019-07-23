// @Author Nabil Lamriben ©2018

public class ZmodelVariations  {

    private int _modelNum;
    public int ModelNumber
    {
        get { return _modelNum; }
        set { _modelNum = value; }
    }

    private int _variations;
    public int Variations
    {
        get { return _variations; }
        set { _variations = value; }
    }



    public ZmodelVariations(int argNum, int argVariations)
    {
        _modelNum = argNum;
        _variations = argVariations;
    }
}
