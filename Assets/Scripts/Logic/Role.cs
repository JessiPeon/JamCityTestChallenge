using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    [System.Serializable]
    public class Role
    {
        public string Name;

        internal Role(string name)
        {
            Name = name;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            Role other = obj as Role;

            if (other == null) return false;

            return this.Name.Equals(other.Name);
        }
    }

}

