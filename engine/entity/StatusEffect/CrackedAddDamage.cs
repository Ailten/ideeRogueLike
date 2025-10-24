
public class CrackedAddDamage : StatusEffect
{
    int damageByCracked;

    public CrackedAddDamage(int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1, int damageByCracked = 1) :
    base(SpriteType.StatusEffect_CrackedAddDamage, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.damageByCracked = damageByCracked;
    }


    public override string getDescription()
    {
        int amountOfDamageAddTotal = this.getAmountOfDamageAddTotal();
        return (
            $"- {this.getName()} :\n" +
            $"ogmente les degats de {amountOfDamageAddTotal}.\n" +
            $"{damageByCracked} degats aditionnel par carte fissuree dans le deck.\n" +
            this.getDescriptionTurn()
        );
    }
    public override string getName()
    {
        return $"Duplication de fissure";
    }
    public override bool isAMalus()
    {
        return this.damageByCracked < 0;
    }


    private int getAmountOfDamageAddTotal()
    {
        Character? whoHas = this.getCharacterWhoHasEffect;
        if (whoHas is null)
            return 0;

        Deck deck = whoHas.deck;

        int crackedCount = 0;
        crackedCount += deck.cardsInCimetier.Where(c => c.cardEdition == CardEdition.Cracked).Count();
        crackedCount += deck.cardsInHand.Where(c => c.cardEdition == CardEdition.Cracked).Count();
        crackedCount += deck.cardsInPioche.Where(c => c.cardEdition == CardEdition.Cracked).Count();
        return (crackedCount * this.damageByCracked);
    }


    public override void eventWhenTargetMakeDamage(ref Character target, ref int atk, ref PackageRefCard? refCard)
    {
        atk += this.getAmountOfDamageAddTotal(); // increase atk by sending reference.
    }

}