using System;
using System.Text;
using System.Collections.Generic;


namespace Think.Core.Domain {
    
    public class Author {
        public virtual int Id { get; set; }
        public virtual Language Language { get; set; }
        public virtual RootAuthor RootAuthor { get; set; }
        public virtual string FullName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual string LastName { get; set; }
    }
}
