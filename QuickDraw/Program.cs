using System.Diagnostics;

Random random = new Random();

const string startMenu = @"
Quick Draw
Face your opponent and wait for the signal. Once the
signal is given, shoot your opponent by pressing SPACE
before they shoot yoou. It's all about your reaction time.
Choose your opponent:
[1] Easy       (1000 milliseconds)
[2] Medium     (500 milliseconds)
[3] Hard       (250 milliseconds)
[4] Impossible (125 milliseconds)";

const string wait = @"

        _O                    O_        
       |/|_       wait       _|/|
       /\                      /\
      / |                      | \
---------------------------------------";

const string action = @"

        _O                    O_        
       |/|_       FIRE       _|/|
       /\        [SPACE]       /\
      / |                      | \
---------------------------------------";

const string loseSlow = @"

                            >q__o        
    //         Too Slow        / \
   o/__/\      You Lose       /\
        \                    |  \
---------------------------------------";

const string loseFast = @"

                            >q__o        
    //         Too Fast        / \
   o/__/\     You Missed      /\
        \      You Lose      |  \
---------------------------------------";

const string win = @"

       o__p<                      
      / \                       \\
        /\      You Win      /\__\o
       /  |                  /  
---------------------------------------";



while (true)
{
    Console.Clear();
    Console.WriteLine(startMenu);
    TimeSpan neededReactionTime;
    string input = Console.ReadLine();

    switch (input)
    {
        case "1":
            neededReactionTime = TimeSpan.FromMilliseconds(1000);
            break;
        case "2":
            neededReactionTime = TimeSpan.FromMilliseconds(0500);
            break;
        case "3":
            neededReactionTime = TimeSpan.FromMilliseconds(0250);
            break;
        case "4":
            neededReactionTime = TimeSpan.FromMilliseconds(0125);
            break;
        default:
            continue;
    }

    Console.Clear();
    TimeSpan signal = TimeSpan.FromMilliseconds(random.Next(5000, 10000));
    Console.WriteLine(wait);

    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Restart();

    bool isTooFast = false;

    while(stopwatch.Elapsed < signal && !isTooFast)
    {
        if(Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Spacebar)
        {
            isTooFast = true;
        }
    }

    Console.Clear();

    if(isTooFast)
    {
        Console.WriteLine(loseFast);
    }
    else
    {
        Console.WriteLine(action);
        stopwatch.Restart();
        bool isTooSlow = true;
        TimeSpan reactionTime = default;

        while(stopwatch.Elapsed < neededReactionTime && isTooSlow)
        {
            if(Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Spacebar)
            {
                isTooSlow = false;
                reactionTime = stopwatch.Elapsed;
            }
        }

        Console.Clear();

        if (isTooSlow)
        {
            Console.WriteLine(loseSlow);
        }
        else
        {
            Console.WriteLine(win);
            Console.WriteLine($"Reaction Time : " +
                $"{reactionTime.TotalMilliseconds} milliseconds");
        }
    }
    Console.Write("Press [1] to Play Again or [2] to quit: ");
    string playOrQuit = Console.ReadLine();
    if (playOrQuit == "2")
    {
        Console.Clear();
        Console.WriteLine("Quick Draw was closed.");
        break;
    }
}
