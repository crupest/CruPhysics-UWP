using ChipmunkX;
using Windows.UI;

namespace CruPhysics.ViewModels
{
    public class BodyViewModel : ViewModelBase
    {
        private readonly Body _body;
        private Color _color;
        private double _positionX;
        private double _positionY;

        internal BodyViewModel(Body body, ShapeData shape, Color color)
        {
            _body = body;
            Shape = shape;
            _color = color;
            _positionX = body.Position.X;
            _positionY = body.Position.Y;
        }

        public ShapeData Shape { get; }

        public Color Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }

        public double PositionX
        {
            get => _positionX;
            set => SetProperty(ref _positionX, value);
        }

        public double PositionY
        {
            get => _positionY;
            set => SetProperty(ref _positionY, value);
        }

    }
}
