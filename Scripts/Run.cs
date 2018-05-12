using UnityEngine;

public class Run : MonoBehaviour {

    private Moon moon;

	void Start () {
        var code = @"
            print('Hello World!')
        ";

        moon = new Moon(code, 100);

        moon.OnEnd += OnEnd;
        moon.OnLuaError += OnError;
        moon.OnTick += OnTick;
        moon.OnPrint += OnPrint;

        moon.Start();
    }

    private void OnPrint(string text)
    {
        Debug.Log(text);
    }

    private void OnTick()
    {
    }

    private void OnError(string err)
    {
        Debug.Log("LUA ERR: " + err);
    }

    private void OnEnd(bool aborted)
    {
        Debug.Log("End" + (aborted ? " Aborted!" : ""));
    }

    void Update () {
		
	}

    void OnApplicationQuit()
    {
        moon.Stop();
    }
}
