//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _4_task.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Request
    {
        public int Id_request { get; set; }
        public int Id_tour { get; set; }
        public System.DateTime Data_tour { get; set; }
        public string FIO_client { get; set; }
        public System.DateTime Birth_data { get; set; }
        public string Passport { get; set; }
        public string Inter_Passport { get; set; }
    
        public virtual Tour Tour { get; set; }
    }
}
