using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace CruPhysicsUnitTest.ViewModel
{
    public enum TestState
    {
        NotRun,
        Passed,
        Failed
    }

    public class TestItemViewModel : INotifyPropertyChanged
    {
        private string _testClassName;
        private string _testName;
        private TestState _testState;
        private string _info;

        public string TestClassName
        {
            get => _testClassName;
            set
            {
                if (value != _testClassName)
                {
                    _testClassName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TestClassName)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FullName)));
                }
            }
        }

        public string TestName
        {
            get => _testName;
            set
            {
                if (value != _testName)
                {
                    _testName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TestName)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FullName)));
                }
            }
        }

        public string FullName => $"{TestClassName}.{TestName}";

        public TestState TestState
        {
            get => _testState;
            set
            {
                if (value != _testState)
                {
                    _testState = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TestState)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemBackground)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Symbol)));
                }
            }
        }

        public string Info
        {
            get => _info;
            set
            {
                if (value != _info)
                {
                    _info = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Info)));
                }
            }
        }

        public Brush ItemBackground
        {
            get
            {
                switch (TestState)
                {
                    case TestState.NotRun:
                        return new SolidColorBrush(Colors.Yellow);
                    case TestState.Passed:
                        return new SolidColorBrush(Colors.Green);
                    case TestState.Failed:
                        return new SolidColorBrush(Colors.Red);
                    default:
                        throw new Exception("Unreachable code.");
                }
            }
        }

        public Symbol Symbol
        {
            get
            {
                switch (TestState)
                {
                    case TestState.NotRun:
                        return Symbol.Remove;
                    case TestState.Passed:
                        return Symbol.Accept;
                    case TestState.Failed:
                        return Symbol.Cancel;
                    default:
                        throw new Exception("Unreachable code.");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        private int _passedTestCount = 0;
        private int _failedTestCount = 0;

        public ObservableCollection<TestItemViewModel> Tests { get; } = new ObservableCollection<TestItemViewModel>();

        public int PassedTestCount
        {
            get => _passedTestCount;
            set
            {
                if (value != _passedTestCount)
                {
                    _passedTestCount = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PassedTestCount)));
                }
            }
        }

        public int FailedTestCount
        {
            get => _failedTestCount;
            set
            {
                if (value != _failedTestCount)
                {
                    _failedTestCount = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FailedTestCount)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
