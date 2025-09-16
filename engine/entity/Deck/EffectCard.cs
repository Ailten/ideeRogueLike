
public enum EffectCard
{
    NoEffect, //no effect.

    Hit, //make damage to the target.
    Shild, //add shild to the target.
    Heal, //heal the hp target.
    MPHit, //cast all mp launcher for aply damage to the target.
    Burn, //apply effect burn to target.
    MoneyLoot, //up gold of target.
    Push, //push target of n cell.
    TrapMp, //place a trap on cell, who make decrease MP by one.
    TrapAp, //place a trap on cell, who make decrease AP by one.
    HitAround, //hit a cross 1 around the cell target.
    InvokeDarunyaNeko, //invoque a darunya neko.
    InvokeLuneAllier, //invoque a lune allier.
    SelfKill, //instant kill the launcher.
    Attire, //attire target to the launcher.
    PropagatePoison, //poison hit at beginning turn, and try to propage to character cel adjacente.
    TeleportSwitch, //teleport or switch position with the cel target.
    PickCard, //pick a card from deck.
    SelfHeal, //heal the launcher.
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
            case (EffectCard.TrapMp):
                return "Piege MP";
            case (EffectCard.TrapAp):
                return "Piege AP";
            case (EffectCard.HitAround):
                return "Attaque de zone";
            case (EffectCard.InvokeDarunyaNeko):
                return "Invoque Darunya Neko";
            case (EffectCard.InvokeLuneAllier):
                return "Invoque Lune Allier";
            case (EffectCard.SelfKill):
                return "Suicide";
            case (EffectCard.Attire):
                return "Attire";
            case (EffectCard.PropagatePoison):
                return "Propagation";
            case (EffectCard.TeleportSwitch):
                return "Teleporte";
            case (EffectCard.PickCard):
                return "Pioche";
            case (EffectCard.SelfHeal):
                return "Auto Soin";
                
            default:
                return "No name";
        }
    }

    public static string getDetails(this EffectCard effectCard, int? valueIntencity = null)
    {
        string value = valueIntencity?.ToString() ?? "N";

        switch (effectCard)
        {
            case (EffectCard.NoEffect):
                return "ne fait rien.";
            case (EffectCard.Hit):
                return ($"- {effectCard.getName()} :\n" +
                    $"effectue {value} points de degat a la cible.\n" +
                    "arrive a 0 points de vie, la cible meure."
                );
            case (EffectCard.Shild):
                return ($"- {effectCard.getName()} :\n" +
                    $"donne {value} points de bouclier a la cible.\n" +
                    "les points de bouclier protege les points de vie.\n" +
                    "les points de bouclier sont perdu en debut de tour."
                );
            case (EffectCard.Heal):
                return ($"- {effectCard.getName()} :\n" +
                    $"soigne {value} points de vie a la cible.\n" +
                    "les soin s arrete au points de vie max."
                );
            case (EffectCard.MPHit):
                return ($"- {effectCard.getName()} :\n" +
                    "draine tout les MP du lanceur.\n" +
                    $"chaque MP converti inflige {value} degats."
                );
            case (EffectCard.Burn):
                return ($"- {effectCard.getName()} :\n" +
                    $"applique l effet Burn.\n" +
                    "inflige 1 degat a la fin du tour de la cible.\n" +
                    $"dure {value} tour."
                );
            case (EffectCard.MoneyLoot):
                return ("- " + effectCard.getName() + " :\n" +
                    "applique l effet MoneyLoot.\n" +
                    $"donne {value} piece d or en plus a la mort de la cible.\n" +
                    "dure 0 tour."
                );
            case (EffectCard.Push):
                return ("- " + effectCard.getName() + " :\n" +
                    $"pouce la cible de {value} cases."
                );
            case (EffectCard.TrapMp):
                return ("- " + effectCard.getName() + " :\n" +
                    $"pose un piege qui diminue les MP de {value}.\n" +
                    "monte jusque maximum 3."
                );
            case (EffectCard.TrapAp):
                return ("- " + effectCard.getName() + " :\n" +
                    $"pose un piege qui diminue les AP de {value}.\n" +
                    "monte jusque maximum 3."
                );
            case (EffectCard.HitAround):
                return ("- " + effectCard.getName() + " :\n" +
                    $"inflige {value} degats au cas adjacente ciblee."
                );
            case (EffectCard.InvokeDarunyaNeko):
                return ("- " + effectCard.getName() + " :\n" +
                    "invoque 1 Darunya Neko.\n" +
                    $"creature imobile, a {value} AP.\n"+
                    "gagniant 1 AP max a chaque coup recu.\n" +
                    "attaque du 4 autour de lui une fois boost a 2 AP."
                );
            case (EffectCard.InvokeLuneAllier):
                return ("- " + effectCard.getName() + " :\n" +
                    "invoque 1 Lune Allier.\n" +
                    "creature imobile.\n" +
                    $"soigne du {value} a un allier a maximum 2 case d'elle."
                );
            case (EffectCard.SelfKill):
                return ("- " + effectCard.getName() + " :\n" +
                    "tue le lanceur."
                );
            case (EffectCard.Attire):
                return ("- " + effectCard.getName() + " :\n" +
                    $"attire la cible de {value} case vers le lanceur."
                );
            case (EffectCard.PropagatePoison):
                return ("- " + effectCard.getName() + " :\n" +
                    $"applique {value} degat a la cible en debut de tour.\n" +
                    $"propage l'effet au celules adjacente en debut de tour."
                );
            case (EffectCard.TeleportSwitch):
                return ("- " + effectCard.getName() + " :\n" +
                    $"Teleporte le lanceur sur la case ciblee.\n" +
                    $"Si la case est occupee, echange de places."
                );
            case (EffectCard.PickCard):
                return ("- " + effectCard.getName() + " :\n" +
                    $"le lanceur pioche {value} carte{(valueIntencity>1?"s":"")}."
                );
            case (EffectCard.SelfHeal):
                return ("- " + effectCard.getName() + " :\n" +
                    $"soigne {value} au lanceur."
                );



            default:
                return "cette effet n'a pas de description.";
        }
    }

    //execute an effect card.
    public static void doEffectCard(this EffectCard effectCard, Character characterLauncher, Vector indexPosTarget, int effectValue, PackageRefCard? refCard = null)
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

            case (EffectCard.Heal):
                if (characterTarget == null)
                    return;
                characterLauncher.giveHeal(characterTarget, effectValue, refCard);
                return;

            case (EffectCard.MPHit):
                int damage = characterLauncher.MP * effectValue;
                characterLauncher.decreaseMP(characterLauncher.MP);
                if (characterTarget == null)
                    return;
                characterLauncher.makeDamage(characterTarget, damage, refCard);
                return;

            case (EffectCard.Burn):
                if (characterTarget == null)
                    return;
                characterTarget.AddStatusEffect(new Burn(
                    characterTarget.idEntity,
                    characterLauncher.idEntity,
                    effectValue,
                    1
                ));
                return;

            case (EffectCard.MoneyLoot):
                if (characterTarget == null)
                    return;
                characterTarget.AddStatusEffect(new MoneyLoot(
                    characterTarget.idEntity,
                    characterLauncher.idEntity,
                    0,
                    effectValue
                ));
                return;

            case (EffectCard.Push):
                if (characterTarget == null)
                    return;
                Vector directionPush = characterTarget.indexPosCel - characterLauncher.indexPosCel; // get dif.
                int directionPushXAbs = (int)Math.Abs(directionPush.x); // take only the axe farest (stay both if diago).
                int directionPushYAbs = (int)Math.Abs(directionPush.y);
                if (directionPushXAbs > directionPushYAbs)
                    directionPush.y = 0;
                else if (directionPushXAbs < directionPushYAbs)
                    directionPush.x = 0;
                directionPush.x = Math.Clamp(directionPush.x, -1f, 1f); //c lamp for stay at 1 max.
                directionPush.y = Math.Clamp(directionPush.y, -1f, 1f);
                for (int i = effectValue - 1; i >= 0; i--) // aply many push one.
                {
                    Vector indexPushed = characterTarget.indexPosCel + directionPush;
                    Character? obstacle = TurnManager.getCharacterAtIndexPos(indexPushed);
                    Cel? celDest = RunManager.getCel(indexPushed);
                    bool isPushedOnAnObstacle = (obstacle != null || celDest == null);
                    if (isPushedOnAnObstacle)
                    {
                        characterTarget.getPushedOnAnObsctable(i + 1, obstacle, characterLauncher, refCard);
                        break;
                    }
                    characterTarget.moveTo(indexPushed);
                }
                return;

            case (EffectCard.TrapMp):
                Cel? celTargetMP = RunManager.getCel(indexPosTarget);
                if (celTargetMP != null && celTargetMP.celType == CelType.Cel)
                {
                    celTargetMP.celType = (
                        (effectValue <= 1) ? CelType.Cel_SandMPDown :
                        (effectValue == 2) ? CelType.Cel_SandMPDown_2 :
                        CelType.Cel_SandMPDown_3
                    );
                    celTargetMP.idCharacterWhoApplyCelType = characterLauncher.idEntity;
                }
                return;

            case (EffectCard.TrapAp):
                Cel? celTargetAP = RunManager.getCel(indexPosTarget);
                if (celTargetAP != null && celTargetAP.celType == CelType.Cel)
                {
                    celTargetAP.celType = (
                        (effectValue <= 1) ? CelType.Cel_SlimeAPDown :
                        (effectValue == 2) ? CelType.Cel_SlimeAPDown_2 :
                        CelType.Cel_SlimeAPDown_3
                    );
                    celTargetAP.idCharacterWhoApplyCelType = characterLauncher.idEntity;
                }
                return;

            case (EffectCard.HitAround):
                for (int i = 0; i < Vector.adjacente.Length; i++)
                {
                    Vector posAdj = indexPosTarget + Vector.adjacente[i];
                    Character? characterTargetAdj = TurnManager.getCharacterAtIndexPos(posAdj);
                    if (characterTargetAdj != null)
                        characterLauncher.makeDamage(characterTargetAdj, effectValue, refCard);
                }
                return;

            case (EffectCard.InvokeDarunyaNeko):
                if (characterTarget != null)
                    return;
                characterLauncher.invokeACharacter(
                    new CharacterDarunyaNeko(indexPosTarget, characterLauncher, effectValue)
                );
                return;

            case (EffectCard.InvokeLuneAllier):
                if (characterTarget != null)
                    return;
                characterLauncher.invokeACharacter(
                    new CharacterLuneAllier(indexPosTarget, characterLauncher, effectValue)
                );
                return;

            case (EffectCard.SelfKill):
                characterLauncher.death(characterLauncher, refCard);
                return;

            case (EffectCard.Attire):
                if (characterTarget == null)
                    return;
                Vector directionMagnet = characterLauncher.indexPosCel - characterTarget.indexPosCel; // get dif.
                int directionMagnetXAbs = (int)Math.Abs(directionMagnet.x); // take only the axe farest (stay both if diago).
                int directionMagnetYAbs = (int)Math.Abs(directionMagnet.y);
                if (directionMagnetXAbs > directionMagnetYAbs)
                    directionMagnet.y = 0;
                else if (directionMagnetXAbs < directionMagnetYAbs)
                    directionMagnet.x = 0;
                directionMagnet.x = Math.Clamp(directionMagnet.x, -1f, 1f); //c lamp for stay at 1 max.
                directionMagnet.y = Math.Clamp(directionMagnet.y, -1f, 1f);
                for (int i = effectValue - 1; i >= 0; i--) // aply many magnet one.
                {
                    Vector indexMagnet = characterTarget.indexPosCel + directionMagnet;
                    Character? obstacle = TurnManager.getCharacterAtIndexPos(indexMagnet);
                    Cel? celDest = RunManager.getCel(indexMagnet);
                    bool isMagnetOnAnObstacle = (obstacle != null || celDest == null);
                    if (isMagnetOnAnObstacle)
                    {
                        // no damage maked when attire on an obstacle.
                        break;
                    }
                    characterTarget.moveTo(indexMagnet);
                }
                return;

            case (EffectCard.PropagatePoison):
                if (characterTarget == null)
                    return;
                characterTarget.AddStatusEffect(new PropagatePoison(
                    characterTarget.idEntity,
                    characterLauncher.idEntity,
                    2,
                    effectValue
                ));
                return;

            case (EffectCard.TeleportSwitch):
                if (characterTarget != null)
                    characterTarget.moveTo(characterLauncher.indexPosCel);
                characterLauncher.moveTo(indexPosTarget);
                return;

            case (EffectCard.PickCard):
                characterLauncher.deck.piocheManyCard(effectValue);
                if(characterLauncher.isInRedTeam) // update afficahge if a turn of a CharacterPlayer.
                    RunHudLayer.layer.cardHandListCardUi!.setListCard(characterLauncher.deck.cardsInHand);
                return;

            case (EffectCard.SelfHeal):
                characterLauncher.giveHeal(characterLauncher, effectValue, refCard);
                return;
                


            default:
                throw new Exception($"useACard find a EffectCard with no effect {effectCard} !");
        }
    }
}