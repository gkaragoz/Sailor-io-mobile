using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerEntity : MonoBehaviour {

    // Those variables should be private!
    // But for now to show in inspector, they're public.
    public string _id;                      //Player's id.
    public string _username;                //Player's username which he/she logged in with this.
    public bool _isMe;                      //Is this client a current user checks.
    public int _gold;                       //Player's gold count that he/she can spend for something on market.
    public float _movementSpeed;            //Player's movement speed.
    public float _attackDamage;             //Player's attack damage.
    public float _attackRate;               //Player's attack rate. (seconds)
    public Raft _raft;                      //Player's that has current ship/raft.
    public Enums.SailorType _sailorType;    //Is the player captain of his/her current ship/raft.
    public float _supplyRate;               //Rate depends on player attacked as percentage on distrubutions for total number of supplies in the player's ship/raft collected.

    public string Id {
        get { return _id; }
        set { _id = value; }
    }

    public string Username {
        get { return _username; }
        set { _id = value; }
    }

    public bool IsMe {
        get { return _isMe; }
        set { _isMe = value; }
    }

    public int Gold {
        get { return _gold; }
        set { _gold = value; }
    }

    public float MovementSpeed {
        get { return _movementSpeed; }
        set { _movementSpeed = value; }
    }

    public float AttackDamage {
        get { return _attackDamage; }
        set { _attackDamage = value; }
    }

    public float AttackRate {
        get { return _attackRate; }
        set { _attackRate = value; }
    }

    public Raft Raft {
        get { return _raft; }
        set { _raft = value; }
    }

    public Enums.SailorType SailorType {
        get { return _sailorType; }
        set { _sailorType = value; }
    }

    public float SupplyRate {
        get { return _supplyRate; }
        set { _supplyRate = value; }
    } 

    //Player's Name UI.
    //Player's Texts UIs (Action Bar).
    //Player's Costumes (Skins).
    //Player's Social Media accounts.
}
