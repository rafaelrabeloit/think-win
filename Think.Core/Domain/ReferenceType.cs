using System;
using System.Text;
using System.Collections.Generic;


namespace Think.Core.Domain {
    
    public class ReferenceType {
        public ReferenceType() {
			References = new List<Reference>();
        }
        public virtual int Id { get; set; }
        public virtual Language Language { get; set; }
        public virtual RootReferenceType RootReferenceType { get; set; }
        public virtual string Description { get; set; }
        public virtual IList<Reference> References { get; set; }
    }
}
