
using System;
using UnityEngine;

public class ComputerGrid
{
    private ComputerDisplay display;
    private int COLS;
    private int ROWS;

    private int[,] text;
    private Color[,] textColor;
    private Color[,] bgColor;
    private bool[,] marks;

    public ComputerGrid(ComputerDisplay display)
    {
        this.display = display;

        COLS = ComputerDisplay.WIDTH / ComputerDisplay.CHAR_WIDTH;
        ROWS = ComputerDisplay.HEIGHT / ComputerDisplay.CHAR_HEIGHT;

        text = new int[COLS, ROWS];
        textColor = new Color[COLS, ROWS];
        bgColor = new Color[COLS, ROWS];
        marks = new bool[COLS, ROWS];
    }

    public void TypeChar(char c, int x, int y)
    {
        TypeChar(CharCodes.GetCode(c), x, y);
    }

    public void TypeChar(int c, int x, int y)
    {
        CheckPosition(x, y);
        display.DrawChar(c, x * ComputerDisplay.CHAR_WIDTH, y * ComputerDisplay.CHAR_HEIGHT);
        SetChar(c, x, y);
    }

    private void CheckPosition(int x, int y)
    {
        if (x < 0 || x >= COLS || y < 0 || y >= ROWS)
            throw new ArgumentException("Position out of allowable");
    }

    private void SetChar(int c, int x, int y)
    {
        text[x, y] = c;
        textColor[x, y] = display.FontColor;
        bgColor[x, y] = display.BackColor;
    }

    private void SetChar(int c, int x, int y, Color fontColor, Color backColor)
    {
        text[x, y] = c;
        textColor[x, y] = fontColor;
        bgColor[x, y] = backColor;
    }

    public void Clear()
    {
        for (int x = 0; x < COLS; x++)
            for (int y = 0; y < ROWS; y++)
            {
                SetChar(0, x, y);
            }

        display.Clear();
    }

    public void SetFontColor(Color color)
    {
        display.FontColor = color;
    }

    public void SetBgColor(Color color)
    {
        display.BackColor = color;
    }

    public void DrawMark(bool state, int x, int y)
    {
        CheckPosition(x, y);
        marks[x, y] = state;
        UpdateChar(x, y);
    }

    public void UpdateChar(int x, int y)
    {
        Color tmpFont = display.FontColor;
        Color tmpBack = display.BackColor;

        if (marks[x, y])
        {
            display.FontColor = bgColor[x, y];
            display.BackColor = textColor[x, y];
        }
        else
        {
            display.FontColor = textColor[x, y];
            display.BackColor = bgColor[x, y];
        }
        display.DrawChar(text[x, y], x * ComputerDisplay.CHAR_WIDTH, y * ComputerDisplay.CHAR_HEIGHT);

        display.FontColor = tmpFont;
        display.BackColor = tmpBack;
    }

    public void Redraw()
    {
        for (int x = 0; x < COLS; x++)
            for (int y = 0; y < ROWS; y++)
                UpdateChar(x, y);
    }

    public void ClearMarks()
    {
        for (int x = 0; x < COLS; x++)
            for (int y = 0; y < ROWS; y++)
                marks[x, y] = false;

        Redraw();
    }

    public void ScrollUp(int count = 1, int border_x = 0, int border_y = 0)
    {
        for (int x = border_x; x < COLS - border_x; x++)
            for (int y = border_y; y < ROWS - border_y; y++)
            {
                if (y >= ROWS - count - border_y)
                {
                    SetChar(0, x, y);
                    marks[x, y] = false;
                }
                else
                {
                    SetChar(text[x, y + count], x, y, textColor[x, y + count], bgColor[x, y + count]);
                    marks[x, y] = marks[x, y + count];
                }

                UpdateChar(x, y);
            }
    }

}
