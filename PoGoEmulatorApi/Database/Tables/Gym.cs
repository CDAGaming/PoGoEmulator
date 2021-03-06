﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

// ReSharper disable InconsistentNaming

namespace PoGoEmulatorApi.Database.Tables
{
    [Table("gym")]
    public class Gym
    {
        public int id { get; set; }

        [StringLength(64)]
        public string cell_id { get; set; }

        public double latitude { get; set; }
        public double longitude { get; set; }
        public byte team { get; set; }
        public bool in_battle { get; set; }
        public int points { get; set; }
    }
}