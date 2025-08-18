
public class BoostIntoInvoke : StatusEffect
{

    public BoostIntoInvoke(int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1) :
    base(SpriteType.StatusEffect_BoostIntoInvoke, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    { }


    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"attribue les bonnus de l invocateur a ses invocations.\n" +
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return $"Inocation puissante";
    }
    public override bool isAMalus()
    {
        return false;
    }


    // duplicate eatch status effect for send to the new invoced character.
    public override void eventWhenMakeAnInvoke(ref Character newInvoke)
    {
        // case ref for linq.
        int idInvoke = newInvoke.idEntity;
        Character newInvokeNR = newInvoke;

        // duplicate eatch status effect for send to the new invoced character.
        newInvoke.invokedBy!.statusEffects
            .Select(se =>
            {
                StatusEffectType set = StaticStatusEffectType.getStatusEffectType(se);
                int turnLife = (se.getTurnEnd < 0) ? se.getTurnEnd : se.getTurnEnd - TurnManager.getTurnCount;
                return StaticStatusEffectType.GetStatusEffect(set, idInvoke, se.getCharacterIdWhoApplyEffect, turnLife);
            }).ToList()
            .ForEach(se => newInvokeNR.AddStatusEffect(se));
    }
}