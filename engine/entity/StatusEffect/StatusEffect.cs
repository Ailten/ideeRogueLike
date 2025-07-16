
// a parent object for effect on a character (edit process of the caractere during battle).
public class StatusEffect
{
    private int turnStart;
    private int turnEnd;
    private int characterIdWhoApplyEffect;
    private int characterIdWhoHasEffect;

    public int getTurnEnd
    {
        get { return turnEnd; }
    }
    public int getCharacterIdWhoApplyEffect
    {
        get { return characterIdWhoApplyEffect; }
    }
    public int getCharacterIdWhoHasEffect
    {
        get { return characterIdWhoHasEffect; }
    }

    protected Character? getCharacterWhoApplyEffect
    {
        get { return TurnManager.getAllCharacters().FirstOrDefault(c => c.idEntity == this.characterIdWhoApplyEffect); }
    }
    protected Character getCharacterHasEffect
    {
        get { return TurnManager.getAllCharacters().Find(c => c.idEntity == this.characterIdWhoHasEffect) ?? throw new Exception("CharacterHasApplyEffect has not found in TurnManager !"); }
    }


    public StatusEffect(int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1)
    {
        this.turnStart = TurnManager.getTurnCount;
        this.turnEnd = (turnLife == -1) ? turnLife : this.turnStart + turnLife;

        this.characterIdWhoHasEffect = characterIdWhoHasEffect;
        this.characterIdWhoApplyEffect = characterIdWhoApplyEffect;
    }


    // event call when effect end life.
    public void eventWhenStatusEffectDisapear(
        bool isEndLifeEffect = false, //implemented.
        bool isEndOfFight = false, //implemented.
        bool isCharacterWhoHasEffectDie = false,
        bool isCharacterWhoApplyEffectDie = false
    ){}

    // event call when target start turn.
    public void eventWhenTargetStartTurn(){}
    // event call when target end turn.
    public virtual void eventWhenTargetEndTurn(){}


}