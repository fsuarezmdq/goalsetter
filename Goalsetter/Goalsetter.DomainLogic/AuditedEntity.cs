﻿using CSharpFunctionalExtensions;
using System;

namespace Goalsetter.Domains
{
    public class AuditedEntity : Entity<Guid>
    {
        public virtual DateTime CreatedDate { get; protected set; }
        public virtual DateTime UpdatedDate { get; protected set; }
        public virtual bool IsActive { get; protected set; }
        protected static DateTime DateTimeNow => DateTime.UtcNow;


        protected AuditedEntity()
        {
        }
        protected AuditedEntity(Guid id)
            : base(id)
        {
        }
        protected static Guid GetId(Guid id)
        {
            return (id == default) ? Guid.NewGuid() : id;
        }

    }
}
