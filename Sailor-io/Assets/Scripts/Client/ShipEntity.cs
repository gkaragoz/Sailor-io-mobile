using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Network.Models;
using UnityEngine;

[System.Serializable]
public class ShipEntity : MonoBehaviour {

    // Those variables should be private!
    // But for now to show in inspector, they're public.
    public string _id;                          //Ship's id.
    public string _name;                        //Ship's name.
    public float _health;                       //Ship's maximum health;
    public float _currentHealth;                //Ship's current health;
    public float _movementSpeed;                //Ship's movement speed;
    public float _rotationSpeed;                //Ship's rotation speed;
    public float _slopeSpeed;                   //Ship's slope speed that is rotated on Z.
    public int _maxSuppliesCount;               //Maximum number of supplies that ship can carry.
    public int _currentSuppliesCount;           //Current number of supplies that ship already carry.
    public int _maxSailorsCount;                //Maximum number of players that ship can carry.
    public int _currentSailorsCount;            //Current number of players that ship already carry.
    public int _marketPrice;                    //Ship's price on market that players can buy.
    public int _captainUserId;					//Captains User Id
    public float _supplyCollectorRange;         //Ability of ship's distance between supply and itself to collect supplies.
	public bool isMe;							//True if ship's captain id is same id for user

    public PlayerEntity _captain;               //The captain of the ship.

    public List<PlayerEntity> _playerEntities   //Player's those are currently in the ship.
        = new List<PlayerEntity>();

	public List<PositionEntryModel> _positionBuffer
		= new List<PositionEntryModel>();

	public string Id {
        get { return _id; } 
        set { _id = value; }
    }

    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    public float Health {
        get { return _health; }
        set { _health = value;
            UIManager.instance.SetHealthHUD(_currentHealth, _health);
        }
    }
    public float CurrentHealth {
        get { return _currentHealth; }
        set { _currentHealth = value;
            UIManager.instance.SetHealthHUD(_currentHealth, _health);
        }
    }

    public float MovementSpeed {
        get { return _movementSpeed; }
        set { _movementSpeed = value; }
    }

    public float RotationSpeed {
        get { return _rotationSpeed; }
        set { _rotationSpeed = value; }
    }

    public float SlopeSpeed {
        get { return _slopeSpeed; }
        set { _slopeSpeed = value; }
    }

    public int MaxSuppliesCount {
        get { return _maxSuppliesCount; }
        set { _maxSuppliesCount = value;
            UIManager.instance.SetSupplyHUD(_currentSuppliesCount, _maxSuppliesCount);
        }
    }

    public int CurrentSuppliesCount {
        get { return _currentSuppliesCount; }
        set { _currentSuppliesCount = value;
            UIManager.instance.SetSupplyHUD(_currentSuppliesCount, _maxSuppliesCount);
        }
    }

    public int MaxSailorsCount {
        get { return _maxSailorsCount; }
        set { _maxSailorsCount = value;
            UIManager.instance.SetSailorHUD(_currentSailorsCount, _maxSailorsCount);
        }
    }

    public int CurrentSailorsCount {
        get { return _currentSailorsCount; }
        set { _currentSailorsCount = value;
            UIManager.instance.SetSailorHUD(_currentSailorsCount, _maxSailorsCount);
        }
    }

    public int MarketPrice {
        get { return _marketPrice; }
        set { _marketPrice = value; }
    }
    public int CaptainUserId {
        get { return _captainUserId; }
        set { _captainUserId = value; }
    }

    public float SupplyCollectorRange {
        get { return _supplyCollectorRange; }
        set { _supplyCollectorRange = value; }
    }

    public PlayerEntity Captain {
        get { return _captain; }
        set { _captain = value; }
    }

    public List<PlayerEntity> PlayerEntities {
        get { return _playerEntities; }
        set { _playerEntities = value; }
    }

    public List<PositionEntryModel> PositionEntries {
        get { return _positionBuffer; }
        set { _positionBuffer = value; }
    }

}
