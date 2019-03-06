using System;
using System.Text;
using System.Collections.Generic;


namespace Think.Core.Domain {
    
    public class Reference {
        public Reference() {
			References = new List<Reference>();
        }
        public virtual int Id { get; set; }
        public virtual Language Language { get; set; }
        public virtual RootReference RootReference { get; set; }
        public virtual ReferenceType ReferenceType { get; set; }
        public virtual Reference ReferenceVal { get; set; }
        public virtual string Description { get; set; }
        public virtual IList<Reference> References { get; set; }
    }
}
