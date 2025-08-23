using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace Contract_Svelte.Camera
{
    public interface IControl
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> EnableAutoTracking;
        event EventHandler<UIEventArgs> DisableAutoTracking;
        event EventHandler<UIEventArgs> Up;
        event EventHandler<UIEventArgs> Down;
        event EventHandler<UIEventArgs> Left;
        event EventHandler<UIEventArgs> Right;
        event EventHandler<UIEventArgs> ZoomIn;
        event EventHandler<UIEventArgs> ZoomOut;
        event EventHandler<UIEventArgs> GetFocusMode;
        event EventHandler<UIEventArgs> CameraFocusAutoOn;
        event EventHandler<UIEventArgs> CameraFocusManualOn;
        event EventHandler<UIEventArgs> CameraFocusNear;
        event EventHandler<UIEventArgs> CameraFocusFar;
        event EventHandler<UIEventArgs> GetExposureMode;
        event EventHandler<UIEventArgs> CameraExposureManualOn;
        event EventHandler<UIEventArgs> CameraIrisOpen;
        event EventHandler<UIEventArgs> CameraIrisClose;
        event EventHandler<UIEventArgs> Home;
        event EventHandler<UIEventArgs> TrackingPosition;
        event EventHandler<UIEventArgs> Preset2;
        event EventHandler<UIEventArgs> Preset3;
        event EventHandler<UIEventArgs> Preset4;
        event EventHandler<UIEventArgs> Preset5;
        event EventHandler<UIEventArgs> Preset6;
        event EventHandler<UIEventArgs> Preset7;
        event EventHandler<UIEventArgs> Preset8;
        event EventHandler<UIEventArgs> Preset9;
        event EventHandler<UIEventArgs> Preset10;
        event EventHandler<UIEventArgs> Preset11;
        event EventHandler<UIEventArgs> Preset12;
        event EventHandler<UIEventArgs> Preset2Save;
        event EventHandler<UIEventArgs> Preset3Save;
        event EventHandler<UIEventArgs> Preset4Save;
        event EventHandler<UIEventArgs> Preset5Save;
        event EventHandler<UIEventArgs> Preset6Save;
        event EventHandler<UIEventArgs> Preset7Save;
        event EventHandler<UIEventArgs> Preset8Save;
        event EventHandler<UIEventArgs> Preset9Save;
        event EventHandler<UIEventArgs> Preset10Save;
        event EventHandler<UIEventArgs> Preset11Save;
        event EventHandler<UIEventArgs> Preset12Save;
        event EventHandler<UIEventArgs> AutoTrackerTX;

        void AutoTrackingIsOn(ControlBoolInputSigDelegate callback);
        void AutoTrackingIsOFf(ControlBoolInputSigDelegate callback);
        void CameraFocusAutoIsOn(ControlBoolInputSigDelegate callback);
        void CameraFocusManualIsOn(ControlBoolInputSigDelegate callback);
        void CameraExposureAutoIsOn(ControlBoolInputSigDelegate callback);
        void CameraExposureManualIsOn(ControlBoolInputSigDelegate callback);
        void AutoTrackerRX(ControlStringInputSigDelegate callback);
        void CameraVideoURL1(ControlStringInputSigDelegate callback);
        void CameraVideoURL2(ControlStringInputSigDelegate callback);

    }

    public delegate void ControlBoolInputSigDelegate(BoolInputSig boolInputSig, IControl control);
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
                public const uint EnableAutoTracking = 1;
                public const uint DisableAutoTracking = 2;
                public const uint Up = 3;
                public const uint Down = 4;
                public const uint Left = 5;
                public const uint Right = 6;
                public const uint ZoomIn = 7;
                public const uint ZoomOut = 8;
                public const uint GetFocusMode = 9;
                public const uint CameraFocusAutoOn = 10;
                public const uint CameraFocusManualOn = 11;
                public const uint CameraFocusNear = 12;
                public const uint CameraFocusFar = 13;
                public const uint GetExposureMode = 14;
                public const uint CameraExposureManualOn = 15;
                public const uint CameraIrisOpen = 16;
                public const uint CameraIrisClose = 17;
                public const uint Home = 18;
                public const uint TrackingPosition = 19;
                public const uint Preset2 = 20;
                public const uint Preset3 = 21;
                public const uint Preset4 = 22;
                public const uint Preset5 = 23;
                public const uint Preset6 = 24;
                public const uint Preset7 = 25;
                public const uint Preset8 = 26;
                public const uint Preset9 = 27;
                public const uint Preset10 = 28;
                public const uint Preset11 = 29;
                public const uint Preset12 = 30;
                public const uint Preset2Save = 31;
                public const uint Preset3Save = 32;
                public const uint Preset4Save = 33;
                public const uint Preset5Save = 34;
                public const uint Preset6Save = 35;
                public const uint Preset7Save = 36;
                public const uint Preset8Save = 37;
                public const uint Preset9Save = 38;
                public const uint Preset10Save = 39;
                public const uint Preset11Save = 40;
                public const uint Preset12Save = 41;

                public const uint AutoTrackingIsOn = 2;
                public const uint AutoTrackingIsOFf = 4;
                public const uint CameraFocusAutoIsOn = 13;
                public const uint CameraFocusManualIsOn = 15;
                public const uint CameraExposureAutoIsOn = 20;
                public const uint CameraExposureManualIsOn = 22;
            }
            internal static class Strings
            {
                public const uint AutoTrackerTX = 1;

                public const uint AutoTrackerRX = 2;
                public const uint CameraVideoURL1 = 3;
                public const uint CameraVideoURL2 = 4;
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
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.EnableAutoTracking, onEnableAutoTracking);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.DisableAutoTracking, onDisableAutoTracking);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Up, onUp);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Down, onDown);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Left, onLeft);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Right, onRight);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.ZoomIn, onZoomIn);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.ZoomOut, onZoomOut);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.GetFocusMode, onGetFocusMode);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.CameraFocusAutoOn, onCameraFocusAutoOn);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.CameraFocusManualOn, onCameraFocusManualOn);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.CameraFocusNear, onCameraFocusNear);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.CameraFocusFar, onCameraFocusFar);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.GetExposureMode, onGetExposureMode);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.CameraExposureManualOn, onCameraExposureManualOn);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.CameraIrisOpen, onCameraIrisOpen);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.CameraIrisClose, onCameraIrisClose);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Home, onHome);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.TrackingPosition, onTrackingPosition);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset2, onPreset2);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset3, onPreset3);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset4, onPreset4);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset5, onPreset5);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset6, onPreset6);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset7, onPreset7);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset8, onPreset8);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset9, onPreset9);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset10, onPreset10);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset11, onPreset11);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset12, onPreset12);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset2Save, onPreset2Save);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset3Save, onPreset3Save);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset4Save, onPreset4Save);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset5Save, onPreset5Save);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset6Save, onPreset6Save);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset7Save, onPreset7Save);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset8Save, onPreset8Save);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset9Save, onPreset9Save);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset10Save, onPreset10Save);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset11Save, onPreset11Save);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset12Save, onPreset12Save);
            ComponentMediator.ConfigureStringEvent(controlJoinId, Joins.Strings.AutoTrackerTX, onAutoTrackerTX);

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

        public event EventHandler<UIEventArgs> EnableAutoTracking;
        private void onEnableAutoTracking(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = EnableAutoTracking;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> DisableAutoTracking;
        private void onDisableAutoTracking(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = DisableAutoTracking;
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

        public event EventHandler<UIEventArgs> ZoomIn;
        private void onZoomIn(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = ZoomIn;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> ZoomOut;
        private void onZoomOut(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = ZoomOut;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> GetFocusMode;
        private void onGetFocusMode(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = GetFocusMode;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> CameraFocusAutoOn;
        private void onCameraFocusAutoOn(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = CameraFocusAutoOn;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> CameraFocusManualOn;
        private void onCameraFocusManualOn(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = CameraFocusManualOn;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> CameraFocusNear;
        private void onCameraFocusNear(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = CameraFocusNear;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> CameraFocusFar;
        private void onCameraFocusFar(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = CameraFocusFar;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> GetExposureMode;
        private void onGetExposureMode(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = GetExposureMode;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> CameraExposureManualOn;
        private void onCameraExposureManualOn(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = CameraExposureManualOn;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> CameraIrisOpen;
        private void onCameraIrisOpen(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = CameraIrisOpen;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> CameraIrisClose;
        private void onCameraIrisClose(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = CameraIrisClose;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Home;
        private void onHome(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Home;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> TrackingPosition;
        private void onTrackingPosition(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = TrackingPosition;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Preset2;
        private void onPreset2(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Preset2;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Preset3;
        private void onPreset3(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Preset3;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Preset4;
        private void onPreset4(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Preset4;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Preset5;
        private void onPreset5(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Preset5;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Preset6;
        private void onPreset6(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Preset6;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Preset7;
        private void onPreset7(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Preset7;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Preset8;
        private void onPreset8(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Preset8;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Preset9;
        private void onPreset9(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Preset9;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Preset10;
        private void onPreset10(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Preset10;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Preset11;
        private void onPreset11(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Preset11;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Preset12;
        private void onPreset12(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Preset12;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Preset2Save;
        private void onPreset2Save(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Preset2Save;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Preset3Save;
        private void onPreset3Save(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Preset3Save;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Preset4Save;
        private void onPreset4Save(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Preset4Save;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Preset5Save;
        private void onPreset5Save(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Preset5Save;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Preset6Save;
        private void onPreset6Save(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Preset6Save;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Preset7Save;
        private void onPreset7Save(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Preset7Save;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Preset8Save;
        private void onPreset8Save(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Preset8Save;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Preset9Save;
        private void onPreset9Save(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Preset9Save;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Preset10Save;
        private void onPreset10Save(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Preset10Save;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Preset11Save;
        private void onPreset11Save(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Preset11Save;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Preset12Save;
        private void onPreset12Save(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Preset12Save;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void AutoTrackingIsOn(ControlBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.AutoTrackingIsOn], this);
            }
        }

        public void AutoTrackingIsOFf(ControlBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.AutoTrackingIsOFf], this);
            }
        }

        public void CameraFocusAutoIsOn(ControlBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.CameraFocusAutoIsOn], this);
            }
        }

        public void CameraFocusManualIsOn(ControlBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.CameraFocusManualIsOn], this);
            }
        }

        public void CameraExposureAutoIsOn(ControlBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.CameraExposureAutoIsOn], this);
            }
        }

        public void CameraExposureManualIsOn(ControlBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.CameraExposureManualIsOn], this);
            }
        }

        public event EventHandler<UIEventArgs> AutoTrackerTX;
        private void onAutoTrackerTX(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = AutoTrackerTX;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void AutoTrackerRX(ControlStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.AutoTrackerRX], this);
            }
        }

        public void CameraVideoURL1(ControlStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.CameraVideoURL1], this);
            }
        }

        public void CameraVideoURL2(ControlStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.CameraVideoURL2], this);
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

            EnableAutoTracking = null;
            DisableAutoTracking = null;
            Up = null;
            Down = null;
            Left = null;
            Right = null;
            ZoomIn = null;
            ZoomOut = null;
            GetFocusMode = null;
            CameraFocusAutoOn = null;
            CameraFocusManualOn = null;
            CameraFocusNear = null;
            CameraFocusFar = null;
            GetExposureMode = null;
            CameraExposureManualOn = null;
            CameraIrisOpen = null;
            CameraIrisClose = null;
            Home = null;
            TrackingPosition = null;
            Preset2 = null;
            Preset3 = null;
            Preset4 = null;
            Preset5 = null;
            Preset6 = null;
            Preset7 = null;
            Preset8 = null;
            Preset9 = null;
            Preset10 = null;
            Preset11 = null;
            Preset12 = null;
            Preset2Save = null;
            Preset3Save = null;
            Preset4Save = null;
            Preset5Save = null;
            Preset6Save = null;
            Preset7Save = null;
            Preset8Save = null;
            Preset9Save = null;
            Preset10Save = null;
            Preset11Save = null;
            Preset12Save = null;
            AutoTrackerTX = null;
        }

        #endregion

    }
}
