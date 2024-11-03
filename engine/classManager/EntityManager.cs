
public static class EntityManager
{

    private static List<Entity> _entities = new(); //all entities instanciate.
    public static List<Entity> entities
    {
        get { return _entities; }
    }
    private static int entitesIndexCount = 0;


    //push a new entity in list entities (after init).
    public static void pushNewEntity(Entity e)
    {
        e.idEntity = entitesIndexCount++; //set id unique.

        _entities.Add(e); //push in list entities.
    }

    //get all enetities of a layer.
    public static List<Entity> getEntitiesByIdLayer(int idLayer)
    {
        return _entities.Where((e) => e.idLayer == idLayer).ToList();
    }

    //sort all entities by zIndex.
    public static void sortAllEntities()
    {
        _entities.Sort((a, b) => a.zIndex.CompareTo(b.zIndex));
    }

    //remove all entities of a layer (out of entities).
    public static void removeEntitiesOfLayer(int idLayer)
    {
        _entities = _entities.Where((e) => e.idLayer != idLayer).ToList(); //drop all entity in layer (stay order).
    }

}