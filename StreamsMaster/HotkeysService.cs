using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;

namespace StreamsMaster
{
    class HotkeysService : IDisposable
    {
        private VolumeService _volumeService = new VolumeService();
        internal InputSimulator Device { get; set; } = new InputSimulator();

        public void RegisterHotkeys()
        {
            var normalReturn = Combination.TriggeredBy(Keys.Return).With(Keys.Decimal);
            var ctrlnormalReturn = Combination.TriggeredBy(Keys.Return).With(Keys.Decimal).With(Keys.Control);
            var normalAdd = Combination.TriggeredBy(Keys.Add);
            var ctrlAdd = Combination.TriggeredBy(Keys.Add).With(Keys.Control);
            var normalSubtract = Combination.TriggeredBy(Keys.Subtract);
            var ctrlSubtract = Combination.TriggeredBy(Keys.Subtract).With(Keys.Control);

            var assignment = new Dictionary<Combination, Action>
            {
                {normalReturn, MuteUnmuteSystemVolume},
                {ctrlnormalReturn, MuteUnmuteAppVolume},
                {normalAdd, RaiseSystemVolume},
                {ctrlAdd, RaiseAppVolume},
                {normalSubtract, LowerSystemVolume},
                {ctrlSubtract, LowerAppVolume}
            };

            Hook.GlobalEvents().OnCombination(assignment);
        }

        private void SendPlayPauseKey(object sender, HandledEventArgs e)
        {
            Device.Keyboard.KeyPress(VirtualKeyCode.MEDIA_PLAY_PAUSE);
        }
        private void SendStopKey(object sender, HandledEventArgs e)
        {
            Device.Keyboard.KeyPress(VirtualKeyCode.MEDIA_STOP);
        }
        private void SendNextTrackKey(object sender, HandledEventArgs e)
        {
            Device.Keyboard.KeyPress(VirtualKeyCode.MEDIA_NEXT_TRACK);
        }
        private void SendPreviousTrackKey(object sender, HandledEventArgs e)
        {
            Device.Keyboard.KeyPress(VirtualKeyCode.MEDIA_PREV_TRACK);
        }
        private void MuteUnmuteSystemVolume()
        {
            _volumeService.MuteUnmuteSystemVolume();
        }

        private void RaiseSystemVolume()
        {
            _volumeService.RaiseSystemVolume();

        }

        private void LowerSystemVolume()
        {
            _volumeService.LowerSystemVolume();

        }

        private void MuteUnmuteAppVolume()
        {
            _volumeService.MuteUnmuteAppVolume();
        }

        private void RaiseAppVolume()
        {
            _volumeService.RaiseAppVolume();
        }

        private void LowerAppVolume()
        {
            _volumeService.LowerAppVolume();
        }

        public void Dispose()
        {
        }
    }
}