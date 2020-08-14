using Exiled.API.Features;
using Exiled.Events;
using Exiled.Events.EventArgs;
using System.Collections.Generic;
using MEC;
using System;

namespace BetterSCPSwap.Handlers
{
    class SwapEvent
    {
        public List<Player> playersToSwap;

        public SwapEvent()
        {
            playersToSwap = new List<Player>();
        }

        public void onConsoleCommand(SendingConsoleCommandEventArgs ev)
        {
           //Request
            if(ev.Player.Team == Team.SCP && ev.Name == Plugin.pluginInstance.Config.requestSwap)

            {
                if (playersToSwap.Contains(ev.Player))
                {
                    ev.Player.SendConsoleMessage("You already sent a request", "red");
                    return;
                }
                if (ev.Arguments is null)
                {
                    ev.Player.SendConsoleMessage($"Invalid arguments. Command syntax: {Plugin.pluginInstance.Config.requestSwap} <class to swap>. Classes: classd, scientist, facilityguard, scp, all", "red");
                    return;
                }
                ev.Allow = true;
                requestSwap(ev.Player, ev.Arguments);
                
                ev.ReturnMessage = "";

            }
            //Accept
            else if(ev.Name == Plugin.pluginInstance.Config.acceptSwap)
            {
                if(ev.Player.Role == RoleType.Spectator)
                {
                    ev.Player.SendConsoleMessage($"You can't ask for a swap", "red");
                    return;
                
                }else if (playersToSwap.Count<1)
                {
                    ev.Player.SendConsoleMessage($"There are no requests", "red");
                }
                else if (ev.Arguments is null)
                {
                    ev.Player.SendConsoleMessage($"Invalid arguments. Command syntax: {Plugin.pluginInstance.Config.acceptSwap} <player id to swap>.", "red");
                    return;
                }else if(ev.Player.Id == int.Parse(ev.Arguments[0]))
                {
                    ev.Player.SendConsoleMessage($"You can't swap yourself", "red");
                    return;
                }


                else acceptSwap(ev.Player, ev.Arguments);
            }     
        }



        private void requestSwap(Player player, List<string> arguments)
        {
            
            switch (arguments[0])
            {
                case "classd":
                    {
                        foreach (var playr in Player.List)
                        {
                            if (playr.Role == RoleType.ClassD)
                            {

                                playr.Broadcast(5, $"<color={playr.RoleColor}> You</color> received a <color=red>SCP</color> swap request");
                                playr.SendConsoleMessage($"New scp request: ID={player.Id}", "yellow");
                                playr.SendConsoleMessage($"Write: 'swap <id of player to swap>' to accept the request ", "yellow");
                            }
                        }
                        break;
                    }
                case "scientist":
                    {
                        foreach (var playr in Player.List)
                        {
                            if (playr.Role == RoleType.Scientist)
                            {

                                player.Broadcast(5, $"<color={playr.RoleColor}> You</color> received a <color=red>SCP</color> swap request");
                                playr.SendConsoleMessage($"New scp request: ID={player.Id}", "yellow");
                                playr.SendConsoleMessage($"Write: 'swap <id of player to swap>' to accept the request ", "yellow");
                            }
                        }
                        break;
                    }
                case "facilityguard":
                    {
                        foreach (var playr in Player.List)
                        {
                            if (playr.Role == RoleType.FacilityGuard)
                            {

                                playr.Broadcast(5, $"<color={playr.RoleColor}> You</color> received a <color=red>SCP</color> swap request");
                                playr.SendConsoleMessage($"New scp request: ID={player.Id}", "yellow");
                                playr.SendConsoleMessage($"Write: 'swap <id of player to swap>' to accept the request ", "yellow");
                            }
                        }
                        break;
                    }
                case "scp":
                    {
                        foreach (var playr in Player.List)
                        {
                            if(playr == player)
                            {

                            }else if (playr.Side == Exiled.API.Enums.Side.Scp)
                            {

                                playr.Broadcast(5, $"<color={playr.RoleColor}> You</color> received a <color=red>SCP</color> swap request");
                                playr.SendConsoleMessage($"New scp request: ID={player.Id}", "yellow");
                                playr.SendConsoleMessage($"Write: 'swap <id of player to swap>' to accept the request ", "yellow");
                            }
                        }
                        break;
                    }
                case "all":
                    {
                        foreach (var playr in Player.List)
                        {
                             playr.Broadcast(5, $"You received a <color=red>SCP</color> swap request");
                             playr.SendConsoleMessage($"New scp request: ID={player.Id}", "yellow");
                             playr.SendConsoleMessage($"Write: 'swap <id of player to swap>' to accept the request ", "yellow");
                        }
                        break;
                    }
                default:
                    player.SendConsoleMessage("Invalid arguments. Command syntax: swaprequest <class to swap>. Classes: classd, scientist, facilityguard, scp, all", "red");
                    return;
            }
            playersToSwap.Add(player);
        }
        private void acceptSwap(Player sender, List<string> arguments)
        {
            foreach (var player in playersToSwap)
            {
                if (int.Parse(arguments[0]) == player.Id)
                {
                    
                    player.Broadcast(5, $"You swapped with <color={sender.RoleColor}> {sender.Nickname}</color>");
                    sender.Broadcast(5, $"You swapped with <color={player.RoleColor}> {player.Nickname}</color>");
                    List<ItemType> virtualInventory = new List<ItemType>();
                    foreach (var item in sender.Inventory.items)
                    {
                        
                        virtualInventory.Add(item.id);
                    }
                    
                    
                    sender.ClearInventory();


                    foreach (var item in virtualInventory)
                    {
                        player.AddItem(item);
                    }

                    playersToSwap.Remove(player);

                }
            }
        }
    }
}
