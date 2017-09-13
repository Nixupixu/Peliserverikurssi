using System;
using System.ComponentModel.DataAnnotations;
using gameapi.Models;

namespace gameapi
{
    public class Player
    {
        public Guid _id {get; set;}
        public string _Name {get; set;}
        public int _Level {get;set;}
        public Item[] _Items {get; set;}

        public Player(Guid id, string Name)
        {
            _id = id;
            _Name = Name;
        }
    }

    public class ModifiedPlayer
    {
        public string _Name {get; set; }
    }

    public class NewPlayer
    {
        [Required]
        public string _Name {get; set;}
    }
}