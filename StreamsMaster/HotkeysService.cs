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
        private bool decimalIsUnused;
        private bool returnUsed;

        internal InputSimulator Device { get; set; } = new InputSimulator();

        public void RegisterHotkeys()
        {
            Hook.GlobalEvents().KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Decimal)
                {
                    decimalIsDown = true;
                    decimalIsUnused = true;
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    return;
                }
                if (!decimalIsDown) return;
                switch (e.KeyCode)
                {
                    case Keys.Return:
                        {
                            if (e.Control)
                            {
                                MuteUnmuteAppVolume();
                            }
                            else
                            {
                                MuteUnmuteSystemVolume();
                            }
                            break;
                        }
                    case Keys.Subtract:
                        {
                            if (e.Control)
                            {
                                LowerAppVolume();
                            }
                            else
                            {
                                LowerSystemVolume();
                            }
                            break;
                        }
                    case Keys.Add:
                        {
                            if (e.Control)
                            {
                                RaiseAppVolume();
                            }
                            else
                            {
                                RaiseSystemVolume();
                            }
                            break;
                        }
                    case Keys.Right:
                        {
                            SendNextTrackKey();
                            break;
                        }
                    case Keys.Left:
                        {
                            SendPreviousTrackKey();
                            break;
                        }
                    case Keys.Down:
                        {
                            SendPlayPauseKey();
                            break;
                        }
                    default:
                        {
                            return;
                        }
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
                decimalIsUnused = false;
            };

            Hook.GlobalEvents().KeyUp += (sender, e) =>
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Decimal:
                        case Keys.Delete:
                            {

                                decimalIsDown = false;
                                if (!decimalIsUnused)
                                {
                                    e.Handled = true;
                                    e.SuppressKeyPress = true;
                                    return;
                                }
                                break;
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