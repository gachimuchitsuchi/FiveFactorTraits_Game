using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement
{
   public int id
    {
        get;
        private set;
    }

    public string name
    {
        get;
        private set;
    }

    public string description
    {
        get;
        private set;
    }

    public bool unlocked
    {
        get;
        set;
    }

    public Achievement(int id, string name, string description)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        unlocked = false;
    }

    public void Unlock()
    {
        unlocked = true;
    }
}
