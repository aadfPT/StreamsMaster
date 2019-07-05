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
        private bool decimalIsDown;
        private bool returnUsed;

        internal InputSimulator Device { get; set; } = new InputSimulator();

        public void RegisterHotkeys()
        {
            Hook.GlobalEvents().KeyDown += (sender, e) =>
            {
                switch (e.KeyCode)
                {
                    case Keys.Return:
                        {


                            if (!decimalIsDown) return;
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            returnUsed = true;
                            if (e.Control)
                            {
                                MuteUnmuteAppVolume();
                            }
                            else
                            {
                                MuteUnmuteSystemVolume();
                            }
                            return;
                        }
                    case Keys.Decimal:
                    case Keys.Delete:
                        {
                            decimalIsDown = true;
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            return;
                        }
                    case Keys.Subtract:
                        {
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            if (e.Control)
                            {
                                LowerAppVolume();
                            }
                            else
                            {
                                LowerSystemVolume();
                            }
                            return;
                        }
                    case Keys.Add:
                        {
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            if (e.Control)
                            {
                                RaiseAppVolume();
                            }
                            else
                            {
                                RaiseSystemVolume();
                            }
                            return;
                        }
                    case Keys.Right:
                        {
                            if (!e.Control) return;
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            SendNextTrackKey();
                            return;
                        }
                    case Keys.Left:
                        {
                            if (!e.Control) return;
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            SendPreviousTrackKey();
                            return;
                        }
                    case Keys.Down:
                        {
                            if (!e.Control) return;
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                            SendPlayPauseKey();
                            return;
                        }
                };
            };

            Hook.GlobalEvents().KeyUp += (sender, e) =>
            {
                switch (e.KeyCode)
                {
                    case Keys.Decimal:
                    case Keys.Delete:
                        {

                            decimalIsDown = false;
                            if (returnUsed)
                            {
                                returnUsed = false;
                                e.Handled = true;
                                e.SuppressKeyPress = true;
                                return;
                            }
                            Device.Keyboard.KeyPress((VirtualKeyCode)e.KeyCode);
                            return;
                        }
                }
            };
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