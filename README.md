# 3D Vector Graphic

A small interactive 3D vector viewer written in C# with [Raylib-cs](https://github.com/ChrisDill/Raylib-cs).
Add vectors, adjust their start and end points with sliders, and watch them update live in a 3D coordinate system.

## Features

- **Interactive vectors**: add and remove vectors with a click; each new vector gets its own color
- **Sliders for every coordinate**: set start point (X, Y, Z) and end point (X, Y, Z) from −5 to 5 in steps of 0.1
- **Vector list**: all vectors are listed with their coordinates; click an entry to select and edit it
- **Auto-scaling grid**: the grid grows with your vectors and is labeled along the X and Z axes
- **Orbiting camera**: the view rotates around the origin; zoom with the mouse wheel
- **Cross-platform**: runs on Linux, Windows and macOS

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- A graphics driver with OpenGL support

Raylib-cs (8.1.0) is restored automatically from NuGet, including the native raylib binaries.

## Getting started

```bash
git clone https://github.com/jbrunnerhtl/3d-vector-graphic.git
cd 3d-vector-graphic
dotnet run --project VectorViewer
```

## Usage

| Action | How |
| --- | --- |
| Add a vector | Click **+ Vektor** |
| Remove the selected vector | Click **Entfernen** |
| Change start / end point | Drag the **Von X/Y/Z** and **Bis X/Y/Z** sliders |
| Select a vector | Click it in the list |
| Zoom | Mouse wheel |

The window opens maximized and can be resized freely.

## Project structure

```
3d-vector-graphic/
├── 3d-vector-graphics.sln
├── VectorViewer/                 # Console app: window, camera, main loop
│   └── Program.cs
└── VectorViewerClasses/          # Class library
    ├── Vec.cs                    # Vector model (start, end, color)
    ├── VectorPanel.cs            # UI panel: buttons, sliders, vector list
    ├── GridSetup.cs              # Auto-sized grid, axes and labels
    └── VectorExtension.cs        # DrawArrow extension method for Vector3
```

## Built with

- [C# / .NET 10](https://dotnet.microsoft.com/)
- [Raylib-cs](https://github.com/ChrisDill/Raylib-cs), C# bindings for [raylib](https://www.raylib.com/)

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE) for details.