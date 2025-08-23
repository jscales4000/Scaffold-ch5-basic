using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace Contract_Svelte
{
    public interface ISettings
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> GuiMicActivate;
        event EventHandler<UIEventArgs> GuiScreenActivate;
        event EventHandler<UIEventArgs> VideoBlankEnable;
        event EventHandler<UIEventArgs> S1SetIconID;
        event EventHandler<UIEventArgs> S2SetIconID;
        event EventHandler<UIEventArgs> S3SetIconID;
        event EventHandler<UIEventArgs> S4SetIconID;
        event EventHandler<UIEventArgs> S5SetIconID;
        event EventHandler<UIEventArgs> S6SetIconID;
        event EventHandler<UIEventArgs> S7SetIconID;
        event EventHandler<UIEventArgs> S8SetIconID;
        event EventHandler<UIEventArgs> Line1GainSet;
        event EventHandler<UIEventArgs> Line2GainSet;
        event EventHandler<UIEventArgs> Line3GainSet;
        event EventHandler<UIEventArgs> Line4GainSet;
        event EventHandler<UIEventArgs> Mic1GainSet;
        event EventHandler<UIEventArgs> Mic2GainSet;
        event EventHandler<UIEventArgs> SourceCntrlID1;
        event EventHandler<UIEventArgs> SourceCntrlID2;
        event EventHandler<UIEventArgs> SourceCntrlID3;
        event EventHandler<UIEventArgs> SourceCntrlID4;
        event EventHandler<UIEventArgs> SourceCntrlID5;
        event EventHandler<UIEventArgs> SourceCntrlID6;
        event EventHandler<UIEventArgs> SourceCntrlID7;
        event EventHandler<UIEventArgs> SourceCntrlID8;
        event EventHandler<UIEventArgs> PasswordIs;

        void GuiMicActive(SettingsBoolInputSigDelegate callback);
        void GuiScreenActive(SettingsBoolInputSigDelegate callback);
        void VideoBlankIsEnabled(SettingsBoolInputSigDelegate callback);
        void S1IconIDFromControlSystem(SettingsUShortInputSigDelegate callback);
        void S2IconIDFromControlSystem(SettingsUShortInputSigDelegate callback);
        void S3IconIDFromControlSystem(SettingsUShortInputSigDelegate callback);
        void S4IconIDFromControlSystem(SettingsUShortInputSigDelegate callback);
        void S5IconIDFromControlSystem(SettingsUShortInputSigDelegate callback);
        void S6IconIDFromControlSystem(SettingsUShortInputSigDelegate callback);
        void S7IconIDFromControlSystem(SettingsUShortInputSigDelegate callback);
        void S8IconIDFromControlSystem(SettingsUShortInputSigDelegate callback);
        void Line1GainIs(SettingsUShortInputSigDelegate callback);
        void Line2GainIs(SettingsUShortInputSigDelegate callback);
        void Line3GainIs(SettingsUShortInputSigDelegate callback);
        void Line4GainIs(SettingsUShortInputSigDelegate callback);
        void Mic1GainIs(SettingsUShortInputSigDelegate callback);
        void Mic2GainIs(SettingsUShortInputSigDelegate callback);
        void SourceCntrlIDfromcntrlSys1(SettingsUShortInputSigDelegate callback);
        void SourceCntrlIDfromcntrlSys2(SettingsUShortInputSigDelegate callback);
        void SourceCntrlIDfromcntrlSys3(SettingsUShortInputSigDelegate callback);
        void SourceCntrlIDfromcntrlSys4(SettingsUShortInputSigDelegate callback);
        void SourceCntrlIDfromcntrlSys5(SettingsUShortInputSigDelegate callback);
        void SourceCntrlIDfromcntrlSys6(SettingsUShortInputSigDelegate callback);
        void SourceCntrlIDfromcntrlSys7(SettingsUShortInputSigDelegate callback);
        void SourceCntrlIDfromcntrlSys8(SettingsUShortInputSigDelegate callback);
        void SetPassword(SettingsStringInputSigDelegate callback);
        void IPAdress(SettingsStringInputSigDelegate callback);

    }

    public delegate void SettingsBoolInputSigDelegate(BoolInputSig boolInputSig, ISettings settings);
    public delegate void SettingsUShortInputSigDelegate(UShortInputSig uShortInputSig, ISettings settings);
    public delegate void SettingsStringInputSigDelegate(StringInputSig stringInputSig, ISettings settings);

    internal class Settings : ISettings, IDisposable
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
                public const uint GuiMicActivate = 3;
                public const uint GuiScreenActivate = 4;
                public const uint VideoBlankEnable = 5;

                public const uint GuiMicActive = 1;
                public const uint GuiScreenActive = 2;
                public const uint VideoBlankIsEnabled = 6;
            }
            internal static class Numerics
            {
                public const uint S1SetIconID = 9;
                public const uint S2SetIconID = 10;
                public const uint S3SetIconID = 11;
                public const uint S4SetIconID = 12;
                public const uint S5SetIconID = 13;
                public const uint S6SetIconID = 14;
                public const uint S7SetIconID = 15;
                public const uint S8SetIconID = 16;
                public const uint Line1GainSet = 17;
                public const uint Line2GainSet = 18;
                public const uint Line3GainSet = 19;
                public const uint Line4GainSet = 20;
                public const uint Mic1GainSet = 25;
                public const uint Mic2GainSet = 26;
                public const uint SourceCntrlID1 = 29;
                public const uint SourceCntrlID2 = 30;
                public const uint SourceCntrlID3 = 31;
                public const uint SourceCntrlID4 = 32;
                public const uint SourceCntrlID5 = 33;
                public const uint SourceCntrlID6 = 34;
                public const uint SourceCntrlID7 = 35;
                public const uint SourceCntrlID8 = 36;

                public const uint S1IconIDFromControlSystem = 1;
                public const uint S2IconIDFromControlSystem = 2;
                public const uint S3IconIDFromControlSystem = 3;
                public const uint S4IconIDFromControlSystem = 4;
                public const uint S5IconIDFromControlSystem = 5;
                public const uint S6IconIDFromControlSystem = 6;
                public const uint S7IconIDFromControlSystem = 7;
                public const uint S8IconIDFromControlSystem = 8;
                public const uint Line1GainIs = 21;
                public const uint Line2GainIs = 22;
                public const uint Line3GainIs = 23;
                public const uint Line4GainIs = 24;
                public const uint Mic1GainIs = 27;
                public const uint Mic2GainIs = 28;
                public const uint SourceCntrlIDfromcntrlSys1 = 37;
                public const uint SourceCntrlIDfromcntrlSys2 = 38;
                public const uint SourceCntrlIDfromcntrlSys3 = 39;
                public const uint SourceCntrlIDfromcntrlSys4 = 40;
                public const uint SourceCntrlIDfromcntrlSys5 = 41;
                public const uint SourceCntrlIDfromcntrlSys6 = 42;
                public const uint SourceCntrlIDfromcntrlSys7 = 43;
                public const uint SourceCntrlIDfromcntrlSys8 = 44;
            }
            internal static class Strings
            {
                public const uint PasswordIs = 2;

                public const uint SetPassword = 1;
                public const uint IPAdress = 3;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Settings(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.GuiMicActivate, onGuiMicActivate);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.GuiScreenActivate, onGuiScreenActivate);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.VideoBlankEnable, onVideoBlankEnable);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.S1SetIconID, onS1SetIconID);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.S2SetIconID, onS2SetIconID);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.S3SetIconID, onS3SetIconID);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.S4SetIconID, onS4SetIconID);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.S5SetIconID, onS5SetIconID);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.S6SetIconID, onS6SetIconID);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.S7SetIconID, onS7SetIconID);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.S8SetIconID, onS8SetIconID);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.Line1GainSet, onLine1GainSet);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.Line2GainSet, onLine2GainSet);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.Line3GainSet, onLine3GainSet);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.Line4GainSet, onLine4GainSet);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.Mic1GainSet, onMic1GainSet);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.Mic2GainSet, onMic2GainSet);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.SourceCntrlID1, onSourceCntrlID1);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.SourceCntrlID2, onSourceCntrlID2);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.SourceCntrlID3, onSourceCntrlID3);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.SourceCntrlID4, onSourceCntrlID4);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.SourceCntrlID5, onSourceCntrlID5);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.SourceCntrlID6, onSourceCntrlID6);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.SourceCntrlID7, onSourceCntrlID7);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.SourceCntrlID8, onSourceCntrlID8);
            ComponentMediator.ConfigureStringEvent(controlJoinId, Joins.Strings.PasswordIs, onPasswordIs);

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

        public event EventHandler<UIEventArgs> GuiMicActivate;
        private void onGuiMicActivate(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = GuiMicActivate;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> GuiScreenActivate;
        private void onGuiScreenActivate(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = GuiScreenActivate;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> VideoBlankEnable;
        private void onVideoBlankEnable(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = VideoBlankEnable;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void GuiMicActive(SettingsBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.GuiMicActive], this);
            }
        }

        public void GuiScreenActive(SettingsBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.GuiScreenActive], this);
            }
        }

        public void VideoBlankIsEnabled(SettingsBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.VideoBlankIsEnabled], this);
            }
        }

        public event EventHandler<UIEventArgs> S1SetIconID;
        private void onS1SetIconID(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = S1SetIconID;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> S2SetIconID;
        private void onS2SetIconID(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = S2SetIconID;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> S3SetIconID;
        private void onS3SetIconID(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = S3SetIconID;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> S4SetIconID;
        private void onS4SetIconID(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = S4SetIconID;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> S5SetIconID;
        private void onS5SetIconID(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = S5SetIconID;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> S6SetIconID;
        private void onS6SetIconID(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = S6SetIconID;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> S7SetIconID;
        private void onS7SetIconID(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = S7SetIconID;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> S8SetIconID;
        private void onS8SetIconID(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = S8SetIconID;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Line1GainSet;
        private void onLine1GainSet(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Line1GainSet;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Line2GainSet;
        private void onLine2GainSet(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Line2GainSet;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Line3GainSet;
        private void onLine3GainSet(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Line3GainSet;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Line4GainSet;
        private void onLine4GainSet(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Line4GainSet;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Mic1GainSet;
        private void onMic1GainSet(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Mic1GainSet;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Mic2GainSet;
        private void onMic2GainSet(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Mic2GainSet;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> SourceCntrlID1;
        private void onSourceCntrlID1(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = SourceCntrlID1;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> SourceCntrlID2;
        private void onSourceCntrlID2(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = SourceCntrlID2;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> SourceCntrlID3;
        private void onSourceCntrlID3(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = SourceCntrlID3;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> SourceCntrlID4;
        private void onSourceCntrlID4(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = SourceCntrlID4;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> SourceCntrlID5;
        private void onSourceCntrlID5(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = SourceCntrlID5;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> SourceCntrlID6;
        private void onSourceCntrlID6(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = SourceCntrlID6;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> SourceCntrlID7;
        private void onSourceCntrlID7(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = SourceCntrlID7;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> SourceCntrlID8;
        private void onSourceCntrlID8(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = SourceCntrlID8;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void S1IconIDFromControlSystem(SettingsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.S1IconIDFromControlSystem], this);
            }
        }

        public void S2IconIDFromControlSystem(SettingsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.S2IconIDFromControlSystem], this);
            }
        }

        public void S3IconIDFromControlSystem(SettingsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.S3IconIDFromControlSystem], this);
            }
        }

        public void S4IconIDFromControlSystem(SettingsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.S4IconIDFromControlSystem], this);
            }
        }

        public void S5IconIDFromControlSystem(SettingsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.S5IconIDFromControlSystem], this);
            }
        }

        public void S6IconIDFromControlSystem(SettingsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.S6IconIDFromControlSystem], this);
            }
        }

        public void S7IconIDFromControlSystem(SettingsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.S7IconIDFromControlSystem], this);
            }
        }

        public void S8IconIDFromControlSystem(SettingsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.S8IconIDFromControlSystem], this);
            }
        }

        public void Line1GainIs(SettingsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.Line1GainIs], this);
            }
        }

        public void Line2GainIs(SettingsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.Line2GainIs], this);
            }
        }

        public void Line3GainIs(SettingsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.Line3GainIs], this);
            }
        }

        public void Line4GainIs(SettingsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.Line4GainIs], this);
            }
        }

        public void Mic1GainIs(SettingsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.Mic1GainIs], this);
            }
        }

        public void Mic2GainIs(SettingsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.Mic2GainIs], this);
            }
        }

        public void SourceCntrlIDfromcntrlSys1(SettingsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.SourceCntrlIDfromcntrlSys1], this);
            }
        }

        public void SourceCntrlIDfromcntrlSys2(SettingsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.SourceCntrlIDfromcntrlSys2], this);
            }
        }

        public void SourceCntrlIDfromcntrlSys3(SettingsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.SourceCntrlIDfromcntrlSys3], this);
            }
        }

        public void SourceCntrlIDfromcntrlSys4(SettingsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.SourceCntrlIDfromcntrlSys4], this);
            }
        }

        public void SourceCntrlIDfromcntrlSys5(SettingsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.SourceCntrlIDfromcntrlSys5], this);
            }
        }

        public void SourceCntrlIDfromcntrlSys6(SettingsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.SourceCntrlIDfromcntrlSys6], this);
            }
        }

        public void SourceCntrlIDfromcntrlSys7(SettingsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.SourceCntrlIDfromcntrlSys7], this);
            }
        }

        public void SourceCntrlIDfromcntrlSys8(SettingsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.SourceCntrlIDfromcntrlSys8], this);
            }
        }

        public event EventHandler<UIEventArgs> PasswordIs;
        private void onPasswordIs(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = PasswordIs;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void SetPassword(SettingsStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.SetPassword], this);
            }
        }

        public void IPAdress(SettingsStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.IPAdress], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Settings", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            GuiMicActivate = null;
            GuiScreenActivate = null;
            VideoBlankEnable = null;
            S1SetIconID = null;
            S2SetIconID = null;
            S3SetIconID = null;
            S4SetIconID = null;
            S5SetIconID = null;
            S6SetIconID = null;
            S7SetIconID = null;
            S8SetIconID = null;
            Line1GainSet = null;
            Line2GainSet = null;
            Line3GainSet = null;
            Line4GainSet = null;
            Mic1GainSet = null;
            Mic2GainSet = null;
            SourceCntrlID1 = null;
            SourceCntrlID2 = null;
            SourceCntrlID3 = null;
            SourceCntrlID4 = null;
            SourceCntrlID5 = null;
            SourceCntrlID6 = null;
            SourceCntrlID7 = null;
            SourceCntrlID8 = null;
            PasswordIs = null;
        }

        #endregion

    }
}
