
public class BoostChooseSpecialRoom : StatusEffect
{

    Dictionary<DicoLabelForSpecialRoom, int> dicoToSpecialRoom;

    public BoostChooseSpecialRoom(int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1, int chooseBoost = 1) :
    base(SpriteType.StatusEffect_BoostChooseSpecialRoom, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.dicoToSpecialRoom = new() {
            {DicoLabelForSpecialRoom.increaseChoise, chooseBoost}
        };
    }


    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"gagne {this.dicoToSpecialRoom[DicoLabelForSpecialRoom.increaseChoise]} choix dans les pieces specials.\n" +
            this.getDescriptionTurn()
        );
    }
    public override string getName()
    {
        return $"Choix aux pieces specials";
    }
    public override bool isAMalus()
    {
        return this.dicoToSpecialRoom[DicoLabelForSpecialRoom.increaseChoise] < 0;
    }


    public override Dictionary<DicoLabelForSpecialRoom, int>? eventWhenEnterOnASpecialRoom()
    {
        return this.dicoToSpecialRoom;
    }
}