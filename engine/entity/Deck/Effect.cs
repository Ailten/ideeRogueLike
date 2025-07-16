
public enum EffectCard
{
    NoEffect, //no effect.

    Hit, //make damage to the target.
    Shild, //add shild to the target.
    Heal, //heal the hp target.
    MPHit, //cast all mp launcher for aply damage to the target.
    Burn, //apply effect burn to target.
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
            case (EffectCard.MPHit):
                return ("- " + effectCard.ToString() + " :\n" +
                    "draine tout les MP du lanceur.\n" +
                    "chaque MP converti inflige N degats."
                );
            case (EffectCard.Burn):
                return ("- " + effectCard.ToString() + " :\n" +
                    "applique l effet Burn.\n" +
                    "inflige 1 degat a la fin du tour de la cible.\n" +
                    "dure N tour."
                );
            default:
                return "cette effet n'a pas de description.";
        }
    }

    //execute an effect card.
    public static void doEffectCard(this EffectCard effectCard, Character characterLauncher, Vector indexPosTarget, int effectValue, int indexCardHand, PackageRefCard? refCard = null)
    {
        Character? characterTarget = TurnManager.getCharacterAtIndexPos(indexPosTarget);

        switch (effectCard)
        {
            case (EffectCard.Hit):
                if (characterTarget == null)
                    return;
                characterLauncher.makeDamage(characterTarget, effectValue, refCard);
                return;

            case (EffectCard.Shild):
                if (characterTarget == null)
                    return;
                characterLauncher.giveShild(characterTarget, effectValue, refCard);
                return;

            case(EffectCard.Heal):
                if (characterTarget == null)
                    return;
                characterLauncher.giveHeal(characterTarget, effectValue, refCard);
                return;

            case(EffectCard.MPHit):
                int damage = characterLauncher.MP * effectValue;
                characterLauncher.decreaseMP(characterLauncher.MP);
                if (characterTarget == null)
                    return;
                characterLauncher.makeDamage(characterTarget, damage, refCard);
                return;

            case(EffectCard.Burn):
                if (characterTarget == null)
                    return;
                characterTarget.statusEffects.Add(new Burn(
                    characterTarget.idEntity,
                    characterLauncher.idEntity,
                    effectValue,
                    1
                ));
                return;

            default:
                throw new Exception($"useACard find a EffectCard with no effect {effectCard} !");
        }
    }
}