//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarsStok.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RentalHistory
    {
        public int Id { get; set; }
        public string Client_name { get; set; }
        public int Cars_id { get; set; }
        public string Cars_model { get; set; }
        public Nullable<int> Rental_period { get; set; }
    }
}