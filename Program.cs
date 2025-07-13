using Raylib_cs;

class Program 
{
    //TODO : fix bug card fight :
    // add special room. (pnj, text, choose option, ...). -> coffre.
    // add boss first floor.
    // ~ add loot mob.


    // add the first statusEffect. -> add N damage make.
    // + status effect print ui, and button to change order.

    // details mobs when click on it, menu.


    // add fx battle when character is heal.
    // add an ui for timeline in battle.


    // ~ add text recap 3 last action in fight.


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