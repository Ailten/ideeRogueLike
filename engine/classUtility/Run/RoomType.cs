
public enum RoomType
{
    Room,

    Room_Boss, //fight with a boss, allow to walk to next stage.
    Room_Chest, //a chest contain a new card to add to deck.
    Room_Fusion, //allow to merge two card.
    Room_Boost, //send a boost permanent on the player.
    Room_Duplicate, //allow to duplicate a card on your deck.
    Room_Discard, //allow to delete a card from deck.

    Room_Center,

    Room_Tuto
}