using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace Contract_Svelte.LectureCapture
{
    /// <summary>
    /// In MB
    /// </summary>
    /// <summary>
    /// In MB
    /// </summary>
    /// <summary>
    /// In MB
    /// </summary>
    public interface IControl
    {
        object UserObject { get; set; }

        event EventHandler<UIEventArgs> StartRecording;
        event EventHandler<UIEventArgs> StopRecording;
        event EventHandler<UIEventArgs> StartRecordingOnChannel1;
        event EventHandler<UIEventArgs> StartRecordingOnChannel2;
        event EventHandler<UIEventArgs> StartRecordingOnChannel3;
        event EventHandler<UIEventArgs> StartRecordingOnChannel4;
        event EventHandler<UIEventArgs> StopRecordingOnChannel1;
        event EventHandler<UIEventArgs> StopRecordingOnChannel2;
        event EventHandler<UIEventArgs> StopRecordingOnChannel3;
        event EventHandler<UIEventArgs> StopRecordingOnChannel4;
        event EventHandler<UIEventArgs> CurrentEventPause;
        event EventHandler<UIEventArgs> CurrentEventResume;
        event EventHandler<UIEventArgs> CurrentEventStop;
        event EventHandler<UIEventArgs> CurrentEventExtend;
        event EventHandler<UIEventArgs> StartURLVideoFeed;

        void IsCommunicatingWdevice(ControlBoolInputSigDelegate callback);
        void NumberOfRecordersAvailable(ControlUShortInputSigDelegate callback);
        void ChannelPreviewURLFB(ControlStringInputSigDelegate callback);
        void UpcomingEventNameFB(ControlStringInputSigDelegate callback);
        void UpcomingEventCountdownFB(ControlStringInputSigDelegate callback);
        void CurrentEventNameFB(ControlStringInputSigDelegate callback);
        void UsedSpaceSerial(ControlStringInputSigDelegate callback);
        void TotalSpaceSerial(ControlStringInputSigDelegate callback);
        void FreeSpaceSerial(ControlStringInputSigDelegate callback);

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
                public const uint StartRecording = 1;
                public const uint StopRecording = 2;
                public const uint StartRecordingOnChannel1 = 3;
                public const uint StartRecordingOnChannel2 = 4;
                public const uint StartRecordingOnChannel3 = 5;
                public const uint StartRecordingOnChannel4 = 6;
                public const uint StopRecordingOnChannel1 = 7;
                public const uint StopRecordingOnChannel2 = 8;
                public const uint StopRecordingOnChannel3 = 9;
                public const uint StopRecordingOnChannel4 = 10;
                public const uint CurrentEventPause = 11;
                public const uint CurrentEventResume = 12;
                public const uint CurrentEventStop = 13;
                public const uint CurrentEventExtend = 14;
                public const uint StartURLVideoFeed = 15;

                public const uint IsCommunicatingWdevice = 19;
            }
            internal static class Numerics
            {
                public const uint NumberOfRecordersAvailable = 1;
            }
            internal static class Strings
            {
                public const uint ChannelPreviewURLFB = 1;
                public const uint UpcomingEventNameFB = 2;
                public const uint UpcomingEventCountdownFB = 3;
                public const uint CurrentEventNameFB = 4;
                public const uint UsedSpaceSerial = 5;
                public const uint TotalSpaceSerial = 6;
                public const uint FreeSpaceSerial = 7;
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
 
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.StartRecording, onStartRecording);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.StopRecording, onStopRecording);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.StartRecordingOnChannel1, onStartRecordingOnChannel1);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.StartRecordingOnChannel2, onStartRecordingOnChannel2);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.StartRecordingOnChannel3, onStartRecordingOnChannel3);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.StartRecordingOnChannel4, onStartRecordingOnChannel4);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.StopRecordingOnChannel1, onStopRecordingOnChannel1);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.StopRecordingOnChannel2, onStopRecordingOnChannel2);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.StopRecordingOnChannel3, onStopRecordingOnChannel3);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.StopRecordingOnChannel4, onStopRecordingOnChannel4);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.CurrentEventPause, onCurrentEventPause);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.CurrentEventResume, onCurrentEventResume);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.CurrentEventStop, onCurrentEventStop);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.CurrentEventExtend, onCurrentEventExtend);
            ComponentMediator.ConfigureBooleanEvent(controlJoinId, Joins.Booleans.StartURLVideoFeed, onStartURLVideoFeed);

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

        public event EventHandler<UIEventArgs> StartRecording;
        private void onStartRecording(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = StartRecording;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> StopRecording;
        private void onStopRecording(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = StopRecording;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> StartRecordingOnChannel1;
        private void onStartRecordingOnChannel1(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = StartRecordingOnChannel1;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> StartRecordingOnChannel2;
        private void onStartRecordingOnChannel2(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = StartRecordingOnChannel2;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> StartRecordingOnChannel3;
        private void onStartRecordingOnChannel3(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = StartRecordingOnChannel3;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> StartRecordingOnChannel4;
        private void onStartRecordingOnChannel4(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = StartRecordingOnChannel4;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> StopRecordingOnChannel1;
        private void onStopRecordingOnChannel1(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = StopRecordingOnChannel1;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> StopRecordingOnChannel2;
        private void onStopRecordingOnChannel2(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = StopRecordingOnChannel2;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> StopRecordingOnChannel3;
        private void onStopRecordingOnChannel3(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = StopRecordingOnChannel3;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> StopRecordingOnChannel4;
        private void onStopRecordingOnChannel4(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = StopRecordingOnChannel4;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> CurrentEventPause;
        private void onCurrentEventPause(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = CurrentEventPause;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> CurrentEventResume;
        private void onCurrentEventResume(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = CurrentEventResume;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> CurrentEventStop;
        private void onCurrentEventStop(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = CurrentEventStop;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> CurrentEventExtend;
        private void onCurrentEventExtend(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = CurrentEventExtend;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }

        public event EventHandler<UIEventArgs> StartURLVideoFeed;
        private void onStartURLVideoFeed(SmartObjectEventArgs eventArgs)
        {
            EventHandler<UIEventArgs> handler = StartURLVideoFeed;
            if (handler != null)
                handler(this, UIEventArgs.CreateEventArgs(eventArgs));
        }


        public void IsCommunicatingWdevice(ControlBoolInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].BooleanInput[Joins.Booleans.IsCommunicatingWdevice], this);
            }
        }

        public void NumberOfRecordersAvailable(ControlUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.NumberOfRecordersAvailable], this);
            }
        }

        public void ChannelPreviewURLFB(ControlStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.ChannelPreviewURLFB], this);
            }
        }

        public void UpcomingEventNameFB(ControlStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.UpcomingEventNameFB], this);
            }
        }

        public void UpcomingEventCountdownFB(ControlStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.UpcomingEventCountdownFB], this);
            }
        }

        public void CurrentEventNameFB(ControlStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.CurrentEventNameFB], this);
            }
        }

        public void UsedSpaceSerial(ControlStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.UsedSpaceSerial], this);
            }
        }

        public void TotalSpaceSerial(ControlStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.TotalSpaceSerial], this);
            }
        }

        public void FreeSpaceSerial(ControlStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.FreeSpaceSerial], this);
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

            StartRecording = null;
            StopRecording = null;
            StartRecordingOnChannel1 = null;
            StartRecordingOnChannel2 = null;
            StartRecordingOnChannel3 = null;
            StartRecordingOnChannel4 = null;
            StopRecordingOnChannel1 = null;
            StopRecordingOnChannel2 = null;
            StopRecordingOnChannel3 = null;
            StopRecordingOnChannel4 = null;
            CurrentEventPause = null;
            CurrentEventResume = null;
            CurrentEventStop = null;
            CurrentEventExtend = null;
            StartURLVideoFeed = null;
        }

        #endregion

    }
}
