using System.Numerics;
using Raylib_cs;
using VectorViewerClasses;
using static Raylib_cs.Raylib;

SetConfigFlags(ConfigFlags.ResizableWindow);
InitWindow(1000, 700, "Vector Viewer");
MaximizeWindow();
SetTargetFPS(60);

var camera = new Camera3D {
    Position = new Vector3(6, 5, 6),
    Target = Vector3.Zero,
    Up = Vector3.UnitY,
    FovY = 45,
    Projection = CameraProjection.Perspective
};
var panel = new VectorPanel();


while (!WindowShouldClose())
{
    UpdateCamera(ref camera, CameraMode.Orbital);

    BeginDrawing();
    ClearBackground(Color.RayWhite);
    

    GridSetup.SetupGrid(panel.Vectors.Count == 0 ? [] : [.. panel.Vectors.Select(s => (s.Start, s.End))], camera);
    BeginMode3D(camera);
    foreach (var vec in panel.Vectors)
        vec.End.DrawArrow(vec.Start, vec.Color);

    EndMode3D();
    panel.Draw();
    DrawText("Mouse wheel: zoom", 10, 10, 20, Color.DarkGray);
    EndDrawing();
}

CloseWindow();
