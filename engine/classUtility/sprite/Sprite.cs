using Raylib_cs;

public class Sprite 
{

    public Texture2D texture;

    public List<SpriteTile> spriteTiles = new();

    public Sprite(string spritePath)
    {
        string pathToSprites = $"assets/sprite/{spritePath}.png";
        
#if DEBUG
        pathToSprites = $"/home/faouzi/Documents/ideeRogueLike/assets/sprite/{spritePath}.png";
#endif

        this.texture = Raylib.LoadTexture(pathToSprites);

        SpriteManager.pushNewSprite(this);
    }


    //add a tile for a sprite type.
    public void addSpriteType(SpriteType spriteType, Vector posStart, Vector size)
    {
        spriteTiles.Add(new SpriteTile(spriteType, posStart, size));
    }

    //get sprite tile by his sprite type.
    public SpriteTile getSpriteTileBySpriteType(SpriteType spriteType)
    {
        return spriteTiles.Find((s) => s.spriteType == spriteType) ?? throw new Exception("SpriteTile not found !");
    }


    //deinit texture.
    public void deinit()
    {
        Raylib.UnloadTexture(texture);
    }
}