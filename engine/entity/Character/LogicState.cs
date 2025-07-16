
public enum LogicState
{

    skipTurn, //skip turn and reset logic state index.

    chase, //run to the closest oponent.
    firstHit, //execute the first card in hand who has at least one effect "hit" (and if the target is on the right dist).
    firstCardPlayableOponent, //execute the first card usable on a target oponent (and not usable on it self).

}