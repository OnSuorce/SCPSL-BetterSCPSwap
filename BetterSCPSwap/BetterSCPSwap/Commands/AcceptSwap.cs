using CommandSystem;
using System;
using RemoteAdmin;
using Exiled.API.Features;

namespace BetterSCPSwap.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    class AcceptSwap : ICommand
    {
        public string Command { get; set; } = Plugin.pluginInstance.Config.acceptSwap;

        public string[] Aliases { get; set; } = null;

        public string Description => throw new NotImplementedException();

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "";
            if(sender is PlayerCommandSender playerSender)
            {
                string[] arr = arguments.Array;
                Player player = Player.Get(playerSender.SenderId);
                foreach(var playr in Plugin.pluginInstance.playersToSwap)
                {
                    if(int.Parse(arr[0]) == player.Id)
                    {
                        player.SendConsoleMessage("You can't swap yourself", "red");
                        return true;
                    }
                    if(int.Parse(arr[0]) == playr.Id)
                    {
                        player.Broadcast(5, $"You swapped with <color={playr.RoleColor}> {playr.Nickname}</color>");
                        playr.Broadcast(5, $"You swapped with <color={player.RoleColor}> {player.Nickname}</color>");
                        var inventory1 = new Inventory();
                        inventory1.items = player.Inventory.items;
                        player.ClearInventory();
                        var role = playr.Role;
                        playr.SetRole(player.Role);
                        player.SetRole(role);
                         
                        foreach(var item in inventory1.items)
                        {
                            playr.AddItem(item);
                        }
                        return true;
                    }
                }
                player.SendConsoleMessage("Some thing went wrong", "red");
            }
            return true;
        }
    }
}
