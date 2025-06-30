using Raylib_cs;

class Program 
{
    //TODO : fix bug card fight :
    // - first click on cel with card selected make no effect (second yes).
    // - wturn is skiped automatically (disable this feature).
    // -- when turn is auto skip : make stay card in hand : not defaus.
    // -- when turn is auto skip : hitbox of hand stay for the second rect (card selected top).
    // - when fight is end card hand ui is stay heer (but card is put on defaus).





    // HUD deck (pioche and cimetier).

    // draw menu pause-button.

    // draw menu print card in deck.



    // json manager (for success, save, etc).

    // make tuto : (not now)
    // - explain mouse (right click drag and drop, for move cam, and scroll for zoom).
    // - card, etc ...

    // KeyboardManager. (for play more speed) (not now)

    // input select seed.


    public static void Main(string[] args)
    {
        //dotnet run

        //init all manager.
        CanvasManager.init("ideeRogueLike");
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