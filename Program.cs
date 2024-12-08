using Raylib_cs;

class Program 
{

    // TODO :


    // dev a mini-map.



    // push git.
    // unfollow + block le bot.
    // ask somenda what is the surprise qui fait peur.
    // add commande !blaca.



    // HUD stats player in choose character.
    // HUD stats character in fight.
    // HUD HP,AP,MP turnCharacter during fight.
    // mob spawner.
    // mob.

    // draw deck icon (pioche and cimetier).
    // draw skip turn icon.
    // draw menu pause button.

    // json manager (for success, save, etc).

    // make tuto :
    // - explain mouse (right click drag and drop, for move cam, and scroll for zoom).


    // KeyboardManager.

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
        while(!Raylib.WindowShouldClose()){

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
            if(CanvasManager.isCloseWindow){
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