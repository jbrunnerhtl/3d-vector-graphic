using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

namespace VectorViewerClasses;

public static class GridSetup
{
    public static void SetupGrid(List<(Vector3, Vector3)> vectors)
    {
        var size = vectors.Count == 0 ? 5f : vectors.Max(v => MathF.Max(MathF.Abs(v.Item1.X) + MathF.Abs(v.Item2.X), MathF.Abs(v.Item1.Y) + MathF.Abs(v.Item2.Y)));
        DrawGrid(((int)size)*2, 1);      
        DrawLine3D(Vector3.Zero, new Vector3(0, (int)size, 0), Color.Gray);
    }
}