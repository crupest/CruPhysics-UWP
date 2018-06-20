using ChipmunkX;
using System.Linq;
using System.Collections.Generic;

namespace CruPhysics.ViewModels
{
    public enum ShapeType
    {
        Circle,
        Polygon,
        Segment
    }

    public abstract class ShapeData
    {
        protected ShapeData(ShapeType type)
        {
            ShapeType = type;
        }

        public ShapeType ShapeType { get; }
    }

    public class CircleData : ShapeData
    {
        public CircleData(double radius)
            : base(ShapeType.Circle)
        {
            Radius = radius;
        }

        public double Radius { get; }
    }

    public class PolygonData : ShapeData
    {
        public PolygonData(params Vector2D[] vertices)
            : base(ShapeType.Polygon)
        {
            Vertices = vertices.ToList();
        }

        public IReadOnlyList<Vector2D> Vertices { get; }
    }

    public class SegmentData : ShapeData
    {
        public SegmentData(Vector2D vertex1, Vector2D vertex2, double radius)
            : base(ShapeType.Segment)
        {
            Vertex1 = vertex1;
            Vertex2 = vertex2;
            Radius = radius;
        }

        public Vector2D Vertex1 { get; set; }
        public Vector2D Vertex2 { get; set; }
        public double Radius { get; set; }
    }
}
