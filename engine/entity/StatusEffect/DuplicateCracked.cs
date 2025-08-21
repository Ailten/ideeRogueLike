
public class DuplicateCracked : StatusEffect
{

    private float purcentSave;

    public DuplicateCracked(int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1, float purcentSave = 0.1f) :
    base(SpriteType.StatusEffect_DuplicateCracked, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.purcentSave = purcentSave;
    }


    public override string getDescription()
    {
        int purcentSave = (int)((this.purcentSave) * 100);
        return (
            $"- {this.getName()} :\n" +
            $"lorce qu'une carte ce casse :\n" +
            $"peu creer une copie de la carte cassee en cimetiere.\n" +
            $"{purcentSave}% de chance de copier la carte.\n" +
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return $"Duplication de fissure";
    }
    public override bool isAMalus()
    {
        return false;
    }


    public override void eventWhenCardBroke(ref PackageRefCard packageRefCard)
    {
        Card card = packageRefCard.getCard();

        int rngSave = RandomManager.rng.Next(1000);
        int rangeForSave = (int)Vector.lerpF(0, 999, this.purcentSave);
        if (rngSave < rangeForSave)
        {
            packageRefCard.character.deck.addCardToDeck(card, isSameColor: true); // add a copy before destroy.
        }
    }
}