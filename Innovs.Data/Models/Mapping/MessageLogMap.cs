using Innovs.Core.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Innovs.Data.Models.Mapping
{
    public class MessageLogMap : EntityTypeConfiguration<MessageLog>
    {
        public MessageLogMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("MessageLog");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.MobileInfoId).HasColumnName("MobileInfoId");
            this.Property(t => t.MessageDetails).HasColumnName("MessageDetails");
            this.Property(t => t.SendingDate).HasColumnName("SendingDate");
            this.Property(t => t.IsSend).HasColumnName("IsSend");
            this.Property(t => t.SendedBy).HasColumnName("SendedBy");

            // Relationships
            this.HasOptional(t => t.MobileInfo)
                .WithMany(t => t.MessageLogs)
                .HasForeignKey(d => d.MobileInfoId);
            this.HasOptional(t => t.User)
                .WithMany(t => t.MessageLogs)
                .HasForeignKey(d => d.SendedBy);

        }
    }
}
