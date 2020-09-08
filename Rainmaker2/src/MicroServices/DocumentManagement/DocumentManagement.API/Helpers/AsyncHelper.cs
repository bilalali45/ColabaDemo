using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManagement.API.Helpers
{
    public static partial class ObjectExtensions
    {
        public static string ToJson(this object value)
        {
            if (value == null) return null;

            try
            {
                var jsonSerializerSettings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                string json = JsonConvert.SerializeObject(value, formatting: Formatting.Indented, settings: jsonSerializerSettings);
                return json;
            }
            catch (Exception)
            {
                // ignored
            }

            return null;
        }

    }
    public static class AsyncHelper
    {
        private static readonly TaskFactory _myTaskFactory = new
          TaskFactory(CancellationToken.None,
                      TaskCreationOptions.None,
                      TaskContinuationOptions.None,
                      TaskScheduler.Default);

        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return AsyncHelper._myTaskFactory
              .StartNew<Task<TResult>>(func)
              .Unwrap<TResult>()
              .GetAwaiter()
              .GetResult();
        }

        public static void RunSync(Func<Task> func)
        {
            AsyncHelper._myTaskFactory
              .StartNew<Task>(func)
              .Unwrap()
              .GetAwaiter()
              .GetResult();
        }
    }
}
