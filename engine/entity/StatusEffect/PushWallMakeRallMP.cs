
public class PushWallMakeRallMP : StatusEffect
{
    private int rallMPByCase;

    public PushWallMakeRallMP(int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1, int rallMPByCase = 1) :
    base(SpriteType.StatusEffect_PushWallMakeRallMP, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.rallMPByCase = rallMPByCase;
    }


    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"poucer un advercaire contre un mure lui retire des points de mouvement.\n" +
            $"{this.rallMPByCase} points de mouvement retire par case de colision.\n" +
            this.getDescriptionTurn()
        );
    }
    public override string getName()
    {
        return $"Imobilisation colision";
    }
    public override bool isAMalus()
    {
        return false;
    }

    
    public override void eventWhenMakeAWallPush(ref int cellBePushed, ref Character characterPushed, ref Character? obstacle, ref Character? characterMakePush, ref PackageRefCard? refCard)
    {
        if (characterMakePush is null)
            return;

        int rallMP = this.rallMPByCase * cellBePushed;
        int idEntityLauncher = this.getCharacterWhoHasEffect?.idEntity ?? -1;

        characterPushed.decreaseMP(rallMP, idCharacterWhoDoDecreaseMP: idEntityLauncher); // do rall to character pushed and obstacle (is has one).
        obstacle?.decreaseMP(rallMP, idCharacterWhoDoDecreaseMP: idEntityLauncher);
    }
}