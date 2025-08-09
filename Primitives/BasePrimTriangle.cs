using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ssm.Render.Primitives
{
    public struct BasePrimTriangle : IVertexType
    {
        public Vector2 _position;
        public Color _color;
        public Vector2 _sideCoordinates;

        public VertexDeclaration VertexDeclaration => _vertexDeclaration;

        private static readonly VertexDeclaration _vertexDeclaration = new(new VertexElement[]
        {
                new VertexElement(0, VertexElementFormat.Vector2, VertexElementUsage.Position, 0),
                new VertexElement(8, VertexElementFormat.Color, VertexElementUsage.Color, 0),
                new VertexElement(12, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),
        });

        public BasePrimTriangle(Vector2 position, Color color, Vector2 sideCoordinates)
        {
            _position = position;
            _color = color;
            _sideCoordinates = sideCoordinates;
        }
    }

    public struct PrimTriangle3D : IVertexType
    {
        public Vector2 Position;
        public Color Color;
        public Vector3 SideCoordinates;

        public VertexDeclaration VertexDeclaration => _vertexDeclaration;

        private static readonly VertexDeclaration _vertexDeclaration = new(new VertexElement[]
        {
            new VertexElement(0,VertexElementFormat.Vector2, VertexElementUsage.Position, 0),
            new VertexElement(0, VertexElementFormat.Color, VertexElementUsage.Color, 0),
            new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 0)
        });

        public PrimTriangle3D(Vector2 position, Color color, Vector3 sideCoordinates)
        {
            Position = position;
            Color = color;
            SideCoordinates = sideCoordinates;
        }
    }

}
