using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class Contact
    {
        public Contact()
        {
            this.ContactItemValues = new List<ContactItemValue>();
        }

        public int Id { get; set; }
        public int CompanyNo { get; set; }
        public string Name { get; set; }
        public string FirstLetterName { get; set; }
        public int CustomerId { get; set; }
        public string DptName { get; set; }
        public string JobTitle { get; set; }
        public int ImportantDegreeValueId { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string WeChatId { get; set; }
        public string QQ { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }
        public string Description { get; set; }
        public int Sex { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public bool UseChineseCalendar { get; set; }
        public int DecisionRelationshipValueId { get; set; }
        public int DptId { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreateOnUtc { get; set; }
        public System.Guid CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateOnUtc { get; set; }
        public Nullable<System.Guid> UpdateBy { get; set; }
        public virtual ICollection<ContactItemValue> ContactItemValues { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Department Department { get; set; }
        public virtual ItemValue ItemValue { get; set; }
        public virtual ItemValue ItemValue1 { get; set; }
    }
}
