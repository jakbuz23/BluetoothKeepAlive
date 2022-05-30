# BluetoothKeepAlive
Simple .NET 6 worker service for preventing your Bluetooth audio from going to sleep.

It uses the NAudio library to play ultra quiet 1 Hz sine wave whenever there is a Bluetooth audio device connected thus preventing it from going to sleep mode.

# Requirements
- .NET 6 runtime
- Windows 10
# Instalation & usage
- Place release in your desired location.
- Run <i>install.bat</i> as administrator.
- Program now runs as a service <i>BTKeepAlive</i>.
# Unistall
- Run <i>unintall.bat</i> as administrator.
# Issues
- If you encounter any issues (such as high CPU usage from DeviceAssociationService) stop the service and restart any offending services.
