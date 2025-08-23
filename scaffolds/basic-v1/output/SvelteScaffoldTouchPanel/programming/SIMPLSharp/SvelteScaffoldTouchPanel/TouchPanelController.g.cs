using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace SvelteScaffoldTouchPanel
{
    /// <summary>
    /// Audio mute status
    /// </summary>
    /// <summary>
    /// Power status
    /// </summary>
    /// <summary>
    /// Current source (1-6)
    /// </summary>
    /// <summary>
    /// Volume level 0-65535
    /// </summary>
    /// <summary>
    /// Volume up button
    /// </summary>
    /// <summary>
    /// Volume down button
    /// </summary>
    /// <summary>
    /// Audio mute toggle
    /// </summary>
    /// <summary>
    /// System power
    /// </summary>
    /// <summary>
    /// Help button
    /// </summary>
    /// <summary>
    /// Settings button
    /// </summary>
    /// <summary>
    /// Source selection (1-6)
    /// </summary>
    public interface ITouchPanelController
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> IsMuted;
        event EventHandler<UIEventArgs> SystemPowered;
        event EventHandler<UIEventArgs> SelectedSource;
        event EventHandler<UIEventArgs> VolumeLevel;

        void VolumeUp(TouchPanelControllerBoolInputSigDelegate callback);
        void VolumeDown(TouchPanelControllerBoolInputSigDelegate callback);
        void MuteToggle(TouchPanelControllerBoolInputSigDelegate callback);
        void PowerButton(TouchPanelControllerBoolInputSigDelegate callback);
        void HelpButton(TouchPanelControllerBoolInputSigDelegate callback);
        void SettingsButton(TouchPanelControllerBoolInputSigDelegate callback);
        void SelectSource(TouchPanelControllerUShortInputSigDelegate callback);

    }

    public delegate void TouchPanelControllerBoolInputSigDelegate(BoolInputSig boolInputSig, ITouchPanelController touchPanelController);
    public delegate void TouchPanelControllerUShortInputSigDelegate(UShortInputSig uShortInputSig, ITouchPanelController touchPanelController);

    internal class TouchPanelController : ITouchPanelController, IDisposable
    {
        #region Standard CH5 Component members

        private ComponentMediator ComponentMediator { get; set; }

        public object UserObject { get; set; }

        public uint ControlJoinId { get; private set; }

        private IList<BasicTriListWithSmartObject> _devices;
        public IList<BasicTriListWithSmartObject> Devices { get { return _devices; } }

        #endregion

        #region Joins

        private static class Joins
        {
            internal static class Booleans
            {
                public const uint IsMuted = 1;
                public const uint SystemPowered = 2;

                public const uint VolumeUp = 1;
                public const uint VolumeDown = 2;
                public const uint MuteToggle = 3;
                public const uint PowerButton = 4;
                public const uint HelpButton = 5;
                public const uint SettingsButton = 6;
            }
            internal static class Numerics
            {
                public const uint SelectedSource = 1;
                public const uint VolumeLevel = 2;

                public const uint SelectSource = 1;
            }
        }

        #endregion

        #region Construction and Initialization

        internal TouchPanelController(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.IsMuted, onIsMuted);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.SystemPowered, onSystemPowered);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.SelectedSource, onSelectedSource);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.VolumeLevel, onVolumeLevel);

        }

        public void AddDevice(BasicTriListWithSmartObject device)
        {
            Devices.Add(device);
            ComponentMediator.HookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
        }

        public void RemoveDevice(BasicTriListWithSmartObject device)
        {
            Devices.Remove(device);
            ComponentMediator.UnHookSmartObjectEvents(device.SmartObjects[ControlJoinId]);
        }

        #endregion

        #region CH5 Contract

        public event EventHandler<UIEventArgs> IsMuted;
        private void onIsMuted(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = IsMuted;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> SystemPowered;
        private void onSystemPowered(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = SystemPowered;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void VolumeUp(TouchPanelControllerBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.VolumeUp], this);
            }
        }

        public void VolumeDown(TouchPanelControllerBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.VolumeDown], this);
            }
        }

        public void MuteToggle(TouchPanelControllerBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.MuteToggle], this);
            }
        }

        public void PowerButton(TouchPanelControllerBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.PowerButton], this);
            }
        }

        public void HelpButton(TouchPanelControllerBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.HelpButton], this);
            }
        }

        public void SettingsButton(TouchPanelControllerBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.SettingsButton], this);
            }
        }

        public event EventHandler<UIEventArgs> SelectedSource;
        private void onSelectedSource(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = SelectedSource;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> VolumeLevel;
        private void onVolumeLevel(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = VolumeLevel;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void SelectSource(TouchPanelControllerUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.SelectSource], this);
            }
        }

        #endregion

        #region Overrides

        public override int GetHashCode()
        {
            return (int)ControlJoinId;
        }

        public override string ToString()
        {
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "TouchPanelController", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            IsMuted = null;
            SystemPowered = null;
            SelectedSource = null;
            VolumeLevel = null;
        }

        #endregion

    }
}
