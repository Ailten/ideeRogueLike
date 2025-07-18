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

    //use sprite mobs (kingSlime, kingFlame, kingRock).
    //use sprite card "axe au loot".
    //use sprite cel coffre, fusion, discard, duplicate, boost.

    // add a special room for "boost card effect".
    
    // draw time line, pause border, and status effect.


    // ? consoleManager for combat print line ?

    // add a pause menu (back to game, back to main menu, ... other option).


    // make a save progression (a class with data for main progression : character unlock, mobs depeats, gold collected ... etc).
    // -> use for success, progression, ...


    // json manager (for success, save, etc).

    // make tuto : (not now)
    // - explain mouse (right click drag and drop, for move cam, and scroll for zoom).
    // - card, etc ...

    // KeyboardManager. (for play more speed) (not now) -> number key for card, and space for skip turn.

    // input select seed. -> menu with enter a number befor start run.


    public static void Main(string[] args)
    {
        //dotnet run

        //init all manager.
        CanvasManager.init("ideeRogueLike");
        CanvasManager.setWindowIcon("gameIcon");
        UpdateManager.init();
        SpriteManager.init(); //include all sprite path.
        FontManager.init(); //include all font by enum.

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

        //deinit all.
        LayerManager.deinit(); //un active all layer.
        SpriteManager.deinit();
        FontManager.deinit();

        //close window.
        Raylib.CloseWindow();

    }

}