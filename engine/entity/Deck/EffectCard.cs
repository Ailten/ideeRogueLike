
public enum EffectCard
{
    NoEffect, //no effect.

    Hit, //make damage to the target.
    Shild, //add shild to the target.
    Heal, //heal the hp target.
    MPHit, //cast all mp launcher for aply damage to the target.
    Burn, //apply effect burn to target.
    MoneyLoot, //up gold of target.
    Push //push target of n cell.
}


public static class StaticEffectCard
{
    public static string getName(this EffectCard effectCard)
    {
        switch (effectCard)
        {
            case (EffectCard.NoEffect):
                return "Aucun effet";
            case (EffectCard.Hit):
                return "Attaque";
            case (EffectCard.Shild):
                return "Bouclier";
            case (EffectCard.Heal):
                return "Soin";
            case (EffectCard.MPHit):
                return "Attaque MP";
            case (EffectCard.Burn):
                return "Brulure";
            case (EffectCard.MoneyLoot):
                return "Chercheur d or";
            case (EffectCard.Push):
                return "Pouce";
            default:
                return "No name";
        }
    }

    public static string getDetails(this EffectCard effectCard)
    {
        switch (effectCard)
        {
            case (EffectCard.NoEffect):
                return "ne fait rien.";
            case (EffectCard.Hit):
                return ($"- {effectCard.getName()} :\n" +
                    "effectue N points de degat a la cible.\n" +
                    "arrive a 0 points de vie, la cible meure."
                );
            case (EffectCard.Shild):
                return ($"- {effectCard.getName()} :\n" +
                    "donne N points de bouclier a la cible.\n" +
                    "les points de bouclier protege les points de vie.\n" +
                    "les points de bouclier sont perdu en debut de tour."
                );
            case (EffectCard.Heal):
                return ($"- {effectCard.getName()} :\n" +
                    "soigne N points de vie a la cible.\n" +
                    "les soin s arrete au points de vie max."
                );
            case (EffectCard.MPHit):
                return ($"- {effectCard.getName()} :\n" +
                    "draine tout les MP du lanceur.\n" +
                    "chaque MP converti inflige N degats."
                );
            case (EffectCard.Burn):
                return ($"- {effectCard.getName()} :\n" +
                    $"applique l effet Burn.\n" +
                    "inflige 1 degat a la fin du tour de la cible.\n" +
                    "dure N tour."
                );
            case (EffectCard.MoneyLoot):
                return ("- " + effectCard.getName() + " :\n" +
                    "applique l effet MoneyLoot.\n" +
                    "donne N piece d or en plus a la mort de la cible.\n" +
                    "dure 0 tour."
                );
            case (EffectCard.Push):
                return ("- " + effectCard.getName() + " :\n" +
                    "pouce la cible de N cases."
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

            case(EffectCard.MoneyLoot):
                if (characterTarget == null)
                    return;
                Vector directionPush = characterTarget.indexPosCel - characterLauncher.indexPosCel;
                directionPush.x = Math.Clamp(directionPush.x, -1f, 1f);
                directionPush.y = Math.Clamp(directionPush.y, -1f, 1f);
                for (int i = effectValue; i >= 0; i--)
                {
                    Vector indexPushed = characterTarget.indexPosCel + directionPush;
                    Character? obstacle = TurnManager.getCharacterAtIndexPos(indexPushed);
                    Cel? celDest = RunManager.getCel(indexPushed);
                    bool isPushedOnAnObstacle = (obstacle != null || celDest == null);
                    if (isPushedOnAnObstacle)
                    {
                        characterTarget.getPushedOnAnObsctable(i, obstacle, characterLauncher, refCard);
                        break;
                    }
                    characterTarget.moveTo(indexPushed);
                }
                return;

            case(EffectCard.Push):
                if (characterTarget == null)
                    return;
                characterTarget.statusEffects.Add(new MoneyLoot(
                    characterTarget.idEntity,
                    characterLauncher.idEntity,
                    0,
                    effectValue
                ));
                return;

            default:
                throw new Exception($"useACard find a EffectCard with no effect {effectCard} !");
        }
    }
}