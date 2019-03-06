using System;
using System.Text;
using System.Collections.Generic;


namespace Think.Core.Domain {
    
    public class UserQuote {
        public virtual int Id { get; set; }
        public virtual User User { get; set; }
        public virtual RootQuote RootQuote { get; set; }
        public virtual DateTime? ReceivedOn { get; set; }
        public virtual int Favorited { get; set; }
    }
}
