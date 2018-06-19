using Prism.Windows.Mvvm;

namespace CruPhysics.ViewModels
{
    public class SecondPageViewModel : ViewModelBase
    {
        public SecondPageViewModel()
        {
            DisplayText = "This is the second page!";
        }

        public string DisplayText { get; private set; }
    }
}
