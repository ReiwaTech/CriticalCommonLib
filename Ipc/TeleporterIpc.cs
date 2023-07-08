using System;
using CriticalCommonLib.Interfaces;
using Dalamud.Logging;
using Dalamud.Plugin;
using Dalamud.Plugin.Ipc;

namespace CriticalCommonLib.Ipc;

public class TeleporterIpc : ITeleporterIpc
{
    private bool _isAvailable;
    private long _timeSinceLastCheck;

    public bool IsAvailable
    {
        get
        {
            if (this._timeSinceLastCheck + 5000 > Environment.TickCount64)
            {
                return this._isAvailable;
            }

            try
            {
                this._consumerMessageSetting.InvokeFunc();
                this._isAvailable = true;
                this._timeSinceLastCheck = Environment.TickCount64;
            }
            catch
            {
                this._isAvailable = false;
            }

            return this._isAvailable;
        }
    }

    private ICallGateSubscriber<bool> _consumerMessageSetting = null!;
    private ICallGateSubscriber<uint, byte, bool> _consumerTeleport = null!;
    private readonly DalamudPluginInterface _pluginInterface;

    private void Subscribe()
    {
        try
        {
            this._consumerTeleport = _pluginInterface.GetIpcSubscriber<uint, byte, bool>("Teleport");
            this._consumerMessageSetting = _pluginInterface.GetIpcSubscriber<bool>("Teleport.ChatMessage");
        }
        catch (Exception ex)
        {
            PluginLog.LogDebug($"Failed to subscribe to Teleporter\nReason: {ex}");
        }
    }

    public TeleporterIpc(DalamudPluginInterface pluginInterface)
    {
        _pluginInterface = pluginInterface;
        this.Subscribe();
    }

    public bool Teleport(uint aetheryteId)
    {
        try
        {
            return this._consumerTeleport.InvokeFunc(aetheryteId, 0);
        }
        catch
        {
            Service.Chat.PrintError("Teleporter plugin is not responding");
            return false;
        }
    }
}