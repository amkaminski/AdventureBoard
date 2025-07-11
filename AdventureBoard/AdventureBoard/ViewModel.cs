using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureBoard
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private AudioEngine audioEngine;

        public PlaySound_Command PlaySound_Command { get; }

        public ViewModel()
        {
            audioEngine = new AudioEngine();
            PlaySound_Command = new PlaySound_Command(this);
        }

        public void playSound()
        {
            audioEngine.playSound();
        }

        public void cleanUp()
        {
            audioEngine.Dispose();
        }

    }
}
