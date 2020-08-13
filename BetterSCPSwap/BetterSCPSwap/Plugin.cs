using System;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.Loader;

namespace BetterSCPSwap
{
    class Plugin : Plugin<Config>
    {
        private static readonly Lazy<Plugin> LazyInstance = new Lazy<Plugin>(() => new Plugin());
        public static Plugin pluginInstance => LazyInstance.Value;
        public List<Player> playersToSwap;

        public override void OnEnabled()
        {

            Register();
        }
        public override void OnDisabled()
        {
            UnRegister();
        }
        public void Register()
        {
            playersToSwap = new List<Player>();

        }
        public void UnRegister()
        {
            //Exiled.Events.Handlers.Scp914.UpgradingItems -= GulagEvent.OnUpgrading;
          


        }
    }
}