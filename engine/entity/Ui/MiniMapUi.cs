
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

        for(int y=0; y<Stage.heightMax; y++){

            posTile.y = posToDraw.y + y * sizeTileWidth; //set pos tile y.
            indexPosRoom.y = y;

            for(int x=0; x<Stage.widthMax; x++){

                posTile.x = posToDraw.x + (x - Stage.widthMax) * sizeTileWidth; //set pos tile x.
                indexPosRoom.x = x;

                bool isTileAlreadyWalked = RunManager.currentStage.isContainInRoomsWalked(indexPosRoom);
                if(!isTileAlreadyWalked) //skip tile if player not walked.
                    continue;

                //get sprite tile.
                Room? roomOfTile = RunManager.currentStage.getRoom(indexPosRoom);
                if(roomOfTile == null)
                    continue;

                //get sprite type.
                SpriteType spriteTypeTile = roomOfTile.getSpriteTypeOfMiniMap();
                Raylib_cs.Rectangle rectSourceInTexture = this.sprite.getSpriteTileBySpriteType(spriteTypeTile).getRectSource();

                Rect rectDestTile = new(posTile, sizeTile);

                //draw tile.
                Raylib_cs.Raylib.DrawTexturePro(
                    this.sprite.texture, //texture.
                    rectSourceInTexture, //rect source from texture.
                    rectDestTile, //rect desintation at screen.
                    origineTile, //origine, like encrage by adapt at sprite draw in screen (for rotation aply).
                    rotate, //rotation.
                    Raylib_cs.Color.White //color (already white).
                );


                //get sprite type of type room.
                SpriteType? spriteTypeTileN = roomOfTile.getSpriteTypeOfMiniMapTypeRoom();
                if(spriteTypeTileN != null){
                    spriteTypeTile = spriteTypeTileN ?? throw new Exception("SpriteTypeTileN is null !");
                    rectSourceInTexture = this.sprite.getSpriteTileBySpriteType(spriteTypeTile).getRectSource();

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
                if(RunManager.currentStage.currentIndexRoom.x == indexPosRoom.x && RunManager.currentStage.currentIndexRoom.y == indexPosRoom.y){
                    rectSourceInTexture = this.sprite.getSpriteTileBySpriteType(SpriteType.MiniMapUI_PosPlayer).getRectSource();

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