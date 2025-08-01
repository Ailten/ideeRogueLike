using Raylib_cs;

class Program 
{
    //TODO : fix bug card fight :

    // debug flame, and rock (mobs and card).

    // add special room. -> coffre.
    // add special room. -> fusion.
    // add special room. -> discard.
    // add a card "axe au loot" -> sprite & cardManager.
    // add boss first floor. -> king slime.
    // add statusEffect. -> add N damage make for M color card.
    // add statusEffect. -> reduce N damage taked by M color card.
    // add StatusEffectManager. -> for generate a statusEffect random (like cardManager).
    // add status effect print ui -> ui list<statusEffect> with button to change order.
    // add details mobs when click on it, menu.
    // add timeLineBattleUi -> for timeline in battle.

    // use sprite mobs (kingSlime, kingFlame, kingRock).
    // use sprite cel coffre, fusion, discard, duplicate, boost.

    // add a special room for "boost card effect".


    // make a pool of status effect (for effects can be pick on special room).
    // add malus effect (like card play has one chance on 6 to become cracked).


    // make win/loose screen (end run).


    // add traps effect (on a card).
    // -> when a fight is end : delete effect Cel apply by a character (like traps).

    // add card Darunya Neko. ?
    // add card Lune Alliee. ?
    // add card Blac a siable. ?
    // add card Batte Bulle. ?



    // add a pause menu (back to game, back to main menu, ... other option).
    // -> make button ui pause.


    // make a save progression (a class with data for main progression : character unlock, mobs depeats, gold collected ... etc).
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

    // KeyboardManager. (for play more speed) (not now) -> number key for card, and space for skip turn.

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