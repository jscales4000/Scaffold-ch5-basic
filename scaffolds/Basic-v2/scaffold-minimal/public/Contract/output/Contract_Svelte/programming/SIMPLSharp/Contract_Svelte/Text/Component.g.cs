using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace Contract_Svelte.Text
{
    public interface IComponent
    {
        object UserObject { get; set; }

        void DeviceConnectionOptionIcon1(ComponentUShortInputSigDelegate callback);
        void DeviceConnectionOptionIcon2(ComponentUShortInputSigDelegate callback);
        void DeviceConnectionOptionIcon3(ComponentUShortInputSigDelegate callback);
        void DeviceConnectionOptionIcon4(ComponentUShortInputSigDelegate callback);
        void Line1Text(ComponentStringInputSigDelegate callback);
        void Line2Text(ComponentStringInputSigDelegate callback);
        void Line3Text(ComponentStringInputSigDelegate callback);
        void Line4Text(ComponentStringInputSigDelegate callback);
        void HeaderText(ComponentStringInputSigDelegate callback);

    }

    public delegate void ComponentUShortInputSigDelegate(UShortInputSig uShortInputSig, IComponent component);
    public delegate void ComponentStringInputSigDelegate(StringInputSig stringInputSig, IComponent component);

    internal class Component : IComponent, IDisposable
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
            internal static class Numerics
            {
                public const uint DeviceConnectionOptionIcon1 = 1;
                public const uint DeviceConnectionOptionIcon2 = 2;
                public const uint DeviceConnectionOptionIcon3 = 3;
                public const uint DeviceConnectionOptionIcon4 = 4;
            }
            internal static class Strings
            {
                public const uint Line1Text = 1;
                public const uint Line2Text = 2;
                public const uint Line3Text = 3;
                public const uint Line4Text = 4;
                public const uint HeaderText = 5;
            }
        }

        #endregion

        #region Construction and Initialization

        internal Component(ComponentMediator componentMediator, uint controlJoinId)
        {
            ComponentMediator = componentMediator;
            Initialize(controlJoinId);
        }

        private void Initialize(uint controlJoinId)
        {
            ControlJoinId = controlJoinId; 
 
            _devices = new List<BasicTriListWithSmartObject>(); 
 
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

        public void DeviceConnectionOptionIcon1(ComponentUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.DeviceConnectionOptionIcon1], this);
            }
        }

        public void DeviceConnectionOptionIcon2(ComponentUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.DeviceConnectionOptionIcon2], this);
            }
        }

        public void DeviceConnectionOptionIcon3(ComponentUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.DeviceConnectionOptionIcon3], this);
            }
        }

        public void DeviceConnectionOptionIcon4(ComponentUShortInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].UShortInput[Joins.Numerics.DeviceConnectionOptionIcon4], this);
            }
        }

        public void Line1Text(ComponentStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Line1Text], this);
            }
        }

        public void Line2Text(ComponentStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Line2Text], this);
            }
        }

        public void Line3Text(ComponentStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Line3Text], this);
            }
        }

        public void Line4Text(ComponentStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.Line4Text], this);
            }
        }

        public void HeaderText(ComponentStringInputSigDelegate callback)
        {
            for (int index = 0; index < Devices.Count; index++)
            {
                callback(Devices[index].SmartObjects[ControlJoinId].StringInput[Joins.Strings.HeaderText], this);
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
            return string.Format("Contract: {0} Component: {1} HashCode: {2} {3}", "Component", GetType().Name, GetHashCode(), UserObject != null ? "UserObject: " + UserObject : null);
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

        }

        #endregion

    }
}
