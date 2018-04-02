using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using CruPhysicsUnitTest.Model;
using CruPhysicsUnitTest.ViewModel;
using System.Threading;

namespace CruPhysicsUnitTest
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private TestController model;

        public MainPage()
        {
            this.InitializeComponent();


            var viewModel = new MainViewModel();
            DataContext = viewModel;

            model = new TestController(viewModel);

            var thread = new Thread(() =>
            {
                model.RunAllTest();
            });
            thread.Start();
        }       
    }
}
