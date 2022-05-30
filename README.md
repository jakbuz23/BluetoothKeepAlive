# BluetoothKeepAlive
Simple .NET 6 worker service for preventing your Bluetooth audio from going to sleep.

It uses the NAudio library to play ultra quiet 1 Hz sine wave whenever there is a Bluetooth audio device connected thus preventing it from going to sleep mode.
