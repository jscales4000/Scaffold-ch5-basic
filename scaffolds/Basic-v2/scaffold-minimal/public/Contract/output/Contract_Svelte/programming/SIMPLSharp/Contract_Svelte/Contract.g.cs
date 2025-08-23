using System;
using System.Collections.Generic;
using System.Linq;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharpPro;

namespace Contract_Svelte
{
    /// <summary>
    /// Common Interface for Root Contracts.
    /// </summary>
    public interface IContract
    {
        object UserObject { get; set; }
        void AddDevice(BasicTriListWithSmartObject device);
        void RemoveDevice(BasicTriListWithSmartObject device);
    }

    /// <summary>
    /// Crestron TS-60/70/80
    /// </summary>
    public class Contract : IContract, IDisposable
    {
        #region Components

        private ComponentMediator ComponentMediator { get; set; }

        public Contract_Svelte.IDiagnostics Diagnostics { get { return (Contract_Svelte.IDiagnostics)InternalDiagnostics; } }
        private Contract_Svelte.Diagnostics InternalDiagnostics { get; set; }

        public Contract_Svelte.IDeviceControl FullDeviceControl { get { return (Contract_Svelte.IDeviceControl)InternalFullDeviceControl; } }
        private Contract_Svelte.DeviceControl InternalFullDeviceControl { get; set; }

        public Contract_Svelte.IMain MPC3201B { get { return (Contract_Svelte.IMain)InternalMPC3201B; } }
        private Contract_Svelte.Main InternalMPC3201B { get; set; }

        public Contract_Svelte.ISettings Settings { get { return (Contract_Svelte.ISettings)InternalSettings; } }
        private Contract_Svelte.Settings InternalSettings { get; set; }

        public Contract_Svelte.Text.IComponent[] Text { get { return InternalText.Cast<Contract_Svelte.Text.IComponent>().ToArray(); } }
        private Contract_Svelte.Text.Component[] InternalText { get; set; }

        public Contract_Svelte.MediaPlayer.IControl[] MediaPlayer { get { return InternalMediaPlayer.Cast<Contract_Svelte.MediaPlayer.IControl>().ToArray(); } }
        private Contract_Svelte.MediaPlayer.Control[] InternalMediaPlayer { get; set; }

        public Contract_Svelte.DocCam.IControl[] DocCam { get { return InternalDocCam.Cast<Contract_Svelte.DocCam.IControl>().ToArray(); } }
        private Contract_Svelte.DocCam.Control[] InternalDocCam { get; set; }

        public Contract_Svelte.AirMedia.IControls[] Airmedia { get { return InternalAirmedia.Cast<Contract_Svelte.AirMedia.IControls>().ToArray(); } }
        private Contract_Svelte.AirMedia.Controls[] InternalAirmedia { get; set; }

        public Contract_Svelte.LectureCapture.IControl[] LectureCapture { get { return InternalLectureCapture.Cast<Contract_Svelte.LectureCapture.IControl>().ToArray(); } }
        private Contract_Svelte.LectureCapture.Control[] InternalLectureCapture { get; set; }

        public Contract_Svelte.Camera.IControl[] Camera { get { return InternalCamera.Cast<Contract_Svelte.Camera.IControl>().ToArray(); } }
        private Contract_Svelte.Camera.Control[] InternalCamera { get; set; }

        public Contract_Svelte.Text.IComponent[] Laptop { get { return InternalLaptop.Cast<Contract_Svelte.Text.IComponent>().ToArray(); } }
        private Contract_Svelte.Text.Component[] InternalLaptop { get; set; }

        public Contract_Svelte.Text.IComponent[] RoomPC { get { return InternalRoomPC.Cast<Contract_Svelte.Text.IComponent>().ToArray(); } }
        private Contract_Svelte.Text.Component[] InternalRoomPC { get; set; }

        #endregion

        #region Construction and Initialization

        private static readonly IDictionary<int, uint> TextSmartObjectIdMappings = new Dictionary<int, uint>{
            { 0, 5 }, { 1, 6 }, { 2, 7 }, { 3, 8 }, { 4, 9 }, { 5, 10 }, { 6, 11 }, { 7, 12 }};
        private static readonly IDictionary<int, uint> MediaPlayerSmartObjectIdMappings = new Dictionary<int, uint>{
            { 0, 13 }, { 1, 14 }, { 2, 15 }, { 3, 16 }, { 4, 17 }, { 5, 18 }, { 6, 19 }, { 7, 20 }};
        private static readonly IDictionary<int, uint> DocCamSmartObjectIdMappings = new Dictionary<int, uint>{
            { 0, 21 }, { 1, 22 }, { 2, 23 }, { 3, 24 }, { 4, 25 }, { 5, 26 }, { 6, 27 }, { 7, 28 }};
        private static readonly IDictionary<int, uint> AirmediaSmartObjectIdMappings = new Dictionary<int, uint>{
            { 0, 29 }, { 1, 30 }, { 2, 31 }, { 3, 32 }, { 4, 33 }, { 5, 34 }, { 6, 35 }, { 7, 36 }};
        private static readonly IDictionary<int, uint> LectureCaptureSmartObjectIdMappings = new Dictionary<int, uint>{
            { 0, 37 }, { 1, 38 }, { 2, 39 }, { 3, 40 }, { 4, 41 }, { 5, 42 }, { 6, 43 }, { 7, 44 }};
        private static readonly IDictionary<int, uint> CameraSmartObjectIdMappings = new Dictionary<int, uint>{
            { 0, 45 }, { 1, 46 }, { 2, 47 }, { 3, 48 }, { 4, 49 }, { 5, 50 }, { 6, 51 }, { 7, 52 }};
        private static readonly IDictionary<int, uint> LaptopSmartObjectIdMappings = new Dictionary<int, uint>{
            { 0, 53 }, { 1, 54 }, { 2, 55 }, { 3, 56 }, { 4, 57 }, { 5, 58 }, { 6, 59 }, { 7, 60 }};
        private static readonly IDictionary<int, uint> RoomPCSmartObjectIdMappings = new Dictionary<int, uint>{
            { 0, 61 }, { 1, 62 }, { 2, 63 }, { 3, 64 }, { 4, 65 }, { 5, 66 }, { 6, 67 }, { 7, 68 }};

        public Contract()
            : this(new List<BasicTriListWithSmartObject>().ToArray())
        {
        }

        public Contract(BasicTriListWithSmartObject device)
            : this(new [] { device })
        {
        }

        public Contract(BasicTriListWithSmartObject[] devices)
        {
            if (devices == null)
                throw new ArgumentNullException("Devices is null");

            ComponentMediator = new ComponentMediator();

            InternalDiagnostics = new Contract_Svelte.Diagnostics(ComponentMediator, 1);
            InternalFullDeviceControl = new Contract_Svelte.DeviceControl(ComponentMediator, 2);
            InternalMPC3201B = new Contract_Svelte.Main(ComponentMediator, 3);
            InternalSettings = new Contract_Svelte.Settings(ComponentMediator, 4);
            InternalText = new Contract_Svelte.Text.Component[TextSmartObjectIdMappings.Count];
            for (int index = 0; index < TextSmartObjectIdMappings.Count; index++)
            {
                InternalText[index] = new Contract_Svelte.Text.Component(ComponentMediator, TextSmartObjectIdMappings[index]);
            }
            InternalMediaPlayer = new Contract_Svelte.MediaPlayer.Control[MediaPlayerSmartObjectIdMappings.Count];
            for (int index = 0; index < MediaPlayerSmartObjectIdMappings.Count; index++)
            {
                InternalMediaPlayer[index] = new Contract_Svelte.MediaPlayer.Control(ComponentMediator, MediaPlayerSmartObjectIdMappings[index]);
            }
            InternalDocCam = new Contract_Svelte.DocCam.Control[DocCamSmartObjectIdMappings.Count];
            for (int index = 0; index < DocCamSmartObjectIdMappings.Count; index++)
            {
                InternalDocCam[index] = new Contract_Svelte.DocCam.Control(ComponentMediator, DocCamSmartObjectIdMappings[index]);
            }
            InternalAirmedia = new Contract_Svelte.AirMedia.Controls[AirmediaSmartObjectIdMappings.Count];
            for (int index = 0; index < AirmediaSmartObjectIdMappings.Count; index++)
            {
                InternalAirmedia[index] = new Contract_Svelte.AirMedia.Controls(ComponentMediator, AirmediaSmartObjectIdMappings[index]);
            }
            InternalLectureCapture = new Contract_Svelte.LectureCapture.Control[LectureCaptureSmartObjectIdMappings.Count];
            for (int index = 0; index < LectureCaptureSmartObjectIdMappings.Count; index++)
            {
                InternalLectureCapture[index] = new Contract_Svelte.LectureCapture.Control(ComponentMediator, LectureCaptureSmartObjectIdMappings[index]);
            }
            InternalCamera = new Contract_Svelte.Camera.Control[CameraSmartObjectIdMappings.Count];
            for (int index = 0; index < CameraSmartObjectIdMappings.Count; index++)
            {
                InternalCamera[index] = new Contract_Svelte.Camera.Control(ComponentMediator, CameraSmartObjectIdMappings[index]);
            }
            InternalLaptop = new Contract_Svelte.Text.Component[LaptopSmartObjectIdMappings.Count];
            for (int index = 0; index < LaptopSmartObjectIdMappings.Count; index++)
            {
                InternalLaptop[index] = new Contract_Svelte.Text.Component(ComponentMediator, LaptopSmartObjectIdMappings[index]);
            }
            InternalRoomPC = new Contract_Svelte.Text.Component[RoomPCSmartObjectIdMappings.Count];
            for (int index = 0; index < RoomPCSmartObjectIdMappings.Count; index++)
            {
                InternalRoomPC[index] = new Contract_Svelte.Text.Component(ComponentMediator, RoomPCSmartObjectIdMappings[index]);
            }

            for (int index = 0; index < devices.Length; index++)
            {
                AddDevice(devices[index]);
            }
        }

        public static void ClearDictionaries()
        {
            TextSmartObjectIdMappings.Clear();
            MediaPlayerSmartObjectIdMappings.Clear();
            DocCamSmartObjectIdMappings.Clear();
            AirmediaSmartObjectIdMappings.Clear();
            LectureCaptureSmartObjectIdMappings.Clear();
            CameraSmartObjectIdMappings.Clear();
            LaptopSmartObjectIdMappings.Clear();
            RoomPCSmartObjectIdMappings.Clear();

        }

        #endregion

        #region Standard Contract Members

        public object UserObject { get; set; }

        public void AddDevice(BasicTriListWithSmartObject device)
        {
            InternalDiagnostics.AddDevice(device);
            InternalFullDeviceControl.AddDevice(device);
            InternalMPC3201B.AddDevice(device);
            InternalSettings.AddDevice(device);
            for (int index = 0; index < 8; index++)
            {
                InternalText[index].AddDevice(device);
            }
            for (int index = 0; index < 8; index++)
            {
                InternalMediaPlayer[index].AddDevice(device);
            }
            for (int index = 0; index < 8; index++)
            {
                InternalDocCam[index].AddDevice(device);
            }
            for (int index = 0; index < 8; index++)
            {
                InternalAirmedia[index].AddDevice(device);
            }
            for (int index = 0; index < 8; index++)
            {
                InternalLectureCapture[index].AddDevice(device);
            }
            for (int index = 0; index < 8; index++)
            {
                InternalCamera[index].AddDevice(device);
            }
            for (int index = 0; index < 8; index++)
            {
                InternalLaptop[index].AddDevice(device);
            }
            for (int index = 0; index < 8; index++)
            {
                InternalRoomPC[index].AddDevice(device);
            }
        }

        public void RemoveDevice(BasicTriListWithSmartObject device)
        {
            InternalDiagnostics.RemoveDevice(device);
            InternalFullDeviceControl.RemoveDevice(device);
            InternalMPC3201B.RemoveDevice(device);
            InternalSettings.RemoveDevice(device);
            for (int index = 0; index < 8; index++)
            {
                InternalText[index].RemoveDevice(device);
            }
            for (int index = 0; index < 8; index++)
            {
                InternalMediaPlayer[index].RemoveDevice(device);
            }
            for (int index = 0; index < 8; index++)
            {
                InternalDocCam[index].RemoveDevice(device);
            }
            for (int index = 0; index < 8; index++)
            {
                InternalAirmedia[index].RemoveDevice(device);
            }
            for (int index = 0; index < 8; index++)
            {
                InternalLectureCapture[index].RemoveDevice(device);
            }
            for (int index = 0; index < 8; index++)
            {
                InternalCamera[index].RemoveDevice(device);
            }
            for (int index = 0; index < 8; index++)
            {
                InternalLaptop[index].RemoveDevice(device);
            }
            for (int index = 0; index < 8; index++)
            {
                InternalRoomPC[index].RemoveDevice(device);
            }
        }

        #endregion

        #region IDisposable

        public bool IsDisposed { get; set; }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            InternalDiagnostics.Dispose();
            InternalFullDeviceControl.Dispose();
            InternalMPC3201B.Dispose();
            InternalSettings.Dispose();
            for (int index = 0; index < 8; index++)
            {
                InternalText[index].Dispose();
            }
            for (int index = 0; index < 8; index++)
            {
                InternalMediaPlayer[index].Dispose();
            }
            for (int index = 0; index < 8; index++)
            {
                InternalDocCam[index].Dispose();
            }
            for (int index = 0; index < 8; index++)
            {
                InternalAirmedia[index].Dispose();
            }
            for (int index = 0; index < 8; index++)
            {
                InternalLectureCapture[index].Dispose();
            }
            for (int index = 0; index < 8; index++)
            {
                InternalCamera[index].Dispose();
            }
            for (int index = 0; index < 8; index++)
            {
                InternalLaptop[index].Dispose();
            }
            for (int index = 0; index < 8; index++)
            {
                InternalRoomPC[index].Dispose();
            }
            ComponentMediator.Dispose(); 
        }

        #endregion

    }
}
