using System;
using System.Text;
using System.Collections.Generic;


namespace Think.Core.Domain {
    
    public class RootCategoryImage {
        public virtual int RootCategoryId { get; set; }
        public virtual int ImageId { get; set; }
        #region NHibernate Composite Key Requirements
        public override bool Equals(object obj) {
			if (obj == null) return false;
			var t = obj as RootCategoryImage;
			if (t == null) return false;
			if (RootCategoryId == t.RootCategoryId
			 && ImageId == t.ImageId)
				return true;

			return false;
        }
        public override int GetHashCode() {
			int hash = GetType().GetHashCode();
			hash = (hash * 397) ^ RootCategoryId.GetHashCode();
			hash = (hash * 397) ^ ImageId.GetHashCode();

			return hash;
        }
        #endregion
    }
}
