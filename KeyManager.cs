using System.Diagnostics;
using System.Runtime.InteropServices;
using Desktop.Robot;

namespace TibiaBot;

public class KeyManager
{
    const uint WM_KEYDOWN = 0x100;
    const uint WM_KEYUP = 0x0101;

    private Process _process;
    private IntPtr _windowHandle => _process.MainWindowHandle;

    public KeyManager(Process process)
    {
        _process = process;
    }

    IntPtr GetKeyPointer(Key key)
    {
        var metadata = key.GetKeycode();
        return (IntPtr)metadata.Keycode;
    }

    public void SendKeyDown(Key key)
    {
        var keyPointer = GetKeyPointer(key);
        PostMessage(_windowHandle, WM_KEYDOWN, keyPointer, IntPtr.Zero);
    }

    public void SendKeyUp(Key key)
    {
        var keyPointer = GetKeyPointer(key);
        PostMessage(_windowHandle, WM_KEYUP, keyPointer, IntPtr.Zero);
    }

    [DllImport("user32.dll")]
    private static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
}