
public class SideEyes : StatusEffect
{
    public SideEyes(int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1) :
    base(SpriteType.StatusEffect_SideEyes, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
    }


    private StatusEffect? getEffectAtRight()
    {
        for (int i = 0; i < this.getCharacterWhoHasEffect.statusEffects.Count; i++)
        {
            if (this.getIdEffect == this.getCharacterWhoHasEffect.statusEffects[i].getIdEffect) // match id.
            {
                int indexAtRight = i + 1;

                if (indexAtRight >= this.getCharacterWhoHasEffect.statusEffects.Count - 2)
                    return null;

                return this.getCharacterWhoHasEffect.statusEffects[indexAtRight];
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
    // event call when make a wall pushed.
    public override void eventWhenMakeAWallPush(ref int cellBePushed, ref Character? obstacle, ref Character? characterMakePush, ref PackageRefCard? refCard)
    {
        this.eventWhenMakeAWallPush(ref cellBePushed, ref obstacle, ref characterMakePush, ref refCard);
    }
    // event call when take a wall pushed.
    public override void eventWhenTakeAWallPush(ref int cellBePushed, ref Character? obstacle, ref Character? characterMakePush, ref PackageRefCard? refCard)
    {
        this.eventWhenTakeAWallPush(ref cellBePushed, ref obstacle, ref characterMakePush, ref refCard);
    }


}