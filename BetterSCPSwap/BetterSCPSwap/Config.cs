using System;
using System.ComponentModel;
using Exiled.API.Interfaces;
namespace BetterSCPSwap
{
    class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        [Description("Request a swap")]
        public string requestSwap { get; set; } = "swaprequest";

        [Description("Accept a swap")]
        public string acceptSwap { get; set; } = "swap";
    }
}
