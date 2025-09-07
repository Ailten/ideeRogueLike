
public enum LogicState
{
    // skip turn.
    skipTurn, //skip turn and reset logic state index.

    // every state.
    chase, //run to the closest oponent.
    fuit, //run in oposit direction of oponent.
    firstHit, //execute the first card in hand who has at least one effect "hit" (and if the target is on the right dist).
    firstCardPlayableOponent, //execute the first card usable on a target oponent (and not usable on it self).
    firstAttire, //execute the first card in hand who has effect "attire" (on target ennemy).

    // or.
    chase_or_firstAttire,
}