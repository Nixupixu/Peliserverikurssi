using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace gameapi.Models
{
    public class Item
    {
        [Required]
        public string _Name {get; set;}
        [Range(1,70)]
        public int _Level;
        public int _Price;  
        public DateTime _CreationDate;

        public Item(string name, int level, int price, DateTime date)
        {
            _Name = name;
            _Level = level;
            _Price = price;
            _CreationDate = date;
        }
    }

    public class NewItem
    {
        public string _Name {get; set;}
        public int _Level {get; set;}
    }
}