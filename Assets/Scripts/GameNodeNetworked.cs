using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameNodeNetworked : NetworkBehaviour, Observable{

    static double flankMultiplier = 0.05; //Bonus percentage to attack force per extra flank
    static double baseSurvival = 0.8; //Percentage of forces that survive a lost fight, before the attackers strength is considered.
    static double survivorProportion = 0.2; //NOTE - MIGHT BE DEFUNCT. Percentage of the surviving retrieved by each neighbouring node. NOTE: if higher than 0.2, then total refugees while exceed combat survivors.
    static double combatLoseRate = 0.6; //The percentage of the defending forces that kill attackers a capture.
    static double reinforceRate = 0.5; //Percentage of reinforcement muster added to target
    static double skrimishEffectiveness = 0.8; //The percentage of the attacking force removed from the garrison in a skirmish.

    //For triggering shake effect on nodes. No longer used.
    NodeShaker shaker;

    public GameNodeNetworked[] connentions;

    [SyncVar(hook = "SetOwner")]
    public uint owner = 0;
    [SyncVar(hook = "SetStrength")]
    public int strength = 0;

    bool refuges = true;


    //Observer Pattern - Wish Multi inheritance was a thing.;
    //--> Probably should have just made an 'Observable' abstract class extending MetworkBehaviour and extended down from that. 
    List<Observer> observers = new List<Observer>();
    public void AddObserver(Observer observer)
    {
        observers.Add(observer);
    }
    public void InformObservers()
    {
        foreach (Observer observer in observers)
        {
            observer.Look();
        }
    }
    //~End Observer Implementations



    // Use this for initialization
    void Start () {
        Debug.Log(name + "Started.");
        shaker = GetComponent<NodeShaker>();
        if (shaker == null) Debug.LogError("Node shaker missing on " + name);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    override public void OnStartAuthority() {
        refuges = true;
        InformObservers();
    }

    override public void OnStopAuthority()
    {
        refuges = true;
        InformObservers();
    }

    //Tooltip
    void OnMouseEnter()
    {
        if (owner != Player.Access().playerNumber)
        {
            //Calculate Attack Strength
            int power = 0;
            int flank = 0;
            foreach (GameNodeNetworked connection in connentions)
            {
                if (connection.owner == Player.Access().playerNumber)
                {
                    flank++;
                    power += connection.strength;
                }
            }
            if (flank != 0)
            {
                if (owner == 0)
                    AttDisplay.Access().SetDisplay((power));
                else
                    AttDisplay.Access().SetDisplay((int)(power * Flank(flank)));
            }
        } else if (owner == Player.Access().playerNumber)
        {
            //Calculate Reinforcement
            int power = 0;
            foreach (GameNodeNetworked connection in connentions)
            {

                if (connection.owner == Player.Access().playerNumber)
                {
                    power += connection.strength;
                }
            }
            AttDisplay.Access().SetDisplay(strength + ((int)(power * 0.5)));
        }
    }

    void OnMouseExit() {
        AttDisplay.Access().display.text = "";
    }

    //Controls
    void OnMouseDown() {
        Debug.Log("Click Test");
        if(hasAuthority)
            CmdTakeNode(Player.Access().GetPlayerNumber());
        OnMouseEnter();
    }

    [Command]
    public void CmdTakeNode(uint player) {
        if (Gamemaster.Access().inPlay)
        {
            //Self Capture Check;
            if (owner != player)
            {

                Debug.Log("Player " + player + " Attempts to take " + gameObject.name);

                //Muster Strength;
                int power = 0;
                int flank = 0;
                foreach (GameNodeNetworked connection in connentions)
                {
                    if (connection.owner == player)
                    {
                        flank++;
                        power += connection.strength;
                    }
                }

                if (flank > 0)
                {
                    //Fight Started
                    RpcShake();
                    //Resolve Fight - Flank multiplier
                    if (power * Flank(flank) > strength) //Capture successful
                    {
                        //Calculate Retreat Routes
                        int routes = 0;
                        foreach (GameNodeNetworked connection in connentions)
                        {
                            if (connection.owner == owner)
                            {
                                routes++;
                            }
                        }

                        //Kills caused by the battle
                        double survivalRate = (strength / (power * Flank(flank)));

                        //RetreatForce
                        if (routes > 0)
                        {
                            //Surviving forces
                            int retreatForce = (int)((strength * baseSurvival) * survivalRate);
                            float proportionPerPath = 1 / (connentions.Length - 1);
                            refuges = false;
                            foreach (GameNodeNetworked connection in connentions)
                            {
                                if (connection.owner == owner)
                                {
                                    connection.RecieveRetreates((int)(retreatForce * survivorProportion));
                                }
                            }
                        }

                        //Combat Loses - 60% victory losses
                        //strength = (int)(((power * Flank(flank)) - (strength * combatLoseRate)) / Flank(flank));

                        //New Combat loss model - based on the 'survival rate'calculation earlier due to close battles, combined with the previous base rate ~ 24/09/2018
                        strength = power - (int)(strength * survivalRate * combatLoseRate);

                        if (owner == 0)
                        {
                            //Que march sound
                            RpcPlaySound(4);
                        }
                        else
                        {
                            //Que Capture sound
                            RpcCaptureSound(player);
                        }
                        owner = player;
                        Debug.Log("Capture Successful. New Strength: " + strength);

                        //Check for victory
                        if (Gamemaster.Access().Wincheck())
                        {
                            Gamemaster.Access().inPlay = false;
                            RpcObserverInform();
                            RpcWin(owner);
                            return;
                        }

                    }
                    else //skirmishing calculations
                    {
                        Debug.Log("Player " + player + " damages  " + gameObject.name);

                        //Losses caused by attack
                        strength -= (int)(power * Flank(flank) * skrimishEffectiveness);
                        if (strength <= 0)
                        {
                            //returned to neutrality
                            owner = 0;
                            //negative handler.
                            strength = 0;
                        }
                        //Que Battle Sound Effect
                        RpcPlaySound(2);
                    }
                    RpcObserverInform();
                    Gamemaster.Access().NextTurn();
                }
            }
            else
            {
                //Reinforce
                Reinforce();
                RpcPlaySound(3);
            }
        }
    }

    [ClientRpc]
    public void RpcObserverInform() {
        InformObservers();
    }

    [ClientRpc]
    public void RpcShake()
    {
        shaker.Trigger();
    }

    [ClientRpc]
    public void RpcWin(uint player)
    {
        EndGameManager.Access().gameObject.SetActive(true);
        if (player == Player.Access().playerNumber)
        {
            Debug.Log("You Win");
            EndGameManager.Access().VictoryText();
        }
        else
        {
            Debug.Log("You Lose");
            EndGameManager.Access().LossText(player);
        }
    }

    //Client Side Sound Effect Triggers
    [ClientRpc]
    public void RpcPlaySound(uint id)
    {
        SoundSystem.Access().PlayClip(id);
    }

    [ClientRpc]
    public void RpcCaptureSound(uint Victor)
    {
        if (Player.Access().playerNumber == Victor)
        {
            SoundSystem.Access().PlayClip(0);
        } else if(Player.Access().playerNumber == owner)
        {
            SoundSystem.Access().PlayClip(1);
        }
        else
        {
            SoundSystem.Access().PlayClip(2);
        }

    }

    //Recursive Retreat System
    public void RecieveRetreates(int ret)
    {
        strength += ret;
        Debug.Log(name + " recieving " + ret + "refugees");
        if (refuges)
        {
            refuges = false;

            foreach (GameNodeNetworked connection in connentions)
            {
                
                if (connection.owner == owner && connection.refuges)
                {
                    connection.RecieveRetreates((int)(ret * 0.6));
                    //strength -= (int)(ret * 0.1) + 1;
                }
            }
        }
    }

    public void SetOwner(uint _owner) {
        owner = _owner;
        InformObservers();
    }

    public void SetStrength(int _strength)
    {
        strength = _strength;
        InformObservers();
    }

    public int GetStrength()
    {
        return strength;
    }

    public uint GetOwner()
    {
        return owner;
    }

    //Node Mechanic Functions!!!!

    double Flank(int attackVectors) {
        return (1 + (flankMultiplier * (attackVectors - 1)));
        }

    void Reinforce()
    {
        int power = 0;
        //reinforcing owned node
        foreach (GameNodeNetworked connection in connentions)
        {
            
            if (connection.owner == owner)
            {
                power += connection.strength;   
            }
        }
        if (power < 0)
        {
            //Lazy integer overflow handling
            power = int.MaxValue;
        }
        power -= (power * ((power * 2) / int.MaxValue));
        strength += (int)(power * reinforceRate);
        Gamemaster.Access().NextTurn();
    }
}
