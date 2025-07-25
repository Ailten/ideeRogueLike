
public static class ColideManager
{

    public static Entity? findEntityUiColideByMouse(Entity mouse)
    {
        return EntityManager.entities.Where((e) =>  //filter entities valid for try colide to mouse.
            e.geometryTrigger != null &&
            e.isActive
        ).OrderBy((e) =>  //sort by draw order reverce (most close to cam in first).
            3000 - e.zIndex
        ).ToList().FirstOrDefault((e) => //return first entity the mouse is in geometry trigger (or null).

            isPosIsInRect(mouse.pos, e.geometryTriggerNN.getRectAtScreen(e)) //eval the colide.

            || (e.geometryTriggerSecond != null && isPosIsInRect(mouse.pos, e.geometryTriggerSecondNN.getRectAtScreen(e))) //second trigger (if has one).

        );
    }

    //ask if a pos is in a rect.
    public static bool isPosIsInRect(Vector pos, Rect rect)
    {
        return (
            pos.x > rect.posStart.x &&
            pos.y > rect.posStart.y &&
            pos.x < rect.posStart.x + rect.size.x &&
            pos.y < rect.posStart.y + rect.size.y
        );
    }

}