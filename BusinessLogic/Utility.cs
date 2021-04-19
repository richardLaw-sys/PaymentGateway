using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BusinessLogic
{
    public static class Retry
    {
        public static void Do(Action action, TimeSpan retryInterval, int maxAttempt = 3)
        {
            Do<object>(() =>
            {
                action();
                return null;
            }, retryInterval, maxAttempt);
        }

        public static T Do<T>(Func<T> action, TimeSpan retryInterval, int maxAttempt = 3)
        {
            var exception = new List<Exception>();
            for (int attempt = 0; attempt < maxAttempt; attempt++)
            {
                try
                {
                    if (attempt > 0)               
                        Thread.Sleep(retryInterval);                 
                    return action();
                }
                catch (Exception ex)
                {
                    exception.Add(ex);
                }
            }

            throw new AggregateException(exception);
        }
    }
}
