using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockRTS.Core.Reactive
{
    public class ConcurrentObservable<T> : IObservable<T>, IDisposable
    {
        private readonly object _lock = new object();
        private readonly ConcurrentDictionary<IObserver<T>, byte> _subscribers = new ConcurrentDictionary<IObserver<T>, byte>();
        private bool _isDisposed;
        #region IDisposable Members

        public virtual void Dispose()
        {
            _isDisposed = true;
            OnCompleted();
        }

        #endregion

        #region IObservable<T> Members

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (_isDisposed)
                throw new ObjectDisposedException("BufferedObservable<T>");
            _subscribers.TryAdd(observer, new byte());
            return new AnonymousDisposable(() =>
            {
                byte removed;
                _subscribers.TryRemove(observer, out removed);
            });
        }

        #endregion

        public void OnNext(T value)
        {
            lock (_lock)
                foreach (var sub in _subscribers.Keys)
                    sub.OnNext(value);
        }

        public void OnCompleted()
        {
            lock (_lock)
                foreach (var sub in _subscribers.Keys)
                    sub.OnCompleted();
        }

        public void OnError(Exception ex)
        {
            lock (_lock)
                foreach (var sub in _subscribers.Keys)
                    sub.OnError(ex);
        }
    }
}
