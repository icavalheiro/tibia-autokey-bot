using System.Diagnostics;
using System.Runtime.InteropServices;
using Desktop.Robot;
using Desktop.Robot.Extensions;


[DllImport("User32.dll")]
static extern int SetForegroundWindow(IntPtr point);

Process? p = Process.GetProcessesByName("client").FirstOrDefault();
if (p != null)
{
    var robot = new Robot();
    IntPtr h = p.MainWindowHandle;

    void foregroundGame()
    {
        Thread.Sleep(300);
        SetForegroundWindow(h);
        Thread.Sleep(300);
    }

    while (true)
    {
        foregroundGame();

        //food
        Console.WriteLine("Using food");
        robot.KeyPress(Key.Nine);
        Thread.Sleep(300);

        //heal skill 10x
        for (int i = 0; i < 10; i++)
        {
            foregroundGame();

            Console.WriteLine("Using heal skill");
            robot.KeyPress(Key.F);
            Thread.Sleep(300);

            Console.WriteLine("Using atk skill");
            robot.KeyPress(Key.Three);
            Thread.Sleep(1000);
        }

        Thread.Sleep(5000); // wait 5 seconds
    }
}


