using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Seniority
{
    public string Level;

    internal Seniority(string level)
    {
        Level = level;
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;

        Seniority other = obj as Seniority;

        if (other == null) return false;

        return this.Level.Equals(other.Level);
    }
}
