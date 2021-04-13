using System;
using System.Collections.Generic;

namespace Innovs.Core.Data
{
    public partial class MessageLog : BaseEntity
    {
        public int Id { get; set; }
        public Nullable<int> MobileInfoId { get; set; }
        public string MessageDetails { get; set; }
        public Nullable<System.DateTime> SendingDate { get; set; }
        public Nullable<bool> IsSend { get; set; }
        public Nullable<int> SendedBy { get; set; }
        public virtual MobileInfo MobileInfo { get; set; }
        public virtual User User { get; set; }
    }
}
