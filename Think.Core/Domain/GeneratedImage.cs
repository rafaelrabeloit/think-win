using System;
using System.Text;
using System.Collections.Generic;


namespace Think.Core.Domain {
    
    public class GeneratedImage {
        public virtual int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Image Image { get; set; }
        public virtual Quote Quote { get; set; }
        public virtual string Filepath { get; set; }
    }
}
