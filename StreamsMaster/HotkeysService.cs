using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace StreamsMaster
{
    class HotkeysService : IDisposable
    {
        private List<Hotkey> _registeredHotkeys = new List<Hotkey>();
        private VolumeService _volumeService = new VolumeService();

        public void RegisterHotkeys(Form placeholder)
        {
            //this.hook.KeyIntercepted += Hook_KeyIntercepted;
            RegisterHotkey(Keys.VolumeDown, LowerAppVolume, LowerSystemVolume, placeholder);
            RegisterHotkey(Keys.VolumeUp, RaiseAppVolume, RaiseSystemVolume, placeholder);
            RegisterHotkey(Keys.VolumeMute, MuteUnmuteAppVolume, MuteUnmuteSystemVolume, placeholder);
        }

        private void MuteUnmuteSystemVolume(object sender, HandledEventArgs e)
        {
            _volumeService.MuteUnmuteSystemVolume();
        }

        private void RaiseSystemVolume(object sender, HandledEventArgs e)
        {
            _volumeService.RaiseSystemVolume();

        }

        private void LowerSystemVolume(object sender, HandledEventArgs e)
        {
            _volumeService.LowerSystemVolume();

        }

        private void MuteUnmuteAppVolume(object sender, HandledEventArgs e)
        {
            _volumeService.MuteUnmuteAppVolume();
        }

        private void RaiseAppVolume(object sender, HandledEventArgs e)
        {
            _volumeService.RaiseAppVolume();
        }

        private void LowerAppVolume(object sender, HandledEventArgs e)
        {
            _volumeService.LowerAppVolume();
        }

        private void RegisterHotkey(Keys key, HandledEventHandler pressedWithCtrl, HandledEventHandler pressedAlone, Form placeholder)
        {
            //var normalHotkey = new Hotkey
            //{
            //    KeyCode = key,
            //    Control = false
            //};
            //normalHotkey.Pressed += pressedAlone;

            var withCtrlHotkey = new Hotkey
            {
                KeyCode = key,
                Control = true
            };
            withCtrlHotkey.Pressed += pressedWithCtrl;

            //if (!normalHotkey.GetCanRegister(placeholder) || !withCtrlHotkey.GetCanRegister(placeholder))
            if (!withCtrlHotkey.GetCanRegister(placeholder))
            {
                //TODO Send error    
                Console.WriteLine(
                        "Whoops, looks like attempts to register will fail or throw an exception, show an error/visual user feedback");
                return;
            }
            //normalHotkey.Register(placeholder);
            withCtrlHotkey.Register(placeholder);

            //_registeredHotkeys.Add(normalHotkey);
            _registeredHotkeys.Add(withCtrlHotkey);
        }

        public void Dispose()
        {
            foreach (var hk in _registeredHotkeys.Where(hk => hk.Registered))
            {
                hk.Unregister();
            }
        }
    }
}