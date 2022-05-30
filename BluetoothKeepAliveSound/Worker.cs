using System;
using System.Threading;
using System.Threading.Tasks;
using BluetoothKeepAliveSound.BluetoothState;
using BluetoothKeepAliveSound.SoundPlayer;
using Microsoft.Extensions.Hosting;

namespace BluetoothKeepAliveSound
{
    public class Worker : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            KeepAliveGenerator generator = new();
            while (!stoppingToken.IsCancellationRequested)
            {
                var bluetoothState = await BluetoothService.GetBluetoothState();
                switch (bluetoothState)
                {
                    case EBluetoothState.None:
                        if (generator.Playing)
                            generator.StopSound();
                        await StopAsync(stoppingToken);
                        break;
                    case EBluetoothState.EnabledConnected:
                        if (!generator.Playing)
                            generator.PlaySound();
                        break;
                    case EBluetoothState.EnabledDisconnected:
                        if (generator.Playing)
                            generator.StopSound();
                        break;
                    case EBluetoothState.TurnedOff:
                        if (generator.Playing)
                            generator.StopSound();
                        await Task.Delay(30000, stoppingToken);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                await Task.Delay(15000, stoppingToken);
            }
        }
    }
}