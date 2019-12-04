using Able.Store.Infrastructure.Domain.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Able.Store.Infrastructure.Domain
{
    public abstract class EntityBase<TId>:IEntityBase<TId>
    {
        private List<BusinessRule> _brokenRules = new List<BusinessRule>();
        public abstract TId Id { get; set; }
        public abstract DateTime? CreateTime { get; set; }
        protected abstract void Validate();


        public string GetBrokenRuleMessage()
        {

            StringBuilder stb = new StringBuilder();
            foreach (var item in this._brokenRules)
            {
                stb.AppendLine(item.Rule);
            }

            return stb.ToString();

        }
        public IEnumerable<BusinessRule> GetBrokenRules()
        {
            Validate();
            return _brokenRules;
        }
        protected void AddBrokenRule(BusinessRule brokenrule)
        {
            // IsBroken = true;
            _brokenRules.Add(brokenrule);
        }

        public bool IsBroker()
        {
            return this._brokenRules.Count > 0;
        }
        public override bool Equals(object entity)
        {
            return entity != null &&
                entity is EntityBase<TId> &&
                this == (EntityBase<TId>)entity;
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        public static bool operator ==(EntityBase<TId> entity1, EntityBase<TId> entity2)
        {

            if ((object)entity1 == null && (object)entity2 == null)
                return true;

            if ((object)entity1 == null || (object)entity2 == null)
                return false;

            if (entity1.Id.ToString() == entity2.Id.ToString())
                return true;

            return false;
        }
        public static bool operator !=(EntityBase<TId> entity1, EntityBase<TId> entity2)
        {
            return (!(entity1 == entity2));
        }
    }
}
