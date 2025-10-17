using Raylib_cs;

class Program 
{
    // TODO :

    // add stuff on layer Option.
    // add stuff on layer Succes.
    // add stuff on layer EndFight.

    // debug :
    // les details des statusEffect en layer details ne s'affiche pas.
    // king rock can attire without being in line ?? (and attack on same turn)
    // when hit no effect visuel. ?? (when mob stage 1, it work)

    // try to kill a mod by burn. <---

    // do details screen about cel type in left click ?




    // add a card who steel a card from target.

    // add sprite option disabled ?
    // implement layer of option.

    // test dificulty.

    // make a layer PrintSuccesUnlocked
    // make a layer end run (win and loose).


    // need be sure is debug : background opacity light, and time line invoke, and special room fusion card recto when no selected.


    // make win/loose screen (end run).
    // -> sprite letters saying Win or Loose.
    // -> make end run screen with success unlocked during the run.
    // -> back to main menu button.
    // -> make save file.


    // add character : Babulle, Chlow.


    // add a pause menu (back to game, back to main menu, ... other option).
    // -> make button ui pause.



    // make tuto : (not now)
    // - explain mouse (right click drag and drop, for move cam, and scroll for zoom).
    // - card, etc ...
    // - explain monster, type room.

    // ? KeyboardManager. (for play more speed) (not now) -> number key for card, and space for skip turn.

    // input select seed. -> menu with enter a number befor start run.

    // ? consoleManager for combat print line ?


    // make a run of the day, with a defined seed, character, and card effects randomly send.


    public static void Main(string[] args)
    {
        //dotnet run

        //init all manager.
        CanvasManager.init("ideeRogueLike");
        CanvasManager.setWindowIcon("gameIcon");
        UpdateManager.init();
        SpriteManager.init(); //include all sprite path.
        FontManager.init(); //include all font by enum.
        SaveManager.timePlayStart(); //start timer for open window.
        SaveManager.loadFileSave(); //load file save default.

        //active all layers for start window.
        MainMenu.layer.active();

        CanvasManager.isDebug = true; //set debug mode.

        //stay open window (with exit input).
        while (!Raylib.WindowShouldClose())
        {

            //resize window.
            if (Raylib.IsWindowResized())
            {
                CanvasManager.resizeWindow(new(
                    Raylib.GetScreenWidth(),
                    Raylib.GetScreenHeight()
                ));
            }

            //get update user from mouse the frame.
            MouseManager.update();

            //update transition between layers.
            LayerManager.updateTransition();

            //do update for all layers active.
            LayerManager.updateLayers();

            //draw all entities in frame.
            CanvasManager.drawFrame();

            //wait for frame rate.
            UpdateManager.waitEndFrame();

            //exit loop update when bool close window was set.
            if (CanvasManager.isCloseWindow)
            {
                break;
            }

        }

        //save.
        SaveManager.saveFileSave();

        //deinit all.
        LayerManager.deinit(); //un active all layer.
        SpriteManager.deinit();
        FontManager.deinit();

        //close window.
        Raylib.CloseWindow();

    }

}