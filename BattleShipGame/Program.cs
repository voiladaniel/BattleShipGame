
using log4net;

namespace BattleShipGame
{
    using Autofac;
    using IoC;
    using Core.Managers;
    using System;

    public class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            Logger.Info("Program Starting");
            try
            {
                var container = Installer.CompositionRoot();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<ISetupManager>();
                    //_log.Info($"BTG - Program started with success!");
                    app.Start();
                }
            }
            catch(Exception e)
            {
                Logger.Error($"BTG - Error on startup: {e}");
            }
        }
    }
}
