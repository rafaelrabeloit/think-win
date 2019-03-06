using System;
using System.Text;
using System.Collections.Generic;


namespace Think.Core.Domain {
    
    public class RootQuoteReference {
        public virtual int RootQuoteId { get; set; }
        public virtual int RootReferenceId { get; set; }
        #region NHibernate Composite Key Requirements
        public override bool Equals(object obj) {
			if (obj == null) return false;
			var t = obj as RootQuoteReference;
			if (t == null) return false;
			if (RootQuoteId == t.RootQuoteId
			 && RootReferenceId == t.RootReferenceId)
				return true;

			return false;
        }
        public override int GetHashCode() {
			int hash = GetType().GetHashCode();
			hash = (hash * 397) ^ RootQuoteId.GetHashCode();
			hash = (hash * 397) ^ RootReferenceId.GetHashCode();

			return hash;
        }
        #endregion
    }
}
