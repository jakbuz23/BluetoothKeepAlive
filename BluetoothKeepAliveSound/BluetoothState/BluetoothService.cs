using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Enumeration;
using Windows.Devices.Radios;

namespace BluetoothKeepAliveSound.BluetoothState
{
    public abstract class BluetoothService
    {
        public static async Task<EBluetoothState> GetBluetoothState()
        {
            var adapter = await BluetoothAdapter.GetDefaultAsync();
            if (adapter == null)
                return EBluetoothState.None;

            var radio = await adapter.GetRadioAsync();
            if (radio.State is RadioState.Off)
                return EBluetoothState.TurnedOff;

            var conectedDevicesCollection = await DeviceInformation.FindAllAsync(
                BluetoothDevice.GetDeviceSelectorFromConnectionStatus(BluetoothConnectionStatus.Connected));

            if (conectedDevicesCollection.Count is 0)
                return EBluetoothState.EnabledDisconnected;

            var btDevices = new List<BluetoothDevice>();

            foreach (var device in conectedDevicesCollection)
                btDevices.Add(await BluetoothDevice.FromIdAsync(device.Id));

            return btDevices.Exists(device => device.ClassOfDevice.MajorClass == BluetoothMajorClass.AudioVideo)
                ? EBluetoothState.EnabledConnected
                : EBluetoothState.EnabledDisconnected;
        }
    }
}