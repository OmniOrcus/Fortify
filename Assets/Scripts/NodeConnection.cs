using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeConnection {

    public GameNodeNetworked lower;
    public GameNodeNetworked higher;

    public string name;

    public NodeConnection(GameNodeNetworked one, GameNodeNetworked two)
    {
        string oneS = one.name.Split(' ')[1];
        string twoS = two.name.Split(' ')[1];
        
        if (Evaluate(oneS) > Evaluate(twoS))
        {
            lower = two;
            higher = one;
            name = twoS + "->" + oneS;
        } else
        {
            lower = one;
            higher = two;
            name = oneS + "->" + twoS;
        }

    }

    int Evaluate(string str) {
        string[] nums = str.Split('.');
        return (int.Parse(nums[0]) * 10) + int.Parse(nums[1]);
    }

}
