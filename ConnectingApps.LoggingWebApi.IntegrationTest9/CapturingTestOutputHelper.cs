using Xunit.Abstractions;

namespace ConnectingApps.LoggingWebApi.IntegrationTest9
{
    public sealed class CapturingTestOutputHelper : ITestOutputHelper
    {
        private readonly ITestOutputHelper _inner;
        private readonly List<string> _lines = new();

        public CapturingTestOutputHelper(ITestOutputHelper inner)
        {
            _inner = inner;
        }

        public IReadOnlyList<string> Lines
        {
            get
            {
                lock (_lines)
                {
                    return _lines.ToArray();
                }
            }
        }

        public void WriteLine(string message)
        {
            lock (_lines)
            {
                _lines.Add(message);
            }

            _inner.WriteLine(message);
        }

        public void WriteLine(string format, params object[] args)
        {
            WriteLine(string.Format(format, args));
        }
    }
}
