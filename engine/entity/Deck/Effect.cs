
public enum EffectCard
{
    NoEffect, //no effect.

    Hit, //make damage to the target.
    Shild, //add shild to the target.
    Heal //heal the hp target.
}


public static class StaticEffectCard
{
    public static string getDetails(this EffectCard effectCard)
    {
        switch (effectCard)
        {
            case (EffectCard.NoEffect):
                return "ne fait rien.";
            case (EffectCard.Hit):
                return ("- " + effectCard.ToString() + " :\n" +
                    "effectue N points de degat a la cible.\n" +
                    "arrivé a 0 points de vie, la cible meure."
                );
            case (EffectCard.Shild):
                return ("- " + effectCard.ToString() + " :\n" +
                    "donne N points de bouclier a la cible.\n" +
                    "les points de bouclier protege les points de vie.\n" +
                    "les points de bouclier sont perdu en debut de tour."
                );
            case (EffectCard.Heal):
                return ("- " + effectCard.ToString() + " :\n" +
                    "soigne N points de vie a la cible.\n" +
                    "les soin s'arrete au points de vie max."
                );
            default:
                return "cette effet n'a pas de description.";
        }
    }

    //execute an effect card.
    public static void doEffectCard(this EffectCard effectCard, Character characterLauncher, Vector indexPosTarget, int effectValue, int indexCardHand)
    {
        Character? characterTarget = TurnManager.getCharacterAtIndexPos(indexPosTarget);

        switch (effectCard)
        {
            case (EffectCard.Hit):
                if (characterTarget == null)
                    return;
                characterLauncher.makeDamage(characterTarget, effectValue);
                return;

            case (EffectCard.Shild):
                if (characterTarget == null)
                    return;
                characterLauncher.giveShild(characterTarget, effectValue);
                return;

            case(EffectCard.Heal):
                if (characterTarget == null)
                    return;
                characterLauncher.giveHeal(characterTarget, effectValue);
                return;

            default:
                throw new Exception($"useACard find a EffectCard with no effect {effectCard} !");
        }
    }
}