
namespace BattleShipGame.BusinessLogic.Managers
{
    using System;
    using log4net;
    using BattleShipGame.Core.Managers;
    using Core.Models;
    using Data.Models;
    public class CoordinateManager : ICoordinateManager
    {
        private readonly IRandomManager _randomManager;

        private readonly ILog _log;

        public CoordinateManager(IRandomManager randomManager, ILog log)
        {
            _randomManager = randomManager ?? throw new ArgumentNullException(nameof(randomManager));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }
        public ICoordinate GetRandomCoordinate()
        {
            _log.Info("BTG - Get Random Coordinate");

            return new Coordinate(_randomManager.GetLocation(), _randomManager.GetLocation());
        }
    }
}
