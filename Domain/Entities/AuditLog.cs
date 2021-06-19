using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class AuditLog
    {
        [Key]
        public Guid AuditLogId { get; set; }
        public DateTimeOffset EventDateUtc { get; set; }
        public string EventType { get; set; }
        public string TableName { get; set; }
        public string RecordId { get; set; }
        public string ColumnName { get; set; }
        public string OriginalValue { get; set; }
        public string NewValue { get; set; }
        public string Ip { get; set; }
    }
}
