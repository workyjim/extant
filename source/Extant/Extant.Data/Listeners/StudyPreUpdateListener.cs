//-----------------------------------------------------------------------
// <copyright file="StudyPreUpdateListener.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using Extant.Data.Entities;
using NHibernate.Event;
using NHibernate.Persister.Entity;

namespace Extant.Data.Listeners
{
    public class StudyPreUpdateListener : IPreUpdateEventListener
    {
        public bool OnPreUpdate(PreUpdateEvent @event)
        {
            var study = @event.Entity as Study;
            if (null == study) return false;

            var now = DateTime.Now;
            Set(@event.Persister, @event.State, "StudyUpdated", now);
            study.StudyUpdated = now;
            if (!Equals(Get(@event.Persister, @event.OldState, "ParticipantsRecruited"), Get(@event.Persister, @event.State, "ParticipantsRecruited")))
            {
                Set(@event.Persister, @event.State, "ParticipantsRecruitedUpdated", now);
                study.ParticipantsRecruitedUpdated = now;
            }

            return false;
        }

        private static void Set(IEntityPersister persister, object[] state, string propertyName, object value)
        {
            var index = Array.IndexOf(persister.PropertyNames, propertyName);
            if (index == -1)
                return;
            state[index] = value;
        }

        private static object Get(IEntityPersister persister, object[] state, string propertyName)
        {
            var index = Array.IndexOf(persister.PropertyNames, propertyName);
            if (index == -1)
                return null;
            return state[index];
        }
    }
}