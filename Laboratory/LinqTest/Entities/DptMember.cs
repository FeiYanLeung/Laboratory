using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory.LinqTest.Entities
{
    public class DptMember : BaseEntity
    {
        public int DptId { get; set; }
        public string DptName { get; set; }
        public Guid EmpId { get; set; }
    }
}
