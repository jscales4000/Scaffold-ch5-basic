using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace Contract_Svelte.DocCam
{
    public interface IControl
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> CameraOn;
        event EventHandler<UIEventArgs> CameraOff;
        event EventHandler<UIEventArgs> FOCup;
        event EventHandler<UIEventArgs> FOCdown;
        event EventHandler<UIEventArgs> ZoomUp;
        event EventHandler<UIEventArgs> ZoomDown;
        event EventHandler<UIEventArgs> IrisUp;
        event EventHandler<UIEventArgs> IrisDown;
        event EventHandler<UIEventArgs> Preset1;
        event EventHandler<UIEventArgs> Preset2;
        event EventHandler<UIEventArgs> Preset3;
        event EventHandler<UIEventArgs> Preset1Save;
        event EventHandler<UIEventArgs> Preset2Save;
        event EventHandler<UIEventArgs> Preset3Save;

        void CameraIsOn(ControlBoolInputSigDelegate callback);

    }

    public delegate void ControlBoolInputSigDelegate(BoolInputSig boolInputSig, IControl control);

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
                public const uint CameraOn = 1;
                public const uint CameraOff = 2;
                public const uint FOCup = 3;
                public const uint FOCdown = 4;
                public const uint ZoomUp = 5;
                public const uint ZoomDown = 6;
                public const uint IrisUp = 7;
                public const uint IrisDown = 8;
                public const uint Preset1 = 9;
                public const uint Preset2 = 10;
                public const uint Preset3 = 11;
                public const uint Preset1Save = 12;
                public const uint Preset2Save = 13;
                public const uint Preset3Save = 14;

                public const uint CameraIsOn = 3;
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
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.CameraOn, onCameraOn);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.CameraOff, onCameraOff);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.FOCup, onFOCup);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.FOCdown, onFOCdown);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.ZoomUp, onZoomUp);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.ZoomDown, onZoomDown);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.IrisUp, onIrisUp);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.IrisDown, onIrisDown);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset1, onPreset1);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset2, onPreset2);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset3, onPreset3);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset1Save, onPreset1Save);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset2Save, onPreset2Save);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.Preset3Save, onPreset3Save);

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

        public event EventHandler<UIEventArgs> CameraOn;
        private void onCameraOn(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = CameraOn;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> CameraOff;
        private void onCameraOff(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = CameraOff;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> FOCup;
        private void onFOCup(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = FOCup;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> FOCdown;
        private void onFOCdown(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = FOCdown;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> ZoomUp;
        private void onZoomUp(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = ZoomUp;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> ZoomDown;
        private void onZoomDown(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = ZoomDown;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> IrisUp;
        private void onIrisUp(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = IrisUp;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> IrisDown;
        private void onIrisDown(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = IrisDown;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> Preset1;
        private void onPreset1(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Preset1;
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

        public event EventHandler<UIEventArgs> Preset1Save;
        private void onPreset1Save(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = Preset1Save;
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


        public void CameraIsOn(ControlBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.CameraIsOn], this);
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

            CameraOn = null;
            CameraOff = null;
            FOCup = null;
            FOCdown = null;
            ZoomUp = null;
            ZoomDown = null;
            IrisUp = null;
            IrisDown = null;
            Preset1 = null;
            Preset2 = null;
            Preset3 = null;
            Preset1Save = null;
            Preset2Save = null;
            Preset3Save = null;
        }

        #endregion

    }
}
