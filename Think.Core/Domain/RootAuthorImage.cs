using System;
using System.Text;
using System.Collections.Generic;


namespace Think.Core.Domain {
    
    public class RootAuthorImage {
        public virtual int RootAuthorId { get; set; }
        public virtual int ImageId { get; set; }
        #region NHibernate Composite Key Requirements
        public override bool Equals(object obj) {
			if (obj == null) return false;
			var t = obj as RootAuthorImage;
			if (t == null) return false;
			if (RootAuthorId == t.RootAuthorId
			 && ImageId == t.ImageId)
				return true;

			return false;
        }
        public override int GetHashCode() {
			int hash = GetType().GetHashCode();
			hash = (hash * 397) ^ RootAuthorId.GetHashCode();
			hash = (hash * 397) ^ ImageId.GetHashCode();

			return hash;
        }
        #endregion
    }
}
