using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace CruPhysics.Views
{
    public sealed partial class SpaceCanvas : UserControl
    {
        public SpaceCanvas()
        {
            this.InitializeComponent();
            canvas.RenderTransform = new CompositeTransform() { ScaleY = -1 };
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var transform = (CompositeTransform)canvas.RenderTransform;
            transform.TranslateX += (e.NewSize.Width - e.PreviousSize.Width) / 2.0;
            transform.TranslateY += (e.NewSize.Height - e.PreviousSize.Height) / 2.0;
        }
    }
}
