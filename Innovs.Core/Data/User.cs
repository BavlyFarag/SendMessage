using System;
using System.Collections.Generic;

namespace Innovs.Core.Data
{
    public partial class User : BaseEntity
    {
        public User()
        {
            this.MessageLogs = new List<MessageLog>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual ICollection<MessageLog> MessageLogs { get; set; }
    }
}
