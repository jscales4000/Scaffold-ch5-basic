using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace Contract_Svelte
{
    public interface IDeviceControl
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> Power;
        event EventHandler<UIEventArgs> Up;
        event EventHandler<UIEventArgs> Down;
        event EventHandler<UIEventArgs> Left;
        event EventHandler<UIEventArgs> Right;
        event EventHandler<UIEventArgs> Ok;
        event EventHandler<UIEventArgs> Back;
        event EventHandler<UIEventArgs> TV;
        event EventHandler<UIEventArgs> PlayPause;
        event EventHandler<UIEventArgs> MediaPlayer;

        void LoginCode(DeviceControlBoolInputSigDelegate callback);
        void MediaPlayerIsPlaying(DeviceControlBoolInputSigDelegate callback);
        void MediaPlayerCurrentTime(DeviceControlBoolInputSigDelegate callback);
        void MediaPlayerDuration(DeviceControlBoolInputSigDelegate callback);
        void Status(DeviceControlUShortInputSigDelegate callback);
        void NumberofUsersCon(DeviceControlUShortInputSigDelegate callback);
        void ConnectionAddress(DeviceControlStringInputSigDelegate callback);
        void LoginCodeText(DeviceControlStringInputSigDelegate callback);
        void IPAddressText(DeviceControlStringInputSigDelegate callback);
        void HostnameText(DeviceControlStringInputSigDelegate callback);

    }

    public delegate void DeviceControlBoolInputSigDelegate(BoolInputSig boolInputSig, IDeviceControl deviceControl);
    public delegate void DeviceControlUShortInputSigDelegate(UShortInputSig uShortInputSig, IDeviceControl deviceControl);
    public delegate void DeviceControlStringInputSigDelegate(StringInputSig stringInputSig, IDeviceControl deviceControl);

    internal class DeviceControl : IDeviceControl, IDisposable
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
                public const uint Power = 1;
                public const uint Up = 2;
                public const uint Down = 3;
                public const uint Left = 4;
                public const uint Right = 5;
                public const uint Ok = 6;
                public const uint Back = 7;
                public const uint TV = 8;
                public const uint PlayPause = 9;
                public const uint MediaPlayer = 14;

                public const uint LoginCode = 10;
                public const uint MediaPlayerIsPlaying = 11;
                public const uint MediaPlayerCurrentTime = 12;
                public const uint MediaPlayerDuration = 13;
            }
            internal static class Numerics
            {

                public const uint Status = 1;
                public const uint NumberofUsersCon = 2;
            }
            internal static class Strings
            {

                public const uint ConnectionAddress = 1;
                public const uint LoginCodeText = 2;
                public const uint IPAddressText = 3;
                public const uint HostnameText = 4;
            }
        }

        #endregion

        #region Construction and Initialization

        internal DeviceControl(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Power, onPower);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Up, onUp);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Down, onDown);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Left, onLeft);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Right, onRight);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Ok, onOk);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Back, onBack);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.TV, onTV);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.PlayPause, onPlayPause);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.MediaPlayer, onMediaPlayer);

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

        public event EventHandler<UIEventArgs> Power;
        private void onPower(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Power;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Up;
        private void onUp(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Up;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Down;
        private void onDown(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Down;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Left;
        private void onLeft(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Left;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Right;
        private void onRight(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Right;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Ok;
        private void onOk(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Ok;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Back;
        private void onBack(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Back;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> TV;
        private void onTV(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = TV;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> PlayPause;
        private void onPlayPause(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = PlayPause;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> MediaPlayer;
        private void onMediaPlayer(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = MediaPlayer;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void LoginCode(DeviceControlBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.LoginCode], this);
            }
        }

        public void MediaPlayerIsPlaying(DeviceControlBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.MediaPlayerIsPlaying], this);
            }
        }

        public void MediaPlayerCurrentTime(DeviceControlBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.MediaPlayerCurrentTime], this);
            }
        }

        public void MediaPlayerDuration(DeviceControlBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.MediaPlayerDuration], this);
            }
        }


        public void Status(DeviceControlUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.Status], this);
            }
        }

        public void NumberofUsersCon(DeviceControlUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberofUsersCon], this);
            }
        }


        public void ConnectionAddress(DeviceControlStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.ConnectionAddress], this);
            }
        }

        public void LoginCodeText(DeviceControlStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.LoginCodeText], this);
            }
        }

        public void IPAddressText(DeviceControlStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.IPAddressText], this);
            }
        }

        public void HostnameText(DeviceControlStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.HostnameText], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "DeviceControl", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            Power = null;
            Up = null;
            Down = null;
            Left = null;
            Right = null;
            Ok = null;
            Back = null;
            TV = null;
            PlayPause = null;
            MediaPlayer = null;
        }

        #endregion

    }
}
