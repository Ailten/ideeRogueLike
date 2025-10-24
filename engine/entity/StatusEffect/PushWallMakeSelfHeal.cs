
public class PushWallMakeSelfHeal : StatusEffect
{
    private int heal;

    public PushWallMakeSelfHeal(int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1, int heal = 1) :
    base(SpriteType.StatusEffect_PushWallMakeSelfHeal, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.heal = heal;
    }


    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"poucer un advercaire contre un mure soigne le lanceur.\n" +
            $"soigne {this.heal} point de vie au lanceur par case.\n" +
            this.getDescriptionTurn()
        );
    }
    public override string getName()
    {
        return $"Soin par impacte";
    }
    public override bool isAMalus()
    {
        return this.heal < 0;
    }


    public override void eventWhenMakeAWallPush(ref int cellBePushed, ref Character characterPushed, ref Character? obstacle, ref Character? characterMakePush, ref PackageRefCard? refCard)
    {
        if (characterMakePush is null)
            return;

        characterMakePush.giveHeal(characterMakePush, cellBePushed, refCard); // heal.
    }
}