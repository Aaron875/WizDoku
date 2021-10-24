using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using TMPro;

public enum MinionType
{
    God,
    Demon
}

public enum ActiveEffect
{
    None,
    Frozen,
    Invulnerable,
    Burned,
}

public class FightManager : MonoBehaviour
{
    public TextMeshPro message;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}

public abstract class CombatEntity //Parent Class
{
    //Fields
    private double health;
    private int attack;
    private bool isAlive;
    private bool entityTurn;
    private ActiveEffect entityEffect;
    private int entityEffectTurns;
    private int disabledTileType;

    //Properties
    public double Health
    {
        get { return health; }
        set { health = value; }
    }

    public int Attack
    {
        get { return attack; }
        set { attack = value; }
    }

    public bool IsAlive
    {
        get { return isAlive; }
        set { isAlive = value; }
    }

    public bool EntityTurn
    {
        get { return entityTurn; }
        set { entityTurn = value; }
    }

    public ActiveEffect EntityEffect //does not stack
    {
        get { return entityEffect; }
        set { entityEffect = value; }
    }

    public int EntityEffectTurns
    {
        get { return entityEffectTurns; }
        set { entityEffectTurns = value; }
    }

    public int DisabledTileType //does not stack
    {
        get { return disabledTileType; }
        set { disabledTileType = value; }
    }

    //Methods
    public void AttackTarget(Fighter target) //can only attack if possible
    {
        if (EntityEffect != ActiveEffect.Frozen && target.EntityEffect != ActiveEffect.Invulnerable)
        {
            target.Health -= Attack;
        }
        if (target.Health <= 0)
        {
            target.IsAlive = false;
        }
    }

    public void AttackTarget(Minion minion) //Can only attack if possible
    {
        if (EntityEffect != ActiveEffect.Frozen && minion.EntityEffect != ActiveEffect.Invulnerable)
        {
            minion.Health -= Attack;
        }
        if (minion.Health <= 0)
        {
            minion.IsAlive = false;
        }
    }

    public void AddAttack(int value)
    {
        Attack += value;
    }

    public void AddHealth(int value)
    {
        Health += value;
    }

    public void ApplyEffect(ActiveEffect effect) //swapping an active effect will only last for one turn.
    {
        EntityEffect = effect;
    }
}

//A class representing the Player and AI fighter
public class Fighter : CombatEntity
{
    //Fields
    //Fighters may have up to 3 minions at a time
    private Minion[] activeMinions = new Minion[3];
    private int numMinions = 0;

    //Properties
    public Minion[] ActiveMinions
    {
        get { return activeMinions; }
        set { activeMinions = value; }
    }

    public int NumMinions
    {
        get { return numMinions; }
        set { numMinions = value; }
    }

    //Constructor
    public Fighter()
    {
        IsAlive = true;
        Health = 150;
        Attack = 5;
        ActiveMinions[0] = null;
        ActiveMinions[1] = null;
        activeMinions[2] = null;
    }

    public void AttemptSummonMinion(MinionType mType)
    {
        if (NumMinions < 3)
        {
            for (int i = 0; i < ActiveMinions.Length; i++)
            {
                if (ActiveMinions[i] == null)
                {
                    ActiveMinions[i] = new Minion(mType);
                    NumMinions++;
                    break;
                }
            }
        }
    }

    //Check to see if any minions have died
    public void CheckMinions(Minion[] minionList, Image[] minionTiles, TMP_Text[] minonStats, Sprite unselected)
    {
        for (int i = 0; i < minionList.Length; i++)
        {
            if (minionList[i] != null && !minionList[i].IsAlive)
            {
                minionTiles[i].sprite = unselected;
                minonStats[i].text = "Health:\nAttack:";
                minionList[i] = null;
                
                numMinions--;
            }
        }
    }
}

//Minions can be summoned through tile placement, and act as buffers between players
public class Minion : CombatEntity
{
    //constructor
    public Minion(MinionType mType)
    {
        IsAlive = true;
        if (mType == MinionType.Demon)
        {
            Health = 40;
            Attack = 5;
        }
        else //must be God type
        {
            Health = 30;
            Attack = 7;
        }
    }
}
