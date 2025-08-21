using Raylib_cs;

class Program 
{
    //TODO : fix bug card fight :

    // add special room. -> boost status effect permanent.

    // add special room. -> fusion. (with % chance to rate depend on number effects on card end).
    // add special room. -> discard.
    // add special room. -> duplicate.

    // add special room. -> boost value effect card. (with % chance fail depend on effect selected and his level).
    // add special room. -> edit edition of a card. (pass normal into shiny, or a cracked into a normal).

    // add details mobs when click on it, menu. !!!


    // make win/loose screen (end run).


    // special effect : (at eatch fight end, one chance on 20 to drop a status effect) (and one chance on 20 to duplicate a status effect). -> Commun.
    // special effect : récupère 1 AP when use a card shiny.
    // special effect : copy effect of statusEffect at next position. (make an instance of copy statusEffect next, when event move the effect status position).
    // se : rall PM make damage.
    // se : push to wall make heal the launcher.
    // se : push to wall make rall PM.
    // se : boost damage make during the turn of an other character than the launcher.
    // se : make a heal on a persone of same team, make a damage around him.
    // se : multiplie damage by purcent HP launcher has lost.
    // se : make heal on him self, gain damage for this turn (depend on the hp healable).


    // add character : Axo, Blaca, Babulle, Chlow, Lunali, Barbak.


    // add a pause menu (back to game, back to main menu, ... other option).
    // -> make button ui pause.


    // make a save progression (a class with data for main progression : character unlock, mobs defeats, gold collected ... etc).
    // -> use for success, progression, ...
    // -> att the end of the run, make verify succes unlock, add on save, do the save on file.
    // -> make cardPool adapt on succes unlocked.
    // -> make option change save emplacement.
    // -> make print purcent progress success on main menu.
    // -> make end run screen with success unlocked during the run.

    // event run is end (die player on fight, or reach the upper level stage).
    // -> end run screen (ui).
    // -> back to main menu.
    // -> make save file.

    // make tuto : (not now)
    // - explain mouse (right click drag and drop, for move cam, and scroll for zoom).
    // - card, etc ...
    // - explain monster, type room.

    // ? KeyboardManager. (for play more speed) (not now) -> number key for card, and space for skip turn.

    // input select seed. -> menu with enter a number befor start run.

    // ? consoleManager for combat print line ?


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