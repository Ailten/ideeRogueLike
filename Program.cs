﻿using Raylib_cs;

class Program 
{

    // TODO :

    //Stage line 225.


    // dev a mini-map.
    // --- sprite already code, need to add an entity miniMap into layer, in the entity code afterDraw, based on a new list pos into Stage.


    // HUD stats player in choose character.
    // HUD stats character in fight.
    // mob spawner.
    // mob.

    // draw deck icon (pioche and cimetier).
    // draw skip turn icon.
    // draw menu pause-button.

    // json manager (for success, save, etc).

    // make tuto :
    // - explain mouse (right click drag and drop, for move cam, and scroll for zoom).



    // new todo :
    // add a button "skip turn" for player, (ui AND exec).
    // add mob turn (walk to player closest player, eval distance to closest player, state, use atk...(not now)).


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