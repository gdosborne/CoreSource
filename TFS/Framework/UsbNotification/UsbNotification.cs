using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading.Tasks;

namespace GregOsborne.Usb {
    [Flags]
    public enum EventTypeFlags {
        None = 0,
        AnyUsbAdded = 1,
        AnyUsbRemoved = 2,
        DriveAdded = 4,
        DriveRemoved = 8
    }

    public class DriveChangeEventArgs : EventArgs {
        public DriveChangeEventArgs(DriveInfo dInfo, bool removed, Dictionary<string, object> properties) {
            DriveInformation = dInfo;
            Removed = removed;
            Properties = properties;
        }

        public DriveInfo DriveInformation { get; }
        public bool Removed { get; }
        public Dictionary<string, object> Properties { get; }
    }

    public delegate void DriveChangeHandler(object sender, DriveChangeEventArgs e);

    public class UsbDeviceStatusChangedEventArgs : EventArgs {
        public UsbDeviceStatusChangedEventArgs(bool removed, Dictionary<string, object> properties) {
            Removed = removed;
            Properties = properties;
        }

        public bool Removed { get; }
        public Dictionary<string, object> Properties { get; }
    }

    public delegate void UsbDeviceStatusChangeHandler(object sender, UsbDeviceStatusChangedEventArgs e);

    public class DriveMonitor : IDisposable {
        private ManagementEventWatcher _mweAnyUsb;
        private ManagementEventWatcher _mweCreation;
        private ManagementEventWatcher _mweDeletion;
        private bool _isDisposing;

        public DriveMonitor(EventTypeFlags eventsToMonitor) {
            CurrentDrives = new List<DriveInfo>(DriveInfo.GetDrives());
            EventsToMonitor = eventsToMonitor;

            Task.Factory.StartNew(WaitForDriveInsertion);
            Task.Factory.StartNew(WaitForDriveRemoval);
            Task.Factory.StartNew(WaitForUsb);
        }

        public EventTypeFlags EventsToMonitor { get; set; }
        public List<DriveInfo> CurrentDrives { get; private set; }

        public void Dispose() {
            if (_isDisposing)
                return;
            _isDisposing = true;
            _mweCreation.Dispose();
            _mweDeletion.Dispose();
            CurrentDrives = null;
            _isDisposing = false;
        }

        public event DriveChangeHandler DriveChange;
        public event UsbDeviceStatusChangeHandler UsbDeviceStatusChange;

        private void WaitForDriveRemoval() {
            var qDeletion = new WqlEventQuery {
                EventClassName = "__InstanceDeletionEvent",
                WithinInterval = new TimeSpan(0, 0, 0, 0, 500),
                Condition = @"TargetInstance ISA 'Win32_DiskDriveToDiskPartition'  "
            };
            _mweDeletion = new ManagementEventWatcher(qDeletion);
            _mweDeletion.EventArrived += DriveRemovedEvent;
            _mweDeletion.Start(); // Start listen for events
        }

        private void WaitForDriveInsertion() {
            var qCreation = new WqlEventQuery {
                EventClassName = "__InstanceCreationEvent",
                WithinInterval = new TimeSpan(0, 0, 0, 0, 500),
                Condition = @"TargetInstance ISA 'Win32_DiskDriveToDiskPartition'"
            };
            _mweCreation = new ManagementEventWatcher(qCreation);
            _mweCreation.EventArrived += DriveAddedEvent;
            _mweCreation.Start(); // Start listen for events			
        }

        private void WaitForUsb() {
            var qUsb = new WqlEventQuery {
                EventClassName = "__InstanceOperationEvent",
                WithinInterval = new TimeSpan(0, 0, 0, 0, 500),
                Condition = @"TargetInstance ISA 'Win32_USBControllerdevice'"
            };
            _mweAnyUsb = new ManagementEventWatcher(qUsb);
            _mweAnyUsb.EventArrived += AnyUsbEvent;
            _mweAnyUsb.Start(); // Start listen for events
        }

        private static Dictionary<string, object> GetEventProperties(ManagementBaseObject e, string prefix) {
            var results = new Dictionary<string, object>();
            var etr = e.Properties.GetEnumerator();
            while (etr.MoveNext()) {
                var name = prefix + (string.IsNullOrEmpty(prefix) ? string.Empty : ".") + etr.Current.Name;
                if (etr.Current.Value is ManagementBaseObject)
                    results.Add(name, GetEventProperties(etr.Current.Value as ManagementBaseObject, name));
                else
                    results.Add(name, etr.Current.Value);
            }
            return results;
        }

        private void AnyUsbEvent(object sender, EventArrivedEventArgs e) {
            var props = GetEventProperties(e.NewEvent, string.Empty);
            if (!e.NewEvent.Properties["TargetInstance"].Origin.Equals("__InstanceOperationEvent")) return;
            if (EventsToMonitor.HasFlag(EventTypeFlags.AnyUsbAdded) && e.NewEvent.ClassPath.Path.EndsWith("__InstanceCreationEvent", StringComparison.OrdinalIgnoreCase))
                UsbDeviceStatusChange?.Invoke(this, new UsbDeviceStatusChangedEventArgs(false, props));
            else if (EventsToMonitor.HasFlag(EventTypeFlags.AnyUsbRemoved) && !e.NewEvent.ClassPath.Path.EndsWith("__InstanceCreationEvent", StringComparison.OrdinalIgnoreCase))
                UsbDeviceStatusChange?.Invoke(this, new UsbDeviceStatusChangedEventArgs(true, props));
        }

        internal void DriveAddedEvent(object sender, EventArrivedEventArgs e) {
            var props = GetEventProperties(e.NewEvent, string.Empty);
            var tempDrives = new List<DriveInfo>(DriveInfo.GetDrives());
            if (EventsToMonitor.HasFlag(EventTypeFlags.DriveAdded))
                tempDrives.ForEach(x => {
                    if (!CurrentDrives.Any(y => y.Name.Equals(x.Name, StringComparison.OrdinalIgnoreCase))) DriveChange?.Invoke(this, new DriveChangeEventArgs(tempDrives.First(y => y.Name.Equals(x.Name, StringComparison.OrdinalIgnoreCase)), false, props));
                });
            CurrentDrives = tempDrives;
        }

        internal void DriveRemovedEvent(object sender, EventArrivedEventArgs e) {
            var props = GetEventProperties(e.NewEvent, string.Empty);
            var tempDrives = new List<DriveInfo>(DriveInfo.GetDrives());
            if (EventsToMonitor.HasFlag(EventTypeFlags.DriveRemoved))
                CurrentDrives.ForEach(x => {
                    if (tempDrives.Any(y => y.Name.Equals(x.Name, StringComparison.OrdinalIgnoreCase))) return;
                    DriveChange?.Invoke(this, new DriveChangeEventArgs(CurrentDrives.First(y => y.Name.Equals(x.Name, StringComparison.OrdinalIgnoreCase)), true, props));
                });
            CurrentDrives = tempDrives;
        }
    }
}