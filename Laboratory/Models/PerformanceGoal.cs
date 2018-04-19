using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class PerformanceGoal
    {
        public int Id { get; set; }
        public int CompanyNo { get; set; }
        public System.Guid UserGuid { get; set; }
        public System.Guid EmpGuid { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public Nullable<decimal> ContractGoals { get; set; }
        public Nullable<decimal> PaymentGoals { get; set; }
        public int Status { get; set; }
        public System.DateTime CreateOnUtc { get; set; }
        public System.Guid CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateOnUtc { get; set; }
        public Nullable<System.Guid> UpdateBy { get; set; }
    }
}
