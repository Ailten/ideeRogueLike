
public class SideEyes : StatusEffect
{
    public SideEyes(int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1) :
    base(SpriteType.StatusEffect_SideEyes, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
    }


    private StatusEffect? getEffectAtRight()
    {
        Character? characterWhoHas = this.getCharacterWhoHasEffect;
        if (characterWhoHas is null)
            return null;

        for (int i = 0; i < characterWhoHas.statusEffects.Count; i++)
        {
            if (this.getIdEffect == characterWhoHas.statusEffects[i].getIdEffect) // match id.
            {
                int indexAtRight = i + 1;

                if (indexAtRight >= characterWhoHas.statusEffects.Count - 2)
                    return null;

                return characterWhoHas.statusEffects[indexAtRight];
            }
        }

        return null;
    }


    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"copie l'effet de l'etat de droite.\n" +
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return $"Copieur";
    }
    public override bool isAMalus()
    {
        StatusEffect? se = this.getEffectAtRight();
        return (se is null) ? false : se.isAMalus();
    }


    // event call when effect end life.
    public override void eventWhenStatusEffectDisapear(
        bool isEndLifeEffect = false,
        bool isEndOfFight = false,
        bool isCharacterWhoHasEffectDie = false,
        bool isCharacterWhoApplyEffectDie = false,
        bool isDestroyByAction = false
    )
    {
        this.getEffectAtRight()?.eventWhenStatusEffectDisapear(
            isEndLifeEffect,
            isEndOfFight,
            isCharacterWhoHasEffectDie,
            isCharacterWhoApplyEffectDie,
            isDestroyByAction
        );
    }

    // event call when target start turn.
    public override void eventWhenTargetStartTurn()
    {
        this.getEffectAtRight()?.eventWhenTargetStartTurn();
    }
    // event call when target end turn.
    public override void eventWhenTargetEndTurn()
    {
        this.getEffectAtRight()?.eventWhenTargetEndTurn();
    }
    // event call when target make damage.
    public override void eventWhenTargetMakeDamage(ref Character target, ref int atk, ref PackageRefCard? refCard)
    {
        this.getEffectAtRight()?.eventWhenTargetMakeDamage(ref target, ref atk, ref refCard);
    }
    // event call when target take damage.
    public override void eventWhenTargetTakeDamage(ref int atk, ref Character? characterMakeAtk, ref PackageRefCard? refCard)
    {
        this.getEffectAtRight()?.eventWhenTargetTakeDamage(ref atk, ref characterMakeAtk, ref refCard);
    }
    // event call when a fight end.
    public override void eventWhenPlayerWinFight()
    {
        this.getEffectAtRight()?.eventWhenPlayerWinFight();
    }
    // event call when make an invoke.
    public override void eventWhenMakeAnInvoke(ref Character newInvoke)
    {
        this.getEffectAtRight()?.eventWhenMakeAnInvoke(ref newInvoke);
    }
    // event call when use a card.
    public override void eventWhenUseACard(ref PackageRefCard packageRefCard)
    {
        this.getEffectAtRight()?.eventWhenUseACard(ref packageRefCard);
    }
    // event call when a card broke.
    public override void eventWhenCardBroke(ref PackageRefCard packageRefCard)
    {
        this.getEffectAtRight()?.eventWhenCardBroke(ref packageRefCard);
    }
    // event call when apply decrease MP to a target.
    public override void eventWhenDecreaseMPOfATarget(ref int decrease, ref Character whoTakeDecreaseMP)
    {
        this.getEffectAtRight()?.eventWhenDecreaseMPOfATarget(ref decrease, ref whoTakeDecreaseMP);
    }
    // event call when make a wall pushed.
    public override void eventWhenMakeAWallPush(ref int cellBePushed, ref Character characterPushed, ref Character? obstacle, ref Character? characterMakePush, ref PackageRefCard? refCard)
    {
        this.getEffectAtRight()?.eventWhenMakeAWallPush(ref cellBePushed, ref characterPushed, ref obstacle, ref characterMakePush, ref refCard);
    }
    // event call when take a wall pushed.
    public override void eventWhenTakeAWallPush(ref int cellBePushed, ref Character? obstacle, ref Character? characterMakePush, ref PackageRefCard? refCard)
    {
        this.getEffectAtRight()?.eventWhenTakeAWallPush(ref cellBePushed, ref obstacle, ref characterMakePush, ref refCard);
    }
    // event call when make a heal.
    public override void eventWhenMakeAHeal(ref Character target, ref int healIncrement, ref PackageRefCard? refCard)
    {
        this.getEffectAtRight()?.eventWhenMakeAHeal(ref target, ref healIncrement, ref refCard);
    }
    // event call when take a heal.
    public override void eventWhenTakeAHeal(ref int healIncrement, ref Character? characterGiveHeal, ref PackageRefCard? refCard)
    {
        this.getEffectAtRight()?.eventWhenTakeAHeal(ref healIncrement, ref characterGiveHeal, ref refCard);
    }
    // event call when make a shild.
    public override void eventWhenGiveAShild(ref Character target, ref int shildIncrement, ref PackageRefCard? refCard)
    {
        this.getEffectAtRight()?.eventWhenGiveAShild(ref target, ref shildIncrement, ref refCard);
    }
    // event call when take a shild.
    public override void eventWhenTakeAShild(ref int shildIncrement, ref Character? characterGiveShild, ref PackageRefCard? refCard)
    {
        this.getEffectAtRight()?.eventWhenTakeAShild(ref shildIncrement, ref characterGiveShild, ref refCard);
    }



}