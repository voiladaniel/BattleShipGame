
namespace BattleShipGame.IoC
{
    using System.Diagnostics.CodeAnalysis;
    using Autofac;
    using Core.Config;
    using Core.Factories;
    using Core.Helpers;
    using Core.Models;
    using Data.Models;
    using Core.Managers;
    using BusinessLogic.Factories;
    using BusinessLogic.Helpers;
    using BusinessLogic.Managers;
    using Data.Config;
    using Data.Helpers;
    using System;
    using log4net;

    [ExcludeFromCodeCoverage]
    public static class Installer
    {
        public static IContainer CompositionRoot()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<SetupManager>().As<ISetupManager>();
            builder.RegisterType<OutputManager>().As<IOutputManager>();
            builder.RegisterType<InputManager>().As<IInputManager>();
            builder.RegisterType<Player>().As<IPlayer>();
            builder.RegisterType<GameConfig>().As<IGameConfig>();
            builder.RegisterType<RandomManager>().As<IRandomManager>();
            builder.RegisterType<Coordinate>().As<ICoordinate>();
            builder.RegisterType<ShipFactory>().As<IShipFactory>();
            builder.RegisterType<CoordinateManager>().As<ICoordinateManager>();
            builder.RegisterType<ShipManager>().As<IShipManager>();
            builder.RegisterType<Ship>().As<IShip>();
            builder.RegisterType<Board>().As<IBoard>();
            builder.RegisterType<FireShotResponse>().As<IFireShotResponse>();
            builder.RegisterType<Helper>().As<IHelper>();
            builder.RegisterType<ShotManager>().As<IShotManager>();
            builder.Register(c => LogManager.GetLogger(typeof(Object))).As<ILog>();
            return builder.Build();
        }
    }
}
