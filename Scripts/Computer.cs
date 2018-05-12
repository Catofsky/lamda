
using UnityEngine;

public class Computer : MonoBehaviour {

    private ComputerDisplay display;
    private ComputerGrid grid;
    public MeshRenderer mesh;

	void Start () {
        display = new ComputerDisplay(mesh);
        grid = new ComputerGrid(display);
        grid.Clear();

        var text = "Hello World!";
        var pos = 0;
        foreach (var ch in text)
            grid.TypeChar(ch, pos++, 1);

        for (int i = 2; i < 10; i++)
            grid.DrawMark(true, i, 1);

        grid.DrawMark(false, 4, 0);
        grid.DrawMark(true, 3, 3);
        grid.DrawMark(true, 3, 4);

        grid.SetFontColor(Color.magenta);
 
        var p = 0;
        foreach (var ch in "Good bye!!!")
            grid.TypeChar(ch, p++, 5);

        grid.ScrollUp();

        display.Apply();
	}
	
	void Update () {
		
	}
}
