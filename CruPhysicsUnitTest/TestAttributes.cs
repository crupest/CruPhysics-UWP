using System;

namespace CruPhysicsUnitTest
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    sealed class TestAttribute : Attribute
    {

    }
}
