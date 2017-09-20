using System;
using System.ComponentModel.DataAnnotations;
using gameapi.ModelValidation;

namespace gameapi.Models
{
    public class Item
    {
        public Guid _ItemId {get; set;}
        public string _Name {get; set;}

        public string _Type {get; set;}

        [Range(1,70)]
        public int _Level {get; set;}
        public int _Price {get; set;}       
        public DateTime _CreationDate {get; set;}
    }

    public class ModifiedItem
    {
        [Required]
        public string _Name {get; set;}
    }

    public class NewItem
    {
        [Required]
        public string _Name {get; set;}
        [Required]
        public string _Type {get; set;}

        [MinimumItemLevel]
        [Range(1,70)]
        public int _Level {get; set;}
        public int? _Price {get; set;}       
    }
}