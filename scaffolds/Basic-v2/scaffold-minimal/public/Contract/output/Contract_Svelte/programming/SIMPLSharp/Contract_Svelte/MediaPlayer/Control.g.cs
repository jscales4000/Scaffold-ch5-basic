using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace Contract_Svelte.MediaPlayer
{
    public interface IControl
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> up;
        event EventHandler<UIEventArgs> down;
        event EventHandler<UIEventArgs> left;
        event EventHandler<UIEventArgs> right;
        event EventHandler<UIEventArgs> OK;
        event EventHandler<UIEventArgs> Back;
        event EventHandler<UIEventArgs> TV;
        event EventHandler<UIEventArgs> PlayPause;
        event EventHandler<UIEventArgs> Pause;
        event EventHandler<UIEventArgs> Play;
        event EventHandler<UIEventArgs> Menu;
        event EventHandler<UIEventArgs> SmartTVControl;
        event EventHandler<UIEventArgs> Red;
        event EventHandler<UIEventArgs> Blue;
        event EventHandler<UIEventArgs> Green;
        event EventHandler<UIEventArgs> Yellow;
        event EventHandler<UIEventArgs> Guide;
        event EventHandler<UIEventArgs> Info;
        event EventHandler<UIEventArgs> Favorites;
        event EventHandler<UIEventArgs> PowerOn;
        event EventHandler<UIEventArgs> PowerOff;

        void MediaPlayerIsPlaying(ControlBoolInputSigDelegate callback);
        void MediaPlayerStatus(ControlUShortInputSigDelegate callback);
        void MediaPlayerCurrentTime(ControlStringInputSigDelegate callback);
        void MediaPlayerDuration(ControlStringInputSigDelegate callback);

    }

    public delegate void ControlBoolInputSigDelegate(BoolInputSig boolInputSig, IControl control);
    public delegate void ControlUShortInputSigDelegate(UShortInputSig uShortInputSig, IControl control);
    public delegate void ControlStringInputSigDelegate(StringInputSig stringInputSig, IControl control);

    internal class Control : IControl, IDisposable
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
                public const uint up = 1;
                public const uint down = 2;
                public const uint left = 3;
                public const uint right = 4;
                public const uint OK = 5;
                public const uint Back = 6;
                public const uint TV = 7;
                public const uint PlayPause = 8;
                public const uint Pause = 9;
                public const uint Play = 10;
                public const uint Menu = 11;
                public const uint SmartTVControl = 12;
                public const uint Red = 13;
                public const uint Blue = 14;
                public const uint Green = 15;
                public const uint Yellow = 16;
                public const uint Guide = 17;
                public const uint Info = 18;
                public const uint Favorites = 19;
                public const uint PowerOn = 20;
                public const uint PowerOff = 21;

                public const uint MediaPlayerIsPlaying = 22;
            }
            internal static class Numerics
            {
                public const uint MediaPlayerStatus = 1;
            }
            internal static class Strings
            {
                public const uint MediaPlayerCurrentTime = 1;
                public const uint MediaPlayerDuration = 2;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Control(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.up, onup);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.down, ondown);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.left, onleft);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.right, onright);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.OK, onOK);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Back, onBack);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.TV, onTV);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.PlayPause, onPlayPause);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Pause, onPause);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Play, onPlay);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Menu, onMenu);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.SmartTVControl, onSmartTVControl);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Red, onRed);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Blue, onBlue);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Green, onGreen);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Yellow, onYellow);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Guide, onGuide);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Info, onInfo);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Favorites, onFavorites);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.PowerOn, onPowerOn);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.PowerOff, onPowerOff);

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

        public event EventHandler<UIEventArgs> up;
        private void onup(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = up;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> down;
        private void ondown(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = down;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> left;
        private void onleft(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = left;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> right;
        private void onright(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = right;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> OK;
        private void onOK(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = OK;
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

        public event EventHandler<UIEventArgs> Pause;
        private void onPause(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Pause;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Play;
        private void onPlay(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Play;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Menu;
        private void onMenu(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Menu;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> SmartTVControl;
        private void onSmartTVControl(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = SmartTVControl;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Red;
        private void onRed(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Red;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Blue;
        private void onBlue(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Blue;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Green;
        private void onGreen(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Green;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Yellow;
        private void onYellow(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Yellow;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Guide;
        private void onGuide(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Guide;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Info;
        private void onInfo(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Info;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Favorites;
        private void onFavorites(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Favorites;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> PowerOn;
        private void onPowerOn(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = PowerOn;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> PowerOff;
        private void onPowerOff(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = PowerOff;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void MediaPlayerIsPlaying(ControlBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.MediaPlayerIsPlaying], this);
            }
        }

        public void MediaPlayerStatus(ControlUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.MediaPlayerStatus], this);
            }
        }

        public void MediaPlayerCurrentTime(ControlStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.MediaPlayerCurrentTime], this);
            }
        }

        public void MediaPlayerDuration(ControlStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.MediaPlayerDuration], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Control", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            up = null;
            down = null;
            left = null;
            right = null;
            OK = null;
            Back = null;
            TV = null;
            PlayPause = null;
            Pause = null;
            Play = null;
            Menu = null;
            SmartTVControl = null;
            Red = null;
            Blue = null;
            Green = null;
            Yellow = null;
            Guide = null;
            Info = null;
            Favorites = null;
            PowerOn = null;
            PowerOff = null;
        }

        #endregion

    }
}
