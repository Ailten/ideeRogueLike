using Raylib_cs;

class Program 
{
    //TODO : fix bug card fight :

    // add special room. -> coffre. !!!
    // add special room. -> boost status effect permanent.

    // add special room. -> fusion. (with % chance to rate depend on number effects on card end).
    // add special room. -> discard.
    // add special room. -> duplicate.

    // add special room. -> boost value effect card. (with % chance fail depend on effect selected and his level).
    // add special room. -> edit edition of a card. (pass normal into shiny, or a cracked into a normal).

    // add details mobs when click on it, menu.


    // add malus effect (like card play has one chance on 20 to become cracked) (and one chance on 10 to cast as shiny). -> Commune.
    // add malus effect (at eatch fight end, one chance on 20 to drop a status effect) (and one chance on 20 to duplicate a status effect). -> Commun.
    // add status effect copy the status effect at the first position.
    // add status effect copy the status effect at right position. (re-find index by match id, and pick the next one, and redirect all event).


    // make win/loose screen (end run).


    // !!! debug special room. (card).
    // special effect : + 5% degats par cartes dans le deck.
    // special effect : + 10% degats si la dernière card jouée a une couleur diférente (revien a zero si same color).
    // special effect : récupère 1 AP when use a card shiny.
    // special effect : + 1 degats par carte shiny dans le deck.
    // special effect : + 30% degats par carte cracked dans le deck.
    // special effect : 50% de chance de créer une copie de la carte jouée dans le cimetière si la carte jouée est cracked.
    // special effect : donne N de shild a chaque carte cassée.
    // special effect : when use a card sans edition 5% passer shiny, 8% passer cracked.


    // make a methode in statusEffect, override to say if a status effect is a malus (for switch background sprite as red one).


    // debug batte bulle, blaca siable, darunya neko, lune allier.
    // debug card explo, splash, flame, rock.


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