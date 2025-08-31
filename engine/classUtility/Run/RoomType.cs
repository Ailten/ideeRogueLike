
public enum RoomType
{
    Room,

    Room_Boss, //fight with a boss, allow to walk to next stage.
    Room_Chest, //a chest contain a new card to add to deck (or statusEffect).
    Room_Shop, //a room for let player use his PO to buy card or special effect.
    Room_Discard, //allow to delete a card from deck.
    Room_Duplicate, //allow to duplicate a card on your deck.
    //Room_Fusion, //allow to merge two card.
    //Room_Boost, //send a boost permanent on the player.

    Room_Center,

    Room_Tuto
}


public static class StaticRoomType
{

    //return sprite type of the type room (for minimap).
    public static SpriteType? getSpriteTypeOfMiniMapTypeRoom(this RoomType roomType)
    {
        switch (roomType)
        {
            //case(RoomType.Room_Center):
            //    return SpriteType.MiniMapUI_RoomCenter;
            case (RoomType.Room_Boss):
                return SpriteType.MiniMapUI_RoomBoss;
            case (RoomType.Room_Chest):
                return SpriteType.MiniMapUI_RoomChest;
            case (RoomType.Room_Shop):
                return SpriteType.MiniMapUI_RoomShop;
            case(RoomType.Room_Discard):
                return SpriteType.MiniMapUI_RoomDiscard;
            case(RoomType.Room_Duplicate):
                return SpriteType.MiniMapUI_RoomDuplicate;
            //case(RoomType.Room_Boost):
            //    return SpriteType.MiniMapUI_RoomBoost;
            //case(RoomType.Room_Fusion):
            //    return SpriteType.MiniMapUI_RoomFusion;

            default:
                return null;
        }
    }
}