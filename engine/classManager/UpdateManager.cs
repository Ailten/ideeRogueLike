using Raylib_cs;
using System.Diagnostics;

public static class UpdateManager
{

    private static int FPS = 30; //frame per seconde.
    private static int milisecByFrame
    {
        get { return 1000 / FPS ; }
    }
    private static Stopwatch stopwatchForFrameRate = new Stopwatch();
    private static int _deltaTime = 0; //time (in milisec) from last frame.
    public static int deltaTime
    {
        get { return _deltaTime ; }
    }
    private static int _timeFromStartGame = 0; //miliseconde from start the game.
    public static int timeFromStartGame
    {
        get { return _timeFromStartGame ; }
    }


    public static void init()
    {
        //disable frame rate of raylib.
        Raylib.SetTargetFPS(1000);

        //start the stop watch frame rate.
        stopwatchForFrameRate.Start();
    }


    //wait for stay in range frame rate.
    public static void waitEndFrame()
    {
        //mesure milisecondes from last frame render.
        stopwatchForFrameRate.Stop();
        int milisecFromLastUpdate = (int)stopwatchForFrameRate.ElapsedMilliseconds;

        //debug frame rate.
        //Console.WriteLine($"update frame rate : [{milisecFromLastUpdate} / {milisecByFrame}]");

        //frames skiped when to long to render the sceen.
        int frameSkip = milisecFromLastUpdate / milisecByFrame;
        milisecFromLastUpdate %= milisecByFrame;

        //do the sleep (for let the fram rendered at the screen).
        Thread.Sleep(milisecByFrame - milisecFromLastUpdate);

        //adapt amount of frame skip, include frame sleep.
        frameSkip++; 

        //actualise the delta time.
        _deltaTime = milisecByFrame * frameSkip;
        
        //increase time from start game.
        _timeFromStartGame += deltaTime;

        //reset stopWatch for next update event.
        stopwatchForFrameRate.Reset();
        stopwatchForFrameRate.Start();
    }
}