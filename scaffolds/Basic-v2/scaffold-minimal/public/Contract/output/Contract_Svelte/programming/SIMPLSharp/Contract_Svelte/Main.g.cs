using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace Contract_Svelte
{
    public interface IMain
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> Power;
        event EventHandler<UIEventArgs> VolumeUp;
        event EventHandler<UIEventArgs> VolumeDown;
        event EventHandler<UIEventArgs> Mute;
        event EventHandler<UIEventArgs> StartPageButton;
        event EventHandler<UIEventArgs> ScreenUp;
        event EventHandler<UIEventArgs> ScreenDown;
        event EventHandler<UIEventArgs> MicrophoneMakeActive;
        event EventHandler<UIEventArgs> MicrophoneMakeInactive;
        event EventHandler<UIEventArgs> Source;

        void MuteFb(MainBoolInputSigDelegate callback);
        void StartPageActive(MainBoolInputSigDelegate callback);
        void PowerIsOn(MainBoolInputSigDelegate callback);
        void ProjectorWarming(MainBoolInputSigDelegate callback);
        void CameraControl(MainBoolInputSigDelegate callback);
        void ScreenControlNeeded(MainBoolInputSigDelegate callback);
        void MicrophoneRequired(MainBoolInputSigDelegate callback);
        void MicrophoneNotRequired(MainBoolInputSigDelegate callback);
        void PowerState(MainUShortInputSigDelegate callback);
        void Bargraph(MainUShortInputSigDelegate callback);
        void SourceFb(MainUShortInputSigDelegate callback);
        void SourceBtnCount(MainUShortInputSigDelegate callback);
        void RoomName(MainStringInputSigDelegate callback);
        void Source1Name(MainStringInputSigDelegate callback);
        void Source2Name(MainStringInputSigDelegate callback);
        void Source3Name(MainStringInputSigDelegate callback);
        void Source4Name(MainStringInputSigDelegate callback);
        void Source5Name(MainStringInputSigDelegate callback);
        void Source6Name(MainStringInputSigDelegate callback);
        void LampHours(MainStringInputSigDelegate callback);
        void Source7Name(MainStringInputSigDelegate callback);
        void Source8Name(MainStringInputSigDelegate callback);
        void HelpText(MainStringInputSigDelegate callback);

    }

    public delegate void MainBoolInputSigDelegate(BoolInputSig boolInputSig, IMain main);
    public delegate void MainUShortInputSigDelegate(UShortInputSig uShortInputSig, IMain main);
    public delegate void MainStringInputSigDelegate(StringInputSig stringInputSig, IMain main);

    internal class Main : IMain, IDisposable
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
                public const uint VolumeUp = 2;
                public const uint VolumeDown = 3;
                public const uint Mute = 4;
                public const uint StartPageButton = 6;
                public const uint ScreenUp = 8;
                public const uint ScreenDown = 9;
                public const uint MicrophoneMakeActive = 14;
                public const uint MicrophoneMakeInactive = 15;

                public const uint MuteFb = 4;
                public const uint StartPageActive = 5;
                public const uint PowerIsOn = 7;
                public const uint ProjectorWarming = 10;
                public const uint CameraControl = 11;
                public const uint ScreenControlNeeded = 12;
                public const uint MicrophoneRequired = 13;
                public const uint MicrophoneNotRequired = 14;
            }
            internal static class Numerics
            {
                public const uint Source = 3;

                public const uint PowerState = 1;
                public const uint Bargraph = 2;
                public const uint SourceFb = 3;
                public const uint SourceBtnCount = 4;
            }
            internal static class Strings
            {

                public const uint RoomName = 1;
                public const uint Source1Name = 2;
                public const uint Source2Name = 3;
                public const uint Source3Name = 4;
                public const uint Source4Name = 5;
                public const uint Source5Name = 6;
                public const uint Source6Name = 7;
                public const uint LampHours = 8;
                public const uint Source7Name = 10;
                public const uint Source8Name = 11;
                public const uint HelpText = 12;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Main(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Power, onPower);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.VolumeUp, onVolumeUp);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.VolumeDown, onVolumeDown);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Mute, onMute);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.StartPageButton, onStartPageButton);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.ScreenUp, onScreenUp);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.ScreenDown, onScreenDown);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.MicrophoneMakeActive, onMicrophoneMakeActive);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.MicrophoneMakeInactive, onMicrophoneMakeInactive);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.Source, onSource);

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

        public event EventHandler<UIEventArgs> VolumeUp;
        private void onVolumeUp(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = VolumeUp;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> VolumeDown;
        private void onVolumeDown(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = VolumeDown;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Mute;
        private void onMute(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Mute;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> StartPageButton;
        private void onStartPageButton(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = StartPageButton;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> ScreenUp;
        private void onScreenUp(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = ScreenUp;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> ScreenDown;
        private void onScreenDown(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = ScreenDown;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> MicrophoneMakeActive;
        private void onMicrophoneMakeActive(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = MicrophoneMakeActive;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> MicrophoneMakeInactive;
        private void onMicrophoneMakeInactive(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = MicrophoneMakeInactive;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void MuteFb(MainBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.MuteFb], this);
            }
        }

        public void StartPageActive(MainBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.StartPageActive], this);
            }
        }

        public void PowerIsOn(MainBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.PowerIsOn], this);
            }
        }

        public void ProjectorWarming(MainBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.ProjectorWarming], this);
            }
        }

        public void CameraControl(MainBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.CameraControl], this);
            }
        }

        public void ScreenControlNeeded(MainBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.ScreenControlNeeded], this);
            }
        }

        public void MicrophoneRequired(MainBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.MicrophoneRequired], this);
            }
        }

        public void MicrophoneNotRequired(MainBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.MicrophoneNotRequired], this);
            }
        }

        public event EventHandler<UIEventArgs> Source;
        private void onSource(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Source;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void PowerState(MainUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.PowerState], this);
            }
        }

        public void Bargraph(MainUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.Bargraph], this);
            }
        }

        public void SourceFb(MainUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.SourceFb], this);
            }
        }

        public void SourceBtnCount(MainUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.SourceBtnCount], this);
            }
        }


        public void RoomName(MainStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.RoomName], this);
            }
        }

        public void Source1Name(MainStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Source1Name], this);
            }
        }

        public void Source2Name(MainStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Source2Name], this);
            }
        }

        public void Source3Name(MainStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Source3Name], this);
            }
        }

        public void Source4Name(MainStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Source4Name], this);
            }
        }

        public void Source5Name(MainStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Source5Name], this);
            }
        }

        public void Source6Name(MainStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Source6Name], this);
            }
        }

        public void LampHours(MainStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.LampHours], this);
            }
        }

        public void Source7Name(MainStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Source7Name], this);
            }
        }

        public void Source8Name(MainStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Source8Name], this);
            }
        }

        public void HelpText(MainStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.HelpText], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Main", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
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
            VolumeUp = null;
            VolumeDown = null;
            Mute = null;
            StartPageButton = null;
            ScreenUp = null;
            ScreenDown = null;
            MicrophoneMakeActive = null;
            MicrophoneMakeInactive = null;
            Source = null;
        }

        #endregion

    }
}
