using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Friends.Models
{
    public class FriendModel
    {
        [Required]
        [Range(0, 200)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Mesto na ziveenje")]
        public string MestoNaZiveenje { get; set; }

        public FriendModel(int id, string name, string mesto)
        {
            Id = id;
            Name = name;
            MestoNaZiveenje = mesto;
        }
        public FriendModel()
        {

        }
    }
}