using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace BluetoothKeepAliveSound.SoundPlayer
{
    public class KeepAliveGenerator
    {
        public KeepAliveGenerator()
        {
            Playing = false;
        }

        public bool Playing { get; set; }
        public WaveOutEvent? OutEvent { get; set; }

        public void PlaySound()
        {
            OutEvent = new WaveOutEvent();
            SignalGenerator generator = new SignalGenerator
            {
                Gain = 0.005,
                Frequency = 1.0,
                Type = SignalGeneratorType.Sin
            };

            OutEvent?.Init(generator.ToWaveProvider());
            OutEvent?.Play();
            Playing = true;
        }

        public void StopSound()
        {
            OutEvent?.Stop();
            OutEvent?.Dispose();
            Playing = false;
        }
    }
}