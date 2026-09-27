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

var vectors = new List<(Vector3, Vector3, Color)> {
    (Vector3.Zero, new Vector3(1, 2, 3), Color.Red),
    (Vector3.Zero, new Vector3(-2, 1, 1), Color.Blue),
    (new Vector3(1, 2, 3), new Vector3(-1, 3, 4), Color.DarkGreen),
};

while (!WindowShouldClose())
{
    UpdateCamera(ref camera, CameraMode.Orbital);

    BeginDrawing();
    ClearBackground(Color.RayWhite);
    BeginMode3D(camera);

    GridSetup.SetupGrid([.. vectors.Select(s => (s.Item1, s.Item2))]);

    foreach (var (start, end, color) in vectors)
        end.DrawArrow(start, color);

    EndMode3D();
    DrawText("Mouse wheel: zoom", 10, 10, 20, Color.DarkGray);
    EndDrawing();
}

CloseWindow();
