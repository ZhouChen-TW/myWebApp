using NLog;

namespace MyApp
{
    public class NLogger : INLogger
    {
        readonly Logger logger = LogManager.GetLogger("Logger");
        public virtual void Info(string message)
        {
            logger.Info(message);
        }
    }
}