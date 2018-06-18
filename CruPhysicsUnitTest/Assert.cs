using System;

namespace CruPhysicsUnitTest
{
    public class TestFailedException : Exception
    {
        public TestFailedException(string message) : base(message)
        {

        }

        public TestFailedException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }

    public class AssertException : TestFailedException
    {
        public AssertException(string message) : base(message)
        {

        }
    }

    public class ExpectedExceptionNotThrowException<TException> : TestFailedException
    {
        public ExpectedExceptionNotThrowException() : base($"{typeof(TException).FullName} is not thrown.")
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

        public static void Equal(float expected, float actual, int precision)
        {
            var delta = MathF.Pow(0.1f, precision);
            if (actual < expected - delta || actual > expected + delta)
                throw new AssertException($"Assert.Equal failed. Expected: {expected}; Actual: {actual}.");
        }

        public static void Equal(double expected, double actual, int precision)
        {
            var delta = Math.Pow(0.1, precision);
            if (actual < expected - delta || actual > expected + delta)
                throw new AssertException($"Assert.Equal failed. Expected: {expected}; Actual: {actual}.");
        }

        public static void Equal<T>(T expected, T actual)
        {
            if (!object.Equals(expected, actual))
                throw new AssertException($"Assert.Equal failed. Expected: {expected}; Actual: {actual}.");
        }

        public static void ExpectException<TException>(Action block) where TException : Exception
        {
            bool notThrow = false;
            try
            {
                block();
                notThrow = true;
            }
            catch (TException)
            {

            }

            if (notThrow)
                throw new ExpectedExceptionNotThrowException<TException>();
        }
    }
}
