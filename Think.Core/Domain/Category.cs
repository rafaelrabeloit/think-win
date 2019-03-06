using System;
using System.Text;
using System.Collections.Generic;


namespace Think.Core.Domain {
    
    public class Category {
        public virtual int Id { get; set; }
        public virtual Language Language { get; set; }
        public virtual RootCategory RootCategory { get; set; }
        public virtual string Description { get; set; }
    }
}
