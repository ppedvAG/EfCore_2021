using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfMigrations.Model
{
    public class Game
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        //public string Publisher { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Genre { get; set; }

        [NotMapped]
        public int NichtInDB { get; set; }

        public ICollection<DLC> DLCs { get; set; } = new HashSet<DLC>();

        public Company Developer { get; set; }
        public Company Publisher { get; set; }
    }

    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Game> Published { get; set; } = new HashSet<Game>();
        public ICollection<Game> Developed { get; set; } = new HashSet<Game>();

    }
}
