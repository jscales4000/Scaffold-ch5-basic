using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace Contract_Svelte.AirMedia
{
    public interface IControls
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> AutomaticInputRoutingEnable;
        event EventHandler<UIEventArgs> AutomaticInputRoutingDisable;
        event EventHandler<UIEventArgs> Reboot;
        event EventHandler<UIEventArgs> VideoOut;
        event EventHandler<UIEventArgs> DigitalSignageState;
        event EventHandler<UIEventArgs> DigitalSignageURL;
        event EventHandler<UIEventArgs> LoginCodeSet;

        void OUTSyncDetected(ControlsBoolInputSigDelegate callback);
        void OUTInterlaced(ControlsBoolInputSigDelegate callback);
        void OutCECError(ControlsBoolInputSigDelegate callback);
        void OUTDisabledByHDCP(ControlsBoolInputSigDelegate callback);
        void InSyncDetectedFB(ControlsBoolInputSigDelegate callback);
        void INInterlacedFB(ControlsBoolInputSigDelegate callback);
        void AutomaticRoutingEnabledFB(ControlsBoolInputSigDelegate callback);
        void RebootRequired(ControlsBoolInputSigDelegate callback);
        void MiracastEnabledFB(ControlsBoolInputSigDelegate callback);
        void WifiDongleConnectedFB(ControlsBoolInputSigDelegate callback);
        void DeviceOfflineFB(ControlsBoolInputSigDelegate callback);
        void Status(ControlsUShortInputSigDelegate callback);
        void NumberOfUsersCon(ControlsUShortInputSigDelegate callback);
        void LoginCodeAnalog(ControlsUShortInputSigDelegate callback);
        void OUTResolutionFB(ControlsUShortInputSigDelegate callback);
        void OUTFPSFB(ControlsUShortInputSigDelegate callback);
        void OUTAspectRatioFB(ControlsUShortInputSigDelegate callback);
        void OUTAudioFormatFb(ControlsUShortInputSigDelegate callback);
        void OUTHDCPModeFB(ControlsUShortInputSigDelegate callback);
        void InHorizontalRes(ControlsUShortInputSigDelegate callback);
        void InVerticalRes(ControlsUShortInputSigDelegate callback);
        void InFPSFB(ControlsUShortInputSigDelegate callback);
        void InAspectRatio(ControlsUShortInputSigDelegate callback);
        void InAudioFormat(ControlsUShortInputSigDelegate callback);
        void VideoOutFB(ControlsUShortInputSigDelegate callback);
        void ActiveVideoFB(ControlsUShortInputSigDelegate callback);
        void DigitalSignageStateFB(ControlsUShortInputSigDelegate callback);
        void LoginCodeFB(ControlsUShortInputSigDelegate callback);
        void StatusFb(ControlsUShortInputSigDelegate callback);
        void NumberofUsersConnectedfb(ControlsUShortInputSigDelegate callback);
        void NumberofUsersPresentingFB(ControlsUShortInputSigDelegate callback);
        void LoginCodeText(ControlsStringInputSigDelegate callback);
        void IPAddressText(ControlsStringInputSigDelegate callback);
        void HostNameText(ControlsStringInputSigDelegate callback);
        void DigitalSignageURLFB(ControlsStringInputSigDelegate callback);
        void LoginCodeTextFB(ControlsStringInputSigDelegate callback);
        void IPAddressTextFB(ControlsStringInputSigDelegate callback);
        void HostnameTextFB(ControlsStringInputSigDelegate callback);

    }

    public delegate void ControlsBoolInputSigDelegate(BoolInputSig boolInputSig, IControls controls);
    public delegate void ControlsUShortInputSigDelegate(UShortInputSig uShortInputSig, IControls controls);
    public delegate void ControlsStringInputSigDelegate(StringInputSig stringInputSig, IControls controls);

    internal class Controls : IControls, IDisposable
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
                public const uint AutomaticInputRoutingEnable = 8;
                public const uint AutomaticInputRoutingDisable = 9;
                public const uint Reboot = 10;

                public const uint OUTSyncDetected = 1;
                public const uint OUTInterlaced = 2;
                public const uint OutCECError = 3;
                public const uint OUTDisabledByHDCP = 4;
                public const uint InSyncDetectedFB = 5;
                public const uint INInterlacedFB = 6;
                public const uint AutomaticRoutingEnabledFB = 7;
                public const uint RebootRequired = 10;
                public const uint MiracastEnabledFB = 12;
                public const uint WifiDongleConnectedFB = 13;
                public const uint DeviceOfflineFB = 14;
            }
            internal static class Numerics
            {
                public const uint VideoOut = 15;
                public const uint DigitalSignageState = 17;

                public const uint Status = 1;
                public const uint NumberOfUsersCon = 2;
                public const uint LoginCodeAnalog = 4;
                public const uint OUTResolutionFB = 5;
                public const uint OUTFPSFB = 6;
                public const uint OUTAspectRatioFB = 7;
                public const uint OUTAudioFormatFb = 8;
                public const uint OUTHDCPModeFB = 9;
                public const uint InHorizontalRes = 10;
                public const uint InVerticalRes = 11;
                public const uint InFPSFB = 12;
                public const uint InAspectRatio = 13;
                public const uint InAudioFormat = 14;
                public const uint VideoOutFB = 16;
                public const uint ActiveVideoFB = 17;
                public const uint DigitalSignageStateFB = 19;
                public const uint LoginCodeFB = 20;
                public const uint StatusFb = 21;
                public const uint NumberofUsersConnectedfb = 22;
                public const uint NumberofUsersPresentingFB = 23;
            }
            internal static class Strings
            {
                public const uint DigitalSignageURL = 7;
                public const uint LoginCodeSet = 8;

                public const uint LoginCodeText = 1;
                public const uint IPAddressText = 2;
                public const uint HostNameText = 3;
                public const uint DigitalSignageURLFB = 7;
                public const uint LoginCodeTextFB = 9;
                public const uint IPAddressTextFB = 10;
                public const uint HostnameTextFB = 11;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Controls(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.AutomaticInputRoutingEnable, onAutomaticInputRoutingEnable);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.AutomaticInputRoutingDisable, onAutomaticInputRoutingDisable);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Reboot, onReboot);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.VideoOut, onVideoOut);
            ComponentMediator.ConfigureNumericEvent(controlJoinId, Joins.Numerics.DigitalSignageState, onDigitalSignageState);
            ComponentMediator.ConfigureStringEvent(controlJoinId, Joins.Strings.DigitalSignageURL, onDigitalSignageURL);
            ComponentMediator.ConfigureStringEvent(controlJoinId, Joins.Strings.LoginCodeSet, onLoginCodeSet);

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

        public event EventHandler<UIEventArgs> AutomaticInputRoutingEnable;
        private void onAutomaticInputRoutingEnable(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = AutomaticInputRoutingEnable;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> AutomaticInputRoutingDisable;
        private void onAutomaticInputRoutingDisable(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = AutomaticInputRoutingDisable;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Reboot;
        private void onReboot(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Reboot;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void OUTSyncDetected(ControlsBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.OUTSyncDetected], this);
            }
        }

        public void OUTInterlaced(ControlsBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.OUTInterlaced], this);
            }
        }

        public void OutCECError(ControlsBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.OutCECError], this);
            }
        }

        public void OUTDisabledByHDCP(ControlsBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.OUTDisabledByHDCP], this);
            }
        }

        public void InSyncDetectedFB(ControlsBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.InSyncDetectedFB], this);
            }
        }

        public void INInterlacedFB(ControlsBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.INInterlacedFB], this);
            }
        }

        public void AutomaticRoutingEnabledFB(ControlsBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.AutomaticRoutingEnabledFB], this);
            }
        }

        public void RebootRequired(ControlsBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.RebootRequired], this);
            }
        }

        public void MiracastEnabledFB(ControlsBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.MiracastEnabledFB], this);
            }
        }

        public void WifiDongleConnectedFB(ControlsBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.WifiDongleConnectedFB], this);
            }
        }

        public void DeviceOfflineFB(ControlsBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.DeviceOfflineFB], this);
            }
        }

        public event EventHandler<UIEventArgs> VideoOut;
        private void onVideoOut(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = VideoOut;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> DigitalSignageState;
        private void onDigitalSignageState(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = DigitalSignageState;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void Status(ControlsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.Status], this);
            }
        }

        public void NumberOfUsersCon(ControlsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOfUsersCon], this);
            }
        }

        public void LoginCodeAnalog(ControlsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.LoginCodeAnalog], this);
            }
        }

        public void OUTResolutionFB(ControlsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.OUTResolutionFB], this);
            }
        }

        public void OUTFPSFB(ControlsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.OUTFPSFB], this);
            }
        }

        public void OUTAspectRatioFB(ControlsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.OUTAspectRatioFB], this);
            }
        }

        public void OUTAudioFormatFb(ControlsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.OUTAudioFormatFb], this);
            }
        }

        public void OUTHDCPModeFB(ControlsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.OUTHDCPModeFB], this);
            }
        }

        public void InHorizontalRes(ControlsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.InHorizontalRes], this);
            }
        }

        public void InVerticalRes(ControlsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.InVerticalRes], this);
            }
        }

        public void InFPSFB(ControlsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.InFPSFB], this);
            }
        }

        public void InAspectRatio(ControlsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.InAspectRatio], this);
            }
        }

        public void InAudioFormat(ControlsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.InAudioFormat], this);
            }
        }

        public void VideoOutFB(ControlsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.VideoOutFB], this);
            }
        }

        public void ActiveVideoFB(ControlsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.ActiveVideoFB], this);
            }
        }

        public void DigitalSignageStateFB(ControlsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.DigitalSignageStateFB], this);
            }
        }

        public void LoginCodeFB(ControlsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.LoginCodeFB], this);
            }
        }

        public void StatusFb(ControlsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.StatusFb], this);
            }
        }

        public void NumberofUsersConnectedfb(ControlsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberofUsersConnectedfb], this);
            }
        }

        public void NumberofUsersPresentingFB(ControlsUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberofUsersPresentingFB], this);
            }
        }

        public event EventHandler<UIEventArgs> DigitalSignageURL;
        private void onDigitalSignageURL(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = DigitalSignageURL;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> LoginCodeSet;
        private void onLoginCodeSet(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = LoginCodeSet;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void LoginCodeText(ControlsStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.LoginCodeText], this);
            }
        }

        public void IPAddressText(ControlsStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.IPAddressText], this);
            }
        }

        public void HostNameText(ControlsStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.HostNameText], this);
            }
        }

        public void DigitalSignageURLFB(ControlsStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.DigitalSignageURLFB], this);
            }
        }

        public void LoginCodeTextFB(ControlsStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.LoginCodeTextFB], this);
            }
        }

        public void IPAddressTextFB(ControlsStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.IPAddressTextFB], this);
            }
        }

        public void HostnameTextFB(ControlsStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.HostnameTextFB], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Controls", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            AutomaticInputRoutingEnable = null;
            AutomaticInputRoutingDisable = null;
            Reboot = null;
            VideoOut = null;
            DigitalSignageState = null;
            DigitalSignageURL = null;
            LoginCodeSet = null;
        }

        #endregion

    }
}
