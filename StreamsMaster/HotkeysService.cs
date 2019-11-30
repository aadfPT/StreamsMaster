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
        private readonly VolumeService _volumeService = new VolumeService();

        internal InputSimulator Device { get; set; } = new InputSimulator();

        public void RegisterHotkeys()
        {
            Hook.GlobalEvents().KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.PageUp 
                    || e.KeyCode == Keys.PageDown
                    || e.KeyCode == Keys.End)
                {
                    e.SuppressKeyPress = true;
                }
            };
            Hook.GlobalEvents().KeyUp += (sender, e) =>
            {
                if (e.KeyCode != Keys.PageUp 
                    && e.KeyCode != Keys.PageDown
                    &&e.KeyCode != Keys.End)
                {
                    return;
                }

                e.SuppressKeyPress = true;
                switch (e.KeyCode)
                {
                    case Keys.End when e.Control:
                        MuteUnmuteAppVolume();
                        return;
                    case Keys.End:
                        MuteUnmuteSystemVolume();
                        return;
                    case Keys.PageUp when e.Control:
                        RaiseAppVolume();
                        return;
                    case Keys.PageUp:
                        RaiseSystemVolume();
                        return;
                    case Keys.PageDown when e.Control:
                        LowerAppVolume();
                        return;
                    case Keys.PageDown:
                        LowerSystemVolume();
                        return;
                }
            };
        }

        private void ExecuteDependingOnControlKey(bool controlIsActive, Action actionWithControl, Action actionWithoutControl)
        {
            if (controlIsActive)
            {
                actionWithControl();
            }
            else
            {
                actionWithoutControl();
            }
        }

        private void SendPlayPauseKey()
        {
            Device.Keyboard.KeyPress(VirtualKeyCode.MEDIA_PLAY_PAUSE);
        }
        private void SendStopKey()
        {
            Device.Keyboard.KeyPress(VirtualKeyCode.MEDIA_STOP);
        }
        private void SendNextTrackKey()
        {
            Device.Keyboard.KeyPress(VirtualKeyCode.MEDIA_NEXT_TRACK);
        }
        private void SendPreviousTrackKey()
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