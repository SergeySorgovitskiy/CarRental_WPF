using CarRental.MainFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace CarRental.MVVM.Model
{
    public class Car
    {
        public int CarID {  get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string BodyType { get; set; }
        public int Price { get; set; }
        public bool IsAvailable { get; set; }
        public string ImagePath { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
