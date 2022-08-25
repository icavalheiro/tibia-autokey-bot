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
    await PressKeyOnInterval(TimeSpan.FromMinutes(1), Key.Nine);
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

async Task PressKeyOnInterval(TimeSpan interval, Key key)
{
    var cooldown = interval.Milliseconds;
    while (true)
    {
        lock (_queue)
        {
            _queue.Append(key);
        }

        await Task.Delay(cooldown);
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

Console.WriteLine("starting threads");

ConsumeQueue().Start();
EatFood().Start();
AtkSelectedMob().Start();
UseHealSkill().Start();

Console.WriteLine("Press X to close.");

char key = ' ';
while (key != 'x')
{
    key = Console.ReadKey().KeyChar;
}