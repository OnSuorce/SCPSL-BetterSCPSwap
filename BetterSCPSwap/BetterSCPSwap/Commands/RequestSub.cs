using CommandSystem;
using System;
using RemoteAdmin;
using Exiled.API.Features;

namespace BetterSCPSwap.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    class RequestSub : ICommand
    {
        public string Command { get; set; } = Plugin.pluginInstance.Config.requestSwap;

        public string[] Aliases { get; set; } = null;

        public string Description => throw new NotImplementedException();

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "";
            if(sender is PlayerCommandSender playerSender)
            {
                Player player = Player.Get(playerSender.SenderId);
                if (Plugin.pluginInstance.playersToSwap.Contains(player))
                {
                    player.SendConsoleMessage("You already sent a request","red");

                }else if(player.Side == Exiled.API.Enums.Side.Scp)
                {
                    Plugin.pluginInstance.playersToSwap.Add(player);
                    player.SendConsoleMessage("You sent a request", "green");
                    var arr = arguments.Array;
                    
                    switch (arr[0])
                    {
                       case "classd":
                            {
                                foreach (var playr in Player.List)
                                {
                                    if(player.Role == RoleType.ClassD)
                                    {

                                        player.Broadcast(5, $"<color={player.RoleColor}> You</color> received a <color=red>SCP</color> swap request");
                                        player.SendConsoleMessage($"New scp request: ID={player.Id}","yellow");
                                        player.SendConsoleMessage($"Write: 'swap <id of player to swap>' to accept the request ","yellow");
                                    }
                                }
                                    break;
                            }
                        case "scientist":
                            {
                                foreach (var playr in Player.List)
                                {
                                    if (player.Role == RoleType.Scientist)
                                    {

                                        player.Broadcast(5, $"<color={player.RoleColor}> You</color> received a <color=red>SCP</color> swap request");
                                        player.SendConsoleMessage($"New scp request: ID={player.Id}", "yellow");
                                        player.SendConsoleMessage($"Write: 'swap <id of player to swap>' to accept the request ", "yellow");
                                    }
                                }
                                break;
                            }
                        case "facilityguard":
                            {
                                foreach (var playr in Player.List)
                                {
                                    if (player.Role == RoleType.FacilityGuard)
                                    {

                                        player.Broadcast(5, $"<color={player.RoleColor}> You</color> received a <color=red>SCP</color> swap request");
                                        player.SendConsoleMessage($"New scp request: ID={player.Id}", "yellow");
                                        player.SendConsoleMessage($"Write: 'swap <id of player to swap>' to accept the request ", "yellow");
                                    }
                                }
                                break;
                            }
                        case "scp":
                            {
                                foreach (var playr in Player.List)
                                {
                                    if (player.Side == Exiled.API.Enums.Side.Scp)
                                    {

                                        player.Broadcast(5, $"<color={player.RoleColor}> You</color> received a <color=red>SCP</color> swap request");
                                        player.SendConsoleMessage($"New scp request: ID={player.Id}", "yellow");
                                        player.SendConsoleMessage($"Write: 'swap <id of player to swap>' to accept the request ", "yellow");
                                    }
                                }
                                break;
                            }
                        case "all":
                            {
                                foreach (var playr in Player.List)
                                {
                                    if (player.Role == RoleType.ClassD)
                                    {

                                        player.Broadcast(5, $"You received a <color=red>SCP</color> swap request");
                                        player.SendConsoleMessage($"New scp request: ID={player.Id}", "yellow");
                                        player.SendConsoleMessage($"Write: 'swap <id of player to swap>' to accept the request ", "yellow");
                                    }
                                }
                                break;
                            }
                        default:
                            player.SendConsoleMessage("Invalid arguments. Command syntax: swaprequest <class to swap>. Classes: classd, scientist, facilityguard, scp, all", "red");
                            break;
                    }

                }
                else
                {
                    player.SendConsoleMessage("Something went wrong with your swap request", "red");
                }


            }
            return true;
        }
    }
}