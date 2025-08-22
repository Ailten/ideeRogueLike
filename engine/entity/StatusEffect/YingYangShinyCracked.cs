
public class YingYangShinyCracked : StatusEffect
{
    private float purcentCastShiny;
    private float purcentCastCracked;

    public YingYangShinyCracked(int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1, float purcentCastShiny = 0.1f, float purcentCastCracked = 0.1f) :
    base(SpriteType.StatusEffect_YingYangShinyCracked, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.purcentCastShiny = purcentCastShiny;
        this.purcentCastCracked = purcentCastCracked;
    }

    public override string getDescription()
    {
        int purcentShiny = (int)((this.purcentCastShiny) * 100);
        int purcentCracked = (int)((this.purcentCastCracked) * 100);
        return (
            $"- {this.getName()} :\n" +
            $"peu transformer les cartes standard une foi jouee.\n" +
            $"{purcentShiny}% de chance de transformer en brillante.\n" +
            $"{purcentCracked}% de chance de transformer en fissuree.\n" +
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return $"Ying Yang fissure brillante";
    }
    public override bool isAMalus()
    {
        return this.purcentCastShiny > this.purcentCastCracked;
    }


    public override void eventWhenUseACard(ref PackageRefCard packageRefCard)
    {
        Card card = packageRefCard.getCard(); // get card played.
        if (card.cardEdition == CardEdition.Default) // skip the not default edition.
            return;

        int rngShiny = RandomManager.rng.Next(1000); // set rng.
        int rngCracked = RandomManager.rng.Next(1000);
        int rangeForShiny = (int)Vector.lerpF(0, 999, this.purcentCastShiny);
        int rangeForCracked = (int)Vector.lerpF(0, 999, this.purcentCastCracked);
        bool isCastShiny = (rngShiny < rangeForShiny);
        bool isCastCracked = (rngCracked < rangeForCracked);

        if (!isCastShiny && !isCastCracked) // skip rng fail.
            return;
        if (isCastShiny && isCastCracked) // assign only xor bool result.
        {
            isCastShiny = rngShiny < rngCracked;
            isCastCracked = !isCastShiny;
        }

        card.cardEdition = ( // edit card edition.
            (isCastShiny) ? CardEdition.Shinny :
            CardEdition.Cracked
        );
        packageRefCard.character.deck.cardsInHand[packageRefCard.indexCardHand] = card; // edit card in deck.
    }
}