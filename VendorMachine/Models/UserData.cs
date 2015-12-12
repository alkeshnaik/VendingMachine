﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorMachine.Models
{
    public class UserData
    {
        public int ID { get; set; }
        public int Price { get; set; }
        public string CardNumber { get; set; }
        public string Date { get; set; }
        public string Item { get; set; }
        public string Serial { get; set; }
        public string Site { get; set; }
        public string Time { get; set; }
        public string Status { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}
