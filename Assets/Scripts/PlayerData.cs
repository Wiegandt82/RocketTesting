using System;
using System.Numerics;

[Serializable]                 //This + [SerializeField] in game manager will allow to see it in inspector 
public class PlayerData
{
    public int Energy;
    public int Lives = 3;
    public float Fuel = 200;
    
    
    //reset method for new game from main menu
    public void Reset()
    {
        Energy = 0;
        Lives = 3;
        Fuel = 200;
    }
}
