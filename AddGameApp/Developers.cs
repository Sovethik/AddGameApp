//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AddGameApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class Developers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Developers()
        {
            this.Games = new HashSet<Games>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public int idUser { get; set; }
        public string address { get; set; }
        public System.DateTime dateCreate { get; set; }
    
        public virtual Usesrs Usesrs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Games> Games { get; set; }
    }
}
