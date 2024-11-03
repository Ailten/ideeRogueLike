using Raylib_cs;

public static class MouseManager
{

    private static Entity mouse = new(){ 
        isUi = true
    };

    private static Entity? entityLastFind = null;

    //event for update data mouse (input move mouse and click).
    public static void update()
    {
        if(LayerManager.isTransitionActive) //skip update if in transition layers.
            return;

        //get input from mouse.
        mouse.pos = Raylib.GetMousePosition();
        bool isLeftClickDown = Raylib.IsMouseButtonDown(MouseButton.Left);
        bool isRightClickDown = Raylib.IsMouseButtonDown(MouseButton.Right);
        bool isLeftClickUp = Raylib.IsMouseButtonReleased(MouseButton.Left);
        bool isRightClickUp = Raylib.IsMouseButtonReleased(MouseButton.Right);

        //get entity colide with mouse.
        Entity? entityFindByMouse = ColideManager.findEntityUiColideByMouse(mouse);

        //execute event enter/exit.
        if(entityLastFind == null && entityFindByMouse != null){ //from null to entity.
            entityFindByMouse.eventMouseEnter();
            entityLastFind = entityFindByMouse;
        }
        if(entityLastFind != null && entityFindByMouse == null){ //from entity to null.
            entityLastFind.eventMouseExit();
            entityLastFind = null;
        }
        if(entityLastFind != null && entityFindByMouse != null && entityLastFind.idEntity != entityFindByMouse.idEntity){ //from entity to other entity.
            entityLastFind.eventMouseExit();
            entityFindByMouse.eventMouseEnter();
            entityLastFind = entityFindByMouse;
        }

        //execute event click.
        if(isLeftClickDown && entityFindByMouse != null){ //click left down.
            entityFindByMouse.eventMouseClick(true, true);
        }
        if(isRightClickDown && entityFindByMouse != null){ //click right down.
            entityFindByMouse.eventMouseClick(false, true);
        }
        if(isLeftClickUp && entityFindByMouse != null){ //click left up.
            entityFindByMouse.eventMouseClick(true, false);
        }
        if(isRightClickUp && entityFindByMouse != null){ //click right down.
            entityFindByMouse.eventMouseClick(false, false);
        }

    }

}