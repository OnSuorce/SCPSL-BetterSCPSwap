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
        public Handlers.SwapEvent swapEvent;

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
            swapEvent = new Handlers.SwapEvent();
            Exiled.Events.Handlers.Server.SendingConsoleCommand += swapEvent.onConsoleCommand;

        }
        public void UnRegister()
        {
            //Exiled.Events.Handlers.Scp914.UpgradingItems -= GulagEvent.OnUpgrading;
            Exiled.Events.Handlers.Server.SendingConsoleCommand -= swapEvent.onConsoleCommand;

            swapEvent = null;
        }
    }
}