using System.Numerics;
using Raylib_cs;
namespace VectorViewerClasses;

public class Vec(Vector3 start, Vector3 end, Color color)
{
    public Vector3 Start = start;
    public Vector3 End = end;
    public Color Color = color;
}