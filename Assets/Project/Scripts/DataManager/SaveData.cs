using System.Collections.Generic;
using TMPro;

public class SaveData
{
    public string currentProgress;
    public bool[] reachedEnds = new bool[3];
    public bool notPlayed = true;
    public Dictionary<string, bool> flags = new Dictionary<string, bool>() { {"thardOption", false} };
    public bool ending = false;
}
