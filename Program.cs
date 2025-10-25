using Raylib_cs;

class Program 
{
    // TODO :

    // add stuff on layer Succes.
    // ? add stuff on layer Option. --> add button abandon the run (maybe).

    // l'IA des mob chase les invoc en priorité (pas sur).  (!!!) (arakne stage 4)
    //ou ils tape en priorité les invoke. 
    //ou change de target (fuit le cac player pour aller au cac d'une invok).
    // les arakne son un sacré fouilli en combat (et les citrouille son repri en mobe classic, pas boss).
    // un bug avec le skip turn des arakne (ou citrouille).
    // at a certain stage, atk from mob don't make any effect.

    // see what append when chest end run. (by hard coding a reward succes).

    // todo minore :
    // ? when select somehting on shop and have not enoug PO (mark price in darkGold and stay button disabled).
    // ? input select seed. -> menu with enter a number befor start run.
    // ? print the seed on the endRun layer.



    // ? do details screen about cel type in left click.

    // add card who steel cart to target.
    // add SE who reduce price on shop.
    // add card who place trap making damage.
    // add card who reduce res for a turn (base on color).



    // test dificulty.


    // make win/loose screen (end run).
    // -> sprite letters saying Win or Loose.
    // -> make end run screen with success unlocked during the run.
    // -> back to main menu button.
    // -> make save file.

    // add a pause menu (back to game, back to main menu, ... other option).
    // -> make button ui pause.


    // make tuto : (not now)
    // - explain mouse (right click drag and drop, for move cam, and scroll for zoom).
    // - card, etc ...
    // - explain monster, type room.


    // ? KeyboardManager. (for play more speed) (not now) -> number key for card, and space for skip turn.

    // ? consoleManager for combat print line ?

    // ? make a run of the day, with a defined seed, character, and card effects randomly send.


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