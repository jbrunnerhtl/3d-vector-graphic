using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
namespace VectorViewerClasses;

public static class VectorExtension
{
    public static void DrawArrow(this Vector3 vector, Vector3 start, Color color)
    {
        var dir = vector - start;
        float len = dir.Length();
        if (len < 0.0001f) return;
        float headLen = MathF.Min(0.3f, len * 0.3f);
        var headStart = start + Vector3.Normalize(dir) * (len - headLen);

        DrawCylinderEx(start, headStart, 0.03f, 0.03f, 12, color);  
        DrawCylinderEx(headStart, vector, 0.1f, 0f, 12, color);        
    }
}