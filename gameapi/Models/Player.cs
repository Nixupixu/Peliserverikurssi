using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using gameapi.Models;

namespace gameapi
{
    public class Player
    {
        public Guid _id {get; set;}
        public string _Name {get; set;}
        public int _Level {get;set;}

        public List<Item> _Items = new List<Item>();
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