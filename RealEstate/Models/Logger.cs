using Fibretel.Models.Entities;

namespace Fibretel.Models
{
    public static class Logger
    {
        public static Log CreateLog(string usr, string type, string message)
        {
            Log log = new Log();
            log.Time = DateTime.Now;
            log.Username = usr;
            log.Type = type;
            log.Message = message;

            return log;
        }
    }
}
