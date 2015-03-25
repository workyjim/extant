//-----------------------------------------------------------------------
// <copyright file="Entity.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using NHibernate.Search.Attributes;

namespace Extant.Data.Entities
{
    public abstract class Entity
    {
        [DocumentId]
        public virtual int Id { get; set; }

        public virtual int Version { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Entity);
        }

        private static bool IsTransient(Entity obj)
        {
            return obj != null && 0 == obj.Id;
        }

        private Type GetUnproxiedType()
        {
            return GetType();
        }

        public virtual bool Equals(Entity other)
        {
            if (other == null)
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (!IsTransient(this) &&
            !IsTransient(other) &&
            Equals(Id, other.Id))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) ||
                otherType.IsAssignableFrom(thisType);
            }
            return false;
        }

        public override int GetHashCode()
        {
            if ( 0 == Id )
                return base.GetHashCode();
            return Id.GetHashCode();
        }
    }
}