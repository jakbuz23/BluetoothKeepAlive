using System;
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

            var connectedCollection = await DeviceInformation.FindAllAsync(
                BluetoothDevice.GetDeviceSelectorFromConnectionStatus(BluetoothConnectionStatus.Connected));
            if (connectedCollection.Count is 0)
                return EBluetoothState.EnabledDisconnected;

            var device = await BluetoothDevice.FromIdAsync(connectedCollection[0].Id);

            return device.ClassOfDevice.MajorClass == BluetoothMajorClass.AudioVideo
                ? EBluetoothState.EnabledConnected
                : EBluetoothState.EnabledDisconnected;
        }
    }
}