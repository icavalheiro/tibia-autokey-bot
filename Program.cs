using System.Diagnostics;
using System.Runtime.InteropServices;
using Desktop.Robot;
using Desktop.Robot.Extensions;


const uint WM_KEYDOWN = 0x100;
const uint WM_KEYUP = 0x0101;


[DllImport("user32.dll")]
static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

void emulateHumanDelay()
{
    Thread.Sleep(100);
}

IntPtr getKeyPointer(Key key)
{
    var metadata = key.GetKeycode();
    return (IntPtr)metadata.Keycode;
}

// IntPtr hWnd;
Process? p = Process.GetProcessesByName("client").FirstOrDefault();

if (p != null)
{
    IntPtr window = p.MainWindowHandle;

    void pressKey(Key key)
    {
        var keyPointer = getKeyPointer(key);
        PostMessage(window, WM_KEYDOWN, keyPointer, IntPtr.Zero);
    }

    void releaseKey(Key key)
    {
        var keyPointer = getKeyPointer(key);
        PostMessage(window, WM_KEYUP, keyPointer, IntPtr.Zero);
    }

    void sendKey(Key key)
    {
        emulateHumanDelay();
        pressKey(key);
        emulateHumanDelay();
        releaseKey(key);
        emulateHumanDelay();
    }

    sendKey(Key.F);
    Thread.Sleep(300);
    sendKey(Key.Nine);
    Thread.Sleep(1000);
    sendKey(Key.Three);
}


// void sendKeystroke()
// {




//         // PostMessage(edit, WM_KEYDOWN, (IntPtr)(Keys.Control), IntPtr.Zero);
//         // PostMessage(edit, WM_KEYDOWN, (IntPtr)(Keys.A), IntPtr.Zero);
//         // PostMessage(edit, WM_KEYUP, (IntPtr)(Keys.A), IntPtr.Zero);
//         // PostMessage(edit, WM_KEYUP, (IntPtr)(Keys.Control), IntPtr.Zero);

// }


// [DllImport("User32.dll")]
// static extern int SetForegroundWindow(IntPtr point);

// Process? p = Process.GetProcessesByName("client").FirstOrDefault();
// if (p != null)
// {
//     var robot = new Robot();
//     IntPtr h = p.MainWindowHandle;

//     void foregroundGame()
//     {
//         Thread.Sleep(300);
//         SetForegroundWindow(h);
//         Thread.Sleep(300);
//     }

//     while (true)
//     {
//         foregroundGame();

//         //food
//         Console.WriteLine("Using food");
//         robot.KeyPress(Key.Nine);
//         Thread.Sleep(300);

//         //heal skill 10x
//         for (int i = 0; i < 10; i++)
//         {
//             foregroundGame();

//             Console.WriteLine("Using heal skill");
//             robot.KeyPress(Key.F);
//             Thread.Sleep(300);

//             Console.WriteLine("Using atk skill");
//             robot.KeyPress(Key.Three);
//             Thread.Sleep(1000);
//         }

//         Thread.Sleep(5000); // wait 5 seconds
//     }
// }


