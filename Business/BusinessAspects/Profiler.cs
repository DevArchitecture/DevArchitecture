using System;

namespace Business.BusinessAspects
{
    public sealed class Profiler : IDisposable
    {
        private long _lastTick;
        private long _lastDelta;
        private string _lastBlockName;

        public long LastDelta
        {
            get
            {
                return _lastDelta;
            }
        }

        public long LastMsecs
        {
            get
            {
                return LastDelta / 10000;
            }
        }

        public Profiler()
        {
        }

        public void Begin(string blockName, string msg)
        {
            // If our previous begin call is not closed, close it.
            if (!string.IsNullOrEmpty(_lastBlockName))
                End();
            System.Diagnostics.Trace.Indent();
            _lastBlockName = blockName;
            // There are 10000 ticks in msec.
            _lastTick = DateTime.Now.Ticks;
            Write("+{0}", _lastBlockName);
            if (!string.IsNullOrEmpty(msg))
                Write(msg);
        }

        public void Begin(string blockName)
        {
            Begin(blockName, String.Empty);
        }

        public void Write(string format, params object[] args)
        {
            System.Diagnostics.Trace.WriteLine(string.Format(format, args));
        }

        public void End(string msg)
        {
            _lastDelta = DateTime.Now.Ticks - _lastTick;
            Write("-{0}\t{1}ms", _lastBlockName, _lastDelta / 10000);
            _lastBlockName = String.Empty;
            System.Diagnostics.Trace.Unindent();
        }

        public void End()
        {
            End(String.Empty);
        }

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion
    }
}
