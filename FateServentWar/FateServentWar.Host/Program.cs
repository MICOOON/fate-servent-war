using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace FateServentWar.Host {
    static class Program {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main() {
            HostFactory.Run(x =>
            {
                x.Service<ServerStart>(s =>
                {
                    s.ConstructUsing(name => new ServerStart());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("FateServentWar");
                x.SetDisplayName("FateServentWar");
                x.SetServiceName("FateServentWar");
            });
        }
    }

    class ServerStart {
        private static ILog log = log4net.LogManager.GetLogger(typeof(ServerStart));

        public void Start() {
            Console.WriteLine("server start...");
            log.Debug("server start...");
            log.Info("server start...");
            log.Error("server start...");
        }

        public void Stop() {
            Console.WriteLine("server stop...");
            log.Debug("server start...");
            log.Info("server start...");
            log.Error("server start...");
        }
    }
}
