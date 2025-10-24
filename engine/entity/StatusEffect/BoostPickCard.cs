
public class BoostPickCard : StatusEffect
{
    private int cardPickBoost;

    public BoostPickCard(int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1, int cardPickBoost = 1) :
    base(SpriteType.StatusEffect_BoostPickCard, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.cardPickBoost = cardPickBoost;
    }
    public override void ActivateEffect()
    {
        this.getCharacterWhoHasEffect!.deck.pickCountByTurn += this.cardPickBoost;
    }


    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"ogmente le nombre de carte piochee de {this.cardPickBoost}.\n" +
            $"la pioche s effectue au debut du tour.\n" +
            this.getDescriptionTurn()
        );
    }
    public override string getName()
    {
        return $"Pioche cartes";
    }
    public override bool isAMalus()
    {
        return this.cardPickBoost < 0;
    }
    

    public override void eventWhenStatusEffectDisapear(
        bool isEndLifeEffect = false,
        bool isEndOfFight = false,
        bool isCharacterWhoHasEffectDie = false,
        bool isCharacterWhoApplyEffectDie = false,
        bool isDestroyByAction = false)
    {
        Character? whoHas = this.getCharacterWhoHasEffect;
        if (whoHas is null)
            return;

        whoHas.deck.pickCountByTurn -= this.cardPickBoost; // cancel effect.
    }
}