using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.IO;
//[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", ConfigFileExtension = "config", Watch = true)]
namespace MyDapperComp.CommonHelper
{
    /// <summary>
    /// log helper.
    /// </summary>
    public static class LogHelper
    {
        private static ILoggerRepository repository = LogManager.CreateRepository("ApiLogs");
        static LogHelper()
        {
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
        }

        private static ILog log = LogManager.GetLogger(repository.Name, "LogHelper");
        private static ILog log_Normal = LogManager.GetLogger(repository.Name, "LogHelperNormal");
        public static void Write(string msg, LogLev lev)
        {
            switch (lev)
            {
                case LogLev.Debug:
                    log_Normal.Debug(msg);
                    break;
                case LogLev.Error:
                    log.Error(msg);
                    break;
                case LogLev.Fatal:
                    log.Fatal(msg);
                    break;
                case LogLev.Info:
                    log_Normal.Info(msg);
                    break;
                case LogLev.Warn:
                    log_Normal.Warn(msg);
                    break;
                default:
                    break;
            }
        }

        public static void Write(string msg, LogLev lev, params object[] parm)
        {
            switch (lev)
            {
                case LogLev.Debug:
                    log_Normal.DebugFormat(msg, parm);
                    break;
                case LogLev.Error:
                    log.ErrorFormat(msg, parm);
                    break;
                case LogLev.Fatal:
                    log.FatalFormat(msg, parm);
                    break;
                case LogLev.Info:
                    log_Normal.InfoFormat(msg, parm);
                    break;
                case LogLev.Warn:
                    log_Normal.WarnFormat(msg, parm);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// write ex level
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="lev"></param>
        public static void Write(Exception ex, LogLev lev)
        {
            switch (lev)
            {
                case LogLev.Debug:
                    log_Normal.Debug(ex);
                    break;
                case LogLev.Error:
                    log.Error(ex);
                    break;
                case LogLev.Fatal:
                    log.Fatal(ex);
                    break;
                case LogLev.Info:
                    log_Normal.Info(ex);
                    break;
                case LogLev.Warn:
                    log_Normal.Warn(ex);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// log ex.
        /// </summary>
        /// <param name="ex"></param>
        public static void Log(Exception ex)
        {
            Write("方法:{0} 消息:{1} 类:{2} 堆:{3} ", LogLev.Fatal, ex.TargetSite, ex.Message, ex.Source, ex.StackTrace);
        }

        /// <summary>
        /// log ex.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="fmodelid"></param>
        public static void Log(Exception ex, int fmodelid)
        {
            Write("方法:{0} 消息:{1} 类:{2} 堆:{3} fmodelid:{4}", LogLev.Fatal, ex.TargetSite, ex.Message, ex.Source, ex.StackTrace, fmodelid);
        }
    }
}
