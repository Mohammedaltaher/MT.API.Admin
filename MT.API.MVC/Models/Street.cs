//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MT.API.MVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Street
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Street()
        {
            this.Cameras = new HashSet<Camera>();
        }
    
        public int Id { get; set; }
        public int CityID { get; set; }
        public string Name { get; set; }
        public string IsDeleted { get; set; }
        public int Capacity { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Camera> Cameras { get; set; }
        public virtual City City { get; set; }
    }
}
