
public class ShinyGainAP : StatusEffect
{
    public ShinyGainAP(int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1) :
    base(SpriteType.StatusEffect_ShinyGainAP, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
    }


    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"recupere 1 point d'action quand une carte brillante est jouee.\n" +
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return $"Action brillante";
    }
    public override bool isAMalus()
    {
        return false;
    }


    public override void eventWhenUseACard(ref PackageRefCard packageRefCard)
    {
        Card? card = packageRefCard.getCard();
        if (card is null)
            return;

        bool isShiny = (card.cardEdition == CardEdition.Shinny);
        if (isShiny)
        {
            this.getCharacterWhoHasEffect?.increaseAP(1); // boost AP.
        }
    }

    
}