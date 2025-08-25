using Raylib_cs;

class Program 
{
    //TODO : fix bug card fight :

    // add special room. -> fusion. (with % chance to rate depend on number effects on card end).
    // add special room. -> discard.
    // add special room. -> duplicate.

    // add special room. -> boost value effect card. (with % chance fail depend on effect selected and his level).
    // add special room. -> edit edition of a card. (pass normal into shiny, or a cracked into a normal).

    // add details mobs when click on it, menu. !!!


    // make win/loose screen (end run).
    // -> sprite letters saying Win or Loose.
    // -> make end run screen with success unlocked during the run.
    // -> back to main menu button.
    // -> make save file.


    // se : shild give are multiply by 2 ~ 3 when has 5 card or more in hands.
    // se : convert 50 ~ 100 purcent heal maked into shild (if zero or less, do nothing).


    // add character : Axo, Blaca, Babulle, Chlow, Lunali, Barbak.


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