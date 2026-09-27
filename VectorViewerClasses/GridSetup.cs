using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

namespace VectorViewerClasses;

public static class GridSetup
{
    public static void SetupGrid(List<(Vector3, Vector3)> vectors, Camera3D camera)
    {
        var width = vectors.Count == 0 ? 5f : vectors.Max(v => MathF.Max(MathF.Abs(v.Item1.X) + MathF.Abs(v.Item2.X), MathF.Abs(v.Item1.Z) + MathF.Abs(v.Item2.Z)));
        BeginMode3D(camera);
        int half = (int)MathF.Ceiling(width) + 1;
        DrawGrid(half*2, 1);
        var height = vectors.Count == 0 ? 5f : vectors.Max(v => MathF.Max(MathF.Abs(v.Item1.Y), MathF.Abs(v.Item2.Y))) + 1;
        DrawLine3D(Vector3.Zero, new Vector3(0, (int)height, 0), Color.Gray);
        DrawLine3D(Vector3.Zero, new Vector3(0, height * -1, 0), Color.Gray);
        EndMode3D();
        for (int i = -half; i <= half; i++)
        {
            if (i == 0) continue;
            DrawLabel(i.ToString(), new Vector3(i, 0, 0), camera, Color.Red);   
            DrawLabel(i.ToString(), new Vector3(0, 0, i), camera, Color.Blue); 
        }
        DrawLabel("0", Vector3.Zero, camera, Color.DarkGray);
        
    }
    
    private static void DrawLabel(string text, Vector3 pos, Camera3D cam, Color color)
    {
        var forward = Vector3.Normalize(cam.Target - cam.Position);
        if (Vector3.Dot(pos - cam.Position, forward) <= 0) return;

        Vector2 screen = GetWorldToScreen(pos, cam);
        int w = MeasureText(text, 16);
        DrawText(text, (int)screen.X - w / 2, (int)screen.Y + 4, 16, color);
    }
}