//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Assignment1
{
    using System;
    using System.Collections.Generic;
    
    public partial class StudentModuleGrade
    {
        public int StudentModuleGradeId { get; set; }
        public int StudentStudentId { get; set; }
        public int ModuleModuleId { get; set; }
        public int StudentGrade { get; set; }
    
        public virtual Student Student { get; set; }
        public virtual Module Module { get; set; }
    }
}
