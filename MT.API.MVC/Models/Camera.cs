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
    
    public partial class Camera
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Camera()
        {
            this.CameraRequests = new HashSet<CameraRequest>();
        }
    
        public int Id { get; set; }
        public int StreetID { get; set; }
        public string IpAddress { get; set; }
        public string IsDeleted { get; set; }
        public string Diriction { get; set; }
        public Nullable<float> Latitude { get; set; }
        public Nullable<float> Longitude { get; set; }
        public Nullable<bool> IsInStreetBegaining { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CameraRequest> CameraRequests { get; set; }
        public virtual Street Street { get; set; }
    }
}
