using System.Diagnostics;
using System.Runtime.InteropServices;
using Desktop.Robot;
using TibiaBot;

var tibiaProcess = Process.GetProcessesByName("client").FirstOrDefault();

if (tibiaProcess == null)
{
    Console.WriteLine("¬¬ tibia is not opened you modafoca!");
    return;
}

KeyManager keyManager = new(tibiaProcess);

Queue<Key> _queue = new();

async Task EatFood()
{
    Console.WriteLine("eat food enabled.");
    await PressKeyOnInterval(TimeSpan.FromSeconds(55), Key.Nine);
}

async Task AtkSelectedMob()
{
    Console.WriteLine("atk selected mob enabled.");
    await PressKeyOnInterval(TimeSpan.FromSeconds(3.5), Key.Three);
}

async Task UseHealSkill()
{
    Console.WriteLine("use heal skill enabled.");
    await PressKeyOnInterval(TimeSpan.FromSeconds(1.5), Key.F);
}

async Task MakeRune()
{
    Console.WriteLine("make rune enabled.");
    await PressKeyOnInterval(TimeSpan.FromSeconds(30), Key.F2);
}

async Task UseHealingRing()
{
    Console.WriteLine("auto use healing ring enabled.");
    await PressKeyOnInterval(TimeSpan.FromMinutes(20.2), Key.F3);
}

async Task PressKeyOnInterval(TimeSpan interval, Key key)
{
    var cooldown = interval.TotalMilliseconds;
    while (true)
    {
        if (_queue.Count > 10)//avoid flooding the queue
            continue;

        lock (_queue)
        {
            _queue.Enqueue(key);
        }

        await Task.Delay((int)cooldown);
    }
}

async Task ConsumeQueue()
{
    var cooldown = 200;
    var humanDelay = 100;

    while (true)
    {
        await Task.Delay(cooldown);

        if (_queue.Count == 0)
            continue;

        Key key;
        lock (_queue)
        {
            key = _queue.Dequeue();
        }

        keyManager.SendKeyDown(key);
        await Task.Delay(humanDelay);
        keyManager.SendKeyUp(key);
    }
}


void RunMonkMode()
{
    Console.WriteLine("starting Monk Mode....");

    Task.Run(ConsumeQueue);
    Task.Run(EatFood);
    Task.Run(AtkSelectedMob);
    Task.Run(UseHealSkill);
}

void RunRunerMode()
{
    Console.WriteLine("starting Runer Mode....");
    Task.Run(ConsumeQueue);
    Task.Run(EatFood);
    Task.Run(MakeRune);
    Task.Run(UseHealingRing);
}


void WaitForExit()
{
    Thread.Sleep(1000);

    Console.WriteLine("Press X to close:");

    char key = ' ';
    while (key != 'x')
    {
        key = Console.ReadKey().KeyChar;
    }
}

Console.WriteLine("Please select mode:");
Console.WriteLine("[1] Monk mode (atk, food, heal)");
Console.WriteLine("[2] Runner mode (rune, food, ring)");

char selection;

do
{
    selection = Console.ReadKey().KeyChar;
}
while (selection != '1' && selection != '2');

Console.WriteLine("");

switch (selection)
{
    default:
        Console.WriteLine("invalid mode.");
        break;
    case '1':
        RunMonkMode();
        break;
    case '2':
        RunRunerMode();
        break;
}

WaitForExit();

