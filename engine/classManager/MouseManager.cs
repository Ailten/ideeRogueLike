using Raylib_cs;

public static class MouseManager
{

    private static Entity mouse = new(){ 
        isUi = true
    };

    private static Entity? entityLastFind = null;

    public static bool isWheelMoveTheZoomCam = false;
    public static bool isRightMouseMovePosCam = false;

    public static Vector posMouseLastRightClickHandle = new();
    public static bool isMouseRightClickStayHandle = false;


    //event for update data mouse (input move mouse and click).
    public static void update()
    {
        if(LayerManager.isTransitionActive) //skip update if in transition layers.
            return;

        //get input from mouse.
        mouse.pos = Raylib.GetMousePosition();
        bool isLeftClickDown = Raylib.IsMouseButtonPressed(MouseButton.Left);
        bool isRightClickDown = Raylib.IsMouseButtonPressed(MouseButton.Right);
        bool isLeftClickUp = Raylib.IsMouseButtonReleased(MouseButton.Left);
        bool isRightClickUp = Raylib.IsMouseButtonReleased(MouseButton.Right);
        float wheelMove = Raylib.GetMouseWheelMove();

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
        if(entityFindByMouse != null){
            if(isLeftClickDown){ //click left down.
                entityFindByMouse.eventMouseClick(true, true);
            }
            if(isRightClickDown){ //click right down.
                entityFindByMouse.eventMouseClick(false, true);
            }
            if(isLeftClickUp){ //click left up.
                entityFindByMouse.eventMouseClick(true, false);
            }
            if(isRightClickUp){ //click right down.
                entityFindByMouse.eventMouseClick(false, false);
            }
        }


        //wheel move.
        if(isWheelMoveTheZoomCam && wheelMove != 0f){ //aply wheel move into edit zoom cam.
            CameraManager.editZoomCam(wheelMove);
        }

        //mouse left click move cam pos.
        if(isRightMouseMovePosCam){

            if(isMouseRightClickStayHandle){ //stay down.
                Vector movementMouseThisFrame = posMouseLastRightClickHandle - mouse.pos;
                CameraManager.movePosCam(movementMouseThisFrame);
                posMouseLastRightClickHandle = mouse.pos;
            }
            if(isRightClickDown){ //click down.
                posMouseLastRightClickHandle = mouse.pos;
                isMouseRightClickStayHandle = true;
            }
            if(isRightClickUp){ //click up.
                isMouseRightClickStayHandle = false;
            }

        }

    }

}