
public class MiniMapUi : Entity
{

    public MiniMapUi(int idLayer) : base(idLayer, SpriteType.none)
    {
        this.isUi = true;
        this.zIndex = 2000;

        this.encrage = new(1, 0);

        this.size = new(0, 0);
    }


    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        float sizeTileWidth = 16f * CanvasManager.scaleCanvas; //size of tile in screen (width).
        Vector sizeTile = new(sizeTileWidth, sizeTileWidth); //size of tile in screen (square).
        Vector origineTile = new();

        Vector posTile = new(); //stock pos tile.
        Vector indexPosRoom = new(); //stock index room.

        if (CanvasManager.isDebug)
        {
            Raylib_cs.Raylib.DrawRectangleLinesEx( // draw rect of full mini map ui.
                new Rect(
                    posToDraw - new Vector(Stage.widthMax * sizeTileWidth, 0),
                    new Vector(Stage.widthMax, Stage.heightMax) * sizeTileWidth
                ),
                1,
                Raylib_cs.Color.Orange
            );
        }

        for (int y = 0; y < Stage.heightMax; y++)
        {

            posTile.y = posToDraw.y + y * sizeTileWidth; //set pos tile y.
            indexPosRoom.y = y;

            for (int x = 0; x < Stage.widthMax; x++)
            {

                posTile.x = posToDraw.x + (x - Stage.widthMax) * sizeTileWidth; //set pos tile x.
                indexPosRoom.x = x;

                Room? roomOfTile = RunManager.currentStage.getRoom(indexPosRoom);
                if (roomOfTile == null) //skip if no room at this index.
                    continue;

                //get object usefull for draw in many case.
                SpriteType? spriteTypeTileN = roomOfTile.getSpriteTypeOfMiniMapTypeRoom();
                Rect rectDestTile = new(posTile, sizeTile);

                //get bool for if draw.
                bool isASpecialRoom = spriteTypeTileN != null;
                bool isAnAdjacenteRoomToWalk = RunManager.currentStage.getClosestDistFromWalked(indexPosRoom) == 1;
                bool isTileAlreadyWalked = RunManager.currentStage.isContainInRoomsWalked(indexPosRoom);
                bool isTheTileCurrent = RunManager.currentStage.currentIndexRoom.x == indexPosRoom.x && RunManager.currentStage.currentIndexRoom.y == indexPosRoom.y;

                //draw back room if walked.
                if (isTileAlreadyWalked)
                {

                    SpriteType spriteTypeTile = roomOfTile.getSpriteTypeOfMiniMap();
                    Raylib_cs.Rectangle rectSourceInTexture = this.sprite.getSpriteTileBySpriteType(spriteTypeTile).getRectSource();

                    //draw tile.
                    Raylib_cs.Raylib.DrawTexturePro(
                        this.sprite.texture, //texture.
                        rectSourceInTexture, //rect source from texture.
                        rectDestTile, //rect desintation at screen.
                        origineTile, //origine, like encrage by adapt at sprite draw in screen (for rotation aply).
                        rotate, //rotation.
                        Raylib_cs.Color.White //color (already white).
                    );

                }

                if (isASpecialRoom && (isTileAlreadyWalked || isAnAdjacenteRoomToWalk))
                { //draw special icon room.

                    SpriteType spriteTypeTile = spriteTypeTileN ?? throw new Exception("SpriteTypeTileN is null !");
                    Raylib_cs.Rectangle rectSourceInTexture = this.sprite.getSpriteTileBySpriteType(spriteTypeTile).getRectSource();

                    //draw special type room.
                    Raylib_cs.Raylib.DrawTexturePro(
                        this.sprite.texture, //texture.
                        rectSourceInTexture, //rect source from texture.
                        rectDestTile, //rect desintation at screen.
                        origineTile, //origine, like encrage by adapt at sprite draw in screen (for rotation aply).
                        rotate, //rotation.
                        Raylib_cs.Color.White //color (already white).
                    );
                }

                //draw marker if the tile is the current pos of player.
                if (isTheTileCurrent)
                {

                    Raylib_cs.Rectangle rectSourceInTexture = this.sprite.getSpriteTileBySpriteType(SpriteType.MiniMapUI_PosPlayer).getRectSource();

                    //draw marker player on the tile map.
                    Raylib_cs.Raylib.DrawTexturePro(
                        this.sprite.texture, //texture.
                        rectSourceInTexture, //rect source from texture.
                        rectDestTile, //rect desintation at screen.
                        origineTile, //origine, like encrage by adapt at sprite draw in screen (for rotation aply).
                        rotate, //rotation.
                        Raylib_cs.Color.White //color (already white).
                    );
                }

            }
        }

    }

}