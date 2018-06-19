using CruPhysics.ViewModels;
using Prism.Windows.Mvvm;
using System.ComponentModel;
using Windows.UI.Xaml;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
namespace CruPhysics.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : SessionStateAwarePage, INotifyPropertyChanged
    {
        public MainPage()
        {
            InitializeComponent();
            DataContextChanged += MainPage_DataContextChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewModel ConcreteDataContext
        {
            get
            {
                return DataContext as MainPageViewModel;
            }
        }

        private void MainPage_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(nameof(ConcreteDataContext)));
            }
        }
    }
}
