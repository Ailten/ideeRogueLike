
public class FauxEffect : StatusEffect
{
    public FauxEffect(int characterIdWhoHasEffect, int characterIdWhoApplyEffect, int turnLife) :
    base(SpriteType.StatusEffect_FauxEffect, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
    }


    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"retire toute les cartes Drama du deck.\n" +
            $"s'active a la fin du combat.\n" +
            $"ou s'active a la mort du lanceur.\n" +
            this.getDescriptionTurn()
        );
    }
    public override string getName()
    {
        return "Faux";
    }
    public override bool isAMalus()
    {
        return true;
    }


    private bool isACardToRemove(Card c)
    {
        return c.cardIllu == SpriteType.CardImg_Drama;
    }

    public override void eventWhenStatusEffectDisapear(
        bool isEndLifeEffect = false,
        bool isEndOfFight = false,
        bool isCharacterWhoHasEffectDie = false,
        bool isCharacterWhoApplyEffectDie = false,
        bool isDestroyByAction = false)
    {
        if (isCharacterWhoHasEffectDie || isDestroyByAction)
            return;

        Character? whoHas = this.getCharacterWhoHasEffect;
        if (whoHas is null)
            return;

        // remove from pioche.
        for (int i = whoHas.deck.cardsInPioche.Count - 1; i >= 0; i--)
        {
            Card c = whoHas.deck.cardsInPioche[i];
            if (isACardToRemove(c))
            {
                whoHas.deck.destroyCardFromPioche(i);
            }
        }

        // remove from hand.
        int cardDelFromHand = 0;
        for (int i = whoHas.deck.cardsInHand.Count - 1; i >= 0; i--)
        {
            Card c = whoHas.deck.cardsInHand[i];
            if (isACardToRemove(c))
            {
                whoHas.deck.destroyCardFromHand(i);
                cardDelFromHand++;
            }
        }

        // remove from cimetiere.
        for (int i = whoHas.deck.cardsInCimetier.Count - 1; i >= 0; i--)
        {
            Card c = whoHas.deck.cardsInCimetier[i];
            if (isACardToRemove(c))
            {
                whoHas.deck.destroyCardFromCimetier(i);
            }
        }

        // replace hand discard by pioche.
        whoHas.deck.piocheManyCard(cardDelFromHand);
    }


}