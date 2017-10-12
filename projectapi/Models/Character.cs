using System;

namespace projectapi.Models
{
    public class Character
    {
        public Guid _CharId {get; set;}

        public string _Name {get; set;}
        
        public int _Strength {get; set;}
        public int _Agility {get; set;}
        public int _Intelligence {get; set;}
    }

    public class ModifiedCharacter
    {
        public string _Name {get; set;}
    }

    public class NewCharacter
    {
        public string _Name {get; set;}

        public int _Strength {get; set;}
        public int _Agility {get; set;}
        public int _Intelligence {get; set;}
    }
}