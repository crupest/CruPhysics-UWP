using System;

namespace CruPhysicsUnitTest
{
    public class AssertException : Exception
    {
        public AssertException(string message) : base(message)
        {

        }
    }

    public static class Assert
    {
        public static void True(bool value)
        {
            if (!value)
                throw new AssertException("Assert.True failed.");
        }

        public static void True(bool value, string message)
        {
            if (!value)
                throw new AssertException("Assert.True failed. Additional info: " + message);
        }

        public static void Equal<T>(T expected, T actual)
        {
            if (!object.Equals(expected, actual))
                throw new AssertException($"Assert.Equal failed. Expected: {expected}; Actual: {actual}.");
        }

        public static void Equal(double expected, double actual, int precision)
        {
            var delta = Math.Pow(0.1, precision);
            if (actual < expected - delta || actual > expected + delta)
                throw new AssertException($"Assert.Equal failed. Expected: {expected}; Actual: {actual}.");
        }
    }
}
