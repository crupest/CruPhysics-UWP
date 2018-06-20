using System.Collections.ObjectModel;
using Windows.UI;

namespace CruPhysics.ViewModels
{
    public class SpaceViewModel : ViewModelBase
    {
        private Color _background;

        public ObservableCollection<BodyViewModel> BodyViewModels { get; } = new ObservableCollection<BodyViewModel>();

        public Color Background
        {
            get => _background;
            set => SetProperty(ref _background, value);
        }
    }
}
