//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class TB_EMPRESA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TB_EMPRESA()
        {
            this.TB_VAGA = new HashSet<TB_VAGA>();
        }
    
        public int id { get; set; }
        public string nome { get; set; }
        public string telefone { get; set; }
        public int id_setor { get; set; }
    
        public virtual TB_SETOR TB_SETOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_VAGA> TB_VAGA { get; set; }
    }
}
