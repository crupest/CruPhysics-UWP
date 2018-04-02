using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Windows.ApplicationModel.Core;
using Windows.UI.Core;

using CruPhysicsUnitTest.ViewModel;

namespace CruPhysicsUnitTest.Model
{
    public class TestMethod
    {
        public TestMethod(TestClass testClass, MethodInfo method)
        {
            TestClass = testClass;
            Method = method;

            ViewModel = new TestItemViewModel
            {
                TestClassName = method.ReflectedType.Name,
                TestName = method.Name,
                TestState = TestState.NotRun,
                Info = "The test hasn't run."
            };

            TestClass.Controller.ViewModel.Tests.Add(ViewModel);
        }

        public TestClass TestClass { get; private set; }
        public MethodInfo Method { get; private set; }
        public TestItemViewModel ViewModel { get; private set; }

        public void OnPassed()
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                TestClass.Controller.ViewModel.PassedTestCount++;
                ViewModel.TestState = TestState.Passed;
                ViewModel.Info = "The test passed.";
            });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        public void OnFailed(Exception exception)
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                TestClass.Controller.ViewModel.FailedTestCount++;
                ViewModel.TestState = TestState.Failed;
                ViewModel.Info = exception.ToString();
            });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }
    }

    public class TestClass
    {
        public TestClass(TestController controller, Type testClass)
        {
            Controller = controller;
            Class = testClass;

            var linq = from method in testClass.GetMethods()
                       where Attribute.IsDefined(method, typeof(TestAttribute)) &&
                       method.ReturnType == typeof(void) &&  //Return void
                       method.GetParameters().Length == 0    //No parameter
                       select new TestMethod(this, method);

            TestMethods = linq.ToList();
        }

        public TestController Controller { get; private set; }
        public Type Class { get; private set; }
        public IEnumerable<TestMethod> TestMethods { get; private set; }

        public void RunTest()
        {
            var o = Activator.CreateInstance(Class);

            foreach (var method in TestMethods)
            {
                try
                {
                    method.Method.Invoke(o, new object[0]);
                    method.OnPassed();
                }
                catch (TargetInvocationException e)
                {
                    method.OnFailed(e.InnerException);
                }
            }

            //Dispose if it is disposable.
            (o as IDisposable)?.Dispose();
        }
    }

    public class TestController
    {
        public TestController(MainViewModel viewModel)
        {
            ViewModel = viewModel;

            var linq = from type in Assembly.GetAssembly(GetType()).GetTypes()
                       where Attribute.IsDefined(type, typeof(TestAttribute))
                       select new TestClass(this, type);

            TestClasses = linq.ToList();
        }

        public MainViewModel ViewModel { get; private set; }

        public IEnumerable<TestClass> TestClasses { get; private set; }

        public IEnumerable<TestItemViewModel> GetTestItemViewModels()
        {
            foreach (var klass in TestClasses)
                foreach (var method in klass.TestMethods)
                    yield return method.ViewModel;
        }

        public void RunAllTest()
        {
            foreach (var klass in TestClasses)
                klass.RunTest();
        }
    }
}
