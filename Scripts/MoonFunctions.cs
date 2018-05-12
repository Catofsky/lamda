
using System.IO;
using UnityEngine;

public class MoonFunctions
{
    static string fsDir = Application.dataPath + "/ComputerFS";

    static public void CreateFile(string path)
    {
        File.Create(fsDir + "/" + path);
    }
}
