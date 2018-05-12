
using UnityEngine;

public class ComputerDisplay
{
    public const int CHAR_WIDTH = 4;
    public const int CHAR_HEIGHT = 8;
    public const int WIDTH = 160;
    public const int HEIGHT = 160;

    private Texture2D atlas;
    private Texture2D texture;
    private Texture2D[] frames;

    private Color defaultFontColor = new Color(0, 1, 0);
    private Color defaultBackgroundColor = new Color(0, 0, 0);
    public Color FontColor;
    public Color BackColor;

    public ComputerDisplay(MeshRenderer mesh)
    {
        texture = new Texture2D(WIDTH, HEIGHT, TextureFormat.RGB24, false)
        {
            filterMode = FilterMode.Point
        };
        atlas = Resources.Load("font") as Texture2D;
        frames = Split2D(atlas, CHAR_WIDTH, CHAR_HEIGHT);

        FontColor = defaultFontColor;
        BackColor = defaultBackgroundColor;

        mesh.materials[0].SetTexture("_MainTex", texture);
    }

    private Texture2D[] Split2D(Texture2D texture, int width, int height)
    {
        int x = texture.width / width;
        int y = texture.height / height;
        Texture2D[] res = new Texture2D[x * y];

        for (int j = 0; j < y; j++)
            for (int i = 0; i < x; i++)
            {
                var tex = new Texture2D(width, height);
                tex.SetPixels(0, 0, width, height, texture.GetPixels(i * width, j * height, width, height));
                res[j * x + i] = tex;
            }

        return res;
    }

    public void DrawChar(int c, int x, int y)
    {
        Color[] pixels = new Color[CHAR_WIDTH * CHAR_HEIGHT];
        pixels = frames[c].GetPixels();

        for (int i = 0; i < pixels.Length; i++)
            pixels[i] = pixels[i].Equals(Color.black) ? BackColor : FontColor;

        texture.SetPixels(x, HEIGHT - y - CHAR_HEIGHT, CHAR_WIDTH, CHAR_HEIGHT, pixels);
    }

    public void Apply()
    {
        texture.Apply();
    }

    public void Clear()
    {
        Color[] colors = new Color[WIDTH * HEIGHT];

        for (int i = 0; i < WIDTH * HEIGHT; i++)
            colors[i] = BackColor;

        texture.SetPixels(colors, 0);
    }

}
