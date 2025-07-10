using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureBoard
{
    class AudioEngine : IDisposable
    {
        /// <summary>
        /// Declare an output model to the soundcard,
        /// better one declaration for all the sounds 
        /// when fire-and-forget functionality is 
        /// desired
        /// </summary>
        private readonly IWavePlayer outputDevice;
        private readonly MixingSampleProvider mixer;

        private string soundPath = "C:/Users/am_ka/Desktop/Messy Work Bench/Practice/sounds/arrow-woosh.wav";

        // Constructor
        public AudioEngine(int sampleRate = 44100, int channelCount = 2)
        {
            outputDevice = new WaveOutEvent();
            mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount));

            // Configure mixer to constantly produce buffer of 
            // number of samples requested, even if it's zero
            mixer.ReadFully = true;

            // Configure output device to play constantly in silence,
            // feeding the output model sounds via the mixer
            outputDevice.Init(mixer);
            outputDevice.Play(); // start buffer
        }        
        
    
        public void playSound()
        {
            var input = new AudioFileReader(soundPath);
            addMixerInput(new AutoDisposeFileReader(input));
        }

        private ISampleProvider ConvertToRightChannelCount(ISampleProvider input)
        {
            if (input.WaveFormat.Channels == mixer.WaveFormat.Channels)
            {
                return input;
            }
            if (input.WaveFormat.Channels == 1 && mixer.WaveFormat.Channels == 2)
            {
                return new MonoToStereoSampleProvider(input);
            }
            throw new NotImplementedException("Not yet implemented this channel count conversion");
        }


        public void PlaySound(CachedSound sound)
        {
            addMixerInput(new CachedSoundSampleProvider(sound));
        }


        private void addMixerInput(ISampleProvider input)
        {
            mixer.AddMixerInput(input);
        }

        public void Dispose()
        {
            outputDevice.Dispose();
        }


    }
}
