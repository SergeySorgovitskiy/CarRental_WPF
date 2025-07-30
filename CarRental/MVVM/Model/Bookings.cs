using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace CarRental.MVVM.Model
{
    public class Booking 
    {
        public int BookingID {  get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public int CarID { get; set; }
        public Car Car { get; set; }
    }
}
