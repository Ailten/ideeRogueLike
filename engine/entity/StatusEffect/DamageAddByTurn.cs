
public class DamageAddByTurn : StatusEffect
{
    private CardColor color;
    private int damageBoostIncrease;
    private int eatchTurn;
    private int damageBoost = 0;


    public DamageAddByTurn(int characterIdWhoHasEffect, int characterIdWhoApplyEffect, int turnLife, CardColor color, int damageBoostIncrease = 1, int eatchTurn = 3) :
    base(SpriteType.StatusEffect_DamageAddBoostRed, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.color = color;
        this.damageBoostIncrease = damageBoostIncrease;
        this.eatchTurn = eatchTurn;

        this.spriteType = ( // overide sprite.
            (color == CardColor.Red) ? SpriteType.StatusEffect_DamageAddBoostRed :
            (color == CardColor.Blue) ? SpriteType.StatusEffect_DamageAddBoostBlue :
            (color == CardColor.Green) ? SpriteType.StatusEffect_DamageAddBoostGreen :
            throw new Exception("DamageAddBoostColor can't be assigne with this color !")
        );
    }


    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"ogmente les degats des cartes {this.color.getName()} de {this.damageBoost}.\n" +
            $"augmente de {this.damageBoostIncrease} tout les {this.eatchTurn} tours.\n" +
            "revient a 0 a la fin du combat.\n" +
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return $"Domage {(this.isAMalus() ? "moin" : "plus")} {this.color.getName()}";
    }
    public override bool isAMalus()
    {
        return this.damageBoost < 0;
    }


    // increase damage when make damage.
    public override void eventWhenTargetMakeDamage(ref Character target, ref int atk, ref PackageRefCard? refCard)
    {
        if (refCard is null) // skip if damage is not maked by a card.
            return;

        CardColor colorOfCardUsed = refCard!.getCard().cardColor;
        if (colorOfCardUsed.isMatchingColor(this.color))
        {
            atk += this.damageBoost; // increase atk by sending reference.
        }
    }

    // increase damage boost eatch N turn.
    public override void eventWhenTargetStartTurn()
    {
        int turnFromEffectWasApply = this.getTurnFromApply;
        if (turnFromEffectWasApply != 0 && turnFromEffectWasApply % this.eatchTurn == 0)
        {
            this.damageBoost += this.damageBoostIncrease; // increase boost.
        }
    }

    // decreate to zero, when end a fight.
    public override void eventWhenPlayerWinFight()
    {
        this.damageBoost = 0;
    }



}