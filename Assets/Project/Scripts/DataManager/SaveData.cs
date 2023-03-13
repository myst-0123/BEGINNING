using System.Collections.Generic;

public class SaveData
{
    public string currentProgress;
    public bool[] reachedEnds = new bool[3];
    public bool notPlayed;
    public Dictionary<string, bool> flags = new Dictionary<string, bool>() { {"thardOption", false} };
}
