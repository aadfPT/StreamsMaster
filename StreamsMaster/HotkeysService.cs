using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace StreamsMaster
{
    class HotkeysService
    {
        
        //private KeyboardHook hook;

        //private bool enabled = true;

        //private bool Hook_KeyIntercepted(KeyboardHook.KeyboardHookEventArgs e)
        //{
        //    if (!enabled) return true;
        //    if (!e.KeyName.Contains("Volume")) return true;


        //    //Thanks! https://stackoverflow.com/questions/2534595/get-master-sound-volume-in-c-sharp
        //    VolumeService.MMDeviceEnumerator devEnum = new VolumeService.MMDeviceEnumerator();
        //    MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
        //    switch (e.KeyName)
        //    {
        //        case "VolumeUp":
        //            defaultDevice.AudioEndpointVolume.VolumeStepUp();
        //            break;
        //        case "VolumeDown":
        //            defaultDevice.AudioEndpointVolume.VolumeStepDown();
        //            break;
        //        case "VolumeMute":
        //            defaultDevice.AudioEndpointVolume.Mute = !defaultDevice.AudioEndpointVolume.Mute;
        //            break;
        //        case "MediaNextTrack":
        //            break;
        //    }
        //    return false;
        //}

        public void RegisterHotkeys(Form placeholder)
        {
            //this.hook.KeyIntercepted += Hook_KeyIntercepted;
            RegisterHotkey(Keys.VolumeDown, LowerAppVolume, LowerSystemVolume, placeholder);
            RegisterHotkey(Keys.VolumeUp, RaiseAppVolume, RaiseSystemVolume, placeholder);
            RegisterHotkey(Keys.VolumeMute, MuteUnmuteAppVolume, MuteUnmuteSystemVolume, placeholder);
        }

        private void MuteUnmuteSystemVolume(object sender, HandledEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RaiseSystemVolume(object sender, HandledEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void LowerSystemVolume(object sender, HandledEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MuteUnmuteAppVolume(object sender, HandledEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RaiseAppVolume(object sender, HandledEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void LowerAppVolume(object sender, HandledEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void RegisterHotkey(Keys key, HandledEventHandler pressedWithCtrl, HandledEventHandler pressedAlone, Form placeholder)
        {
            var normalHotkey = new Hotkey
            {
                KeyCode = key,
                Control = false
            };
            normalHotkey.Pressed += pressedAlone; //delegate { Console.WriteLine("Windows+1 pressed!"); };

            var withCtrlHotkey = new Hotkey
            {
                KeyCode = key,
                Control = true
            };
            withCtrlHotkey.Pressed += pressedWithCtrl; //delegate { Console.WriteLine("Windows+1 pressed!"); };

            if (!normalHotkey.GetCanRegister(placeholder) || !withCtrlHotkey.GetCanRegister(placeholder))
                {
                    Console.WriteLine(
                        "Whoops, looks like attempts to register will fail or throw an exception, show an error/visual user feedback");
                }
                else
                {
                    normalHotkey.Register(placeholder);
                    withCtrlHotkey.Register(placeholder);
                }
            //// .. later, at some point
            //if (hk.Registered)
            //{ hk.Unregister(); }
        }
    }
}