﻿using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goalsetter.Domains
{
    public class AuditedEntity : Entity<Guid>
    {
        public virtual DateTime CreatedDate { get; protected set; }
        public virtual DateTime UpdatedDate { get; protected set; }
        public virtual bool IsActive { get; protected set; }
        protected AuditedEntity()
        {
        }
        protected AuditedEntity(Guid id)
            : base(id)
        {
        }
        
    }
}
