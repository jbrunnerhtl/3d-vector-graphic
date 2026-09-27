using System.Globalization;
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace VectorViewerClasses;

public class VectorPanel
{
    public List<Vec> Vectors { get; } = new();
    private static int margin = 30;
    public int Selected { get; private set; } = -1;
    public float Min = -5f, Max = 5f;
    public int Width = 400;

    private int activeSlider = -1;
    private static readonly Color[] Palette =
        { Color.Red, Color.Blue, Color.DarkGreen, Color.Orange, Color.Purple, Color.Maroon };

    public void Draw()
    {
        if (IsMouseButtonReleased(MouseButton.Left)) activeSlider = -1;

        DrawRectangle(0, 0, Width, GetScreenHeight(), Fade(Color.White, 0.85f));

        if (Button(new Rectangle(10, 10 + margin, 130, 36), "+ Vektor"))
        {
            Vectors.Add(new Vec (Vector3.Zero ,new Vector3(1, 1, 1), Palette[Vectors.Count % Palette.Length] ));
            Selected = Vectors.Count - 1;
        }
        if (Selected >= 0 && Button(new Rectangle(150, 10 + margin, 130, 36), "Entfernen"))
        {
            Vectors.RemoveAt(Selected);
            Selected = Vectors.Count - 1;
        }

        if (Selected >= 0)
        {
            var v = Vectors[Selected];
            float fromX = Slider(0, 100, "Von X", v.Start.X, Color.Red);
            float fromY = Slider(1, 145, "Von Y", v.Start.Y, Color.DarkGreen);
            float fromZ = Slider(2, 190, "Von Z", v.Start.Z, Color.Blue);
            float endX = Slider(3, 235, "Bis X", v.End.X, Color.Red);
            float endY = Slider(4, 280, "Bis Y", v.End.Y, Color.DarkGreen);
            float endZ = Slider(5, 325, "Bis Z", v.End.Z, Color.Blue);
            v.Start = new Vector3(fromX, fromY, fromZ);
            v.End = new Vector3(endX, endY, endZ);
            DrawText($"Laenge: {Fmt(v.End.Length())}", 12, 380, 18, Color.DarkGray);
        }

        for (int i = 0; i < Vectors.Count; i++)
        {
            var v = Vectors[i];
            var row = new Rectangle(10, 410 + i * 34, Width - 20, 30);
            if (i == Selected) DrawRectangleRec(row, Fade(v.Color, 0.2f));
            DrawRectangle(18, (int)row.Y + 8, 14, 14, v.Color);
            DrawText($"v{i + 1} = ({Fmt(v.Start.X)}, {Fmt(v.Start.Y)}, {Fmt(v.Start.Z)}) -> ({Fmt(v.End.X)}, {Fmt(v.End.Y)}, {Fmt(v.End.Z)})",
                     42, (int)row.Y + 6, 20, Color.Black);
            if (activeSlider == -1 && IsMouseButtonPressed(MouseButton.Left)
                && CheckCollisionPointRec(GetMousePosition(), row))
                Selected = i;
        }
    }

    private bool Button(Rectangle r, string text)
    {
        bool hover = CheckCollisionPointRec(GetMousePosition(), r);
        DrawRectangleRec(r, hover ? Color.SkyBlue : Color.LightGray);
        DrawRectangleLinesEx(r, 1, Color.Gray);
        DrawText(text, (int)r.X + 10, (int)r.Y + 8, 20, Color.Black);
        return hover && IsMouseButtonPressed(MouseButton.Left);
    }

    private float Slider(int id, float y, string label, float value, Color color)
    {
        const int labelHeight = 24;
        var bar = new Rectangle(12, y + labelHeight, 300, 20); 
        var mouse = GetMousePosition();
        var hitArea = new Rectangle(bar.X - 8, bar.Y - 8, bar.Width + 16, bar.Height + 16);

        if (IsMouseButtonPressed(MouseButton.Left) && CheckCollisionPointRec(mouse, hitArea))
            activeSlider = id;
        if (activeSlider == id && IsMouseButtonDown(MouseButton.Left))
        {
            float t = Math.Clamp((mouse.X - bar.X) / bar.Width, 0f, 1f);
            value = MathF.Round((Min + t * (Max - Min)) * 10f) / 10f;
        }

        float tv = (value - Min) / (Max - Min);
        DrawText(label, (int)bar.X, (int)y, 20, color);                          
        DrawRectangleRec(bar, Color.LightGray);
        DrawRectangle((int)bar.X, (int)bar.Y, (int)(bar.Width * tv), (int)bar.Height, Fade(color, 0.4f));
        DrawRectangle((int)(bar.X + bar.Width * tv) - 5, (int)bar.Y - 5, 10, (int)bar.Height + 10, color);
        DrawText(Fmt(value), (int)(bar.X + bar.Width + 12), (int)bar.Y, 20, Color.Black); 
        return value;
    }
    private static string Fmt(float f) => f.ToString("0.0", CultureInfo.InvariantCulture);
}