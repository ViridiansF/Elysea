using UnityEngine;

public class Tech
{
    private string nameTech;
    private bool duplicity;
    private string lockCondition;


    public Tech(string name, string duplicity, string lockCondition)
    {
        nameTech = name;
        this.duplicity = !(duplicity == "");
        this.lockCondition = lockCondition;
    }

    public string getName()
    {
        return nameTech;
    }

    public bool isDuplicable()
    {
        return duplicity;
    }

    public string getLockCondition()
    {
        return lockCondition;
    }

    public override bool Equals(object obj)
    {
        if (obj is Tech other)
        {
            return nameTech == other.nameTech;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return nameTech.GetHashCode();
    }
    
}