
// a parent object for effect on a character (edit process of the caractere during battle).
public class StatusEffect
{
    private int turnStart;
    private int turnEnd;
    private int characterIdWhoApplyEffect;
    private int characterIdWhoHasEffect;
    private SpriteType spriteType;
    private int idEffect;
    private static int idEffectCount = 0;

    public int getTurnEnd
    {
        get { return turnEnd; }
    }
    public int getTurnUntilEnd
    {
        get { return turnEnd - TurnManager.getTurnCount; }
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
    protected Character getCharacterWhoHasEffect
    {
        get { return TurnManager.getAllCharacters().Find(c => c.idEntity == this.characterIdWhoHasEffect) ?? throw new Exception("CharacterHasApplyEffect has not found in TurnManager !"); }
    }

    public SpriteType GetSpriteType
    {
        get { return spriteType; }
    }

    public int getIdEffect
    {
        get { return this.idEffect; }
    }


    public StatusEffect(SpriteType spriteType, int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1)
    {
        this.spriteType = spriteType;

        this.turnStart = TurnManager.getTurnCount;
        this.turnEnd = (turnLife == -1) ? turnLife : this.turnStart + turnLife;

        this.characterIdWhoHasEffect = characterIdWhoHasEffect;
        this.characterIdWhoApplyEffect = characterIdWhoApplyEffect;

        this.idEffect = idEffectCount++;
    }

    // description of effect.
    protected virtual string getDescription()
    {
        throw new Exception("StatusEffect has no description overided !");
    }


    // event call when effect end life.
    public virtual void eventWhenStatusEffectDisapear(
        bool isEndLifeEffect = false,
        bool isEndOfFight = false,
        bool isCharacterWhoHasEffectDie = false,
        bool isCharacterWhoApplyEffectDie = false
    )
    { }

    // event call when target start turn.
    public void eventWhenTargetStartTurn(){}
    // event call when target end turn.
    public virtual void eventWhenTargetEndTurn(){}


}