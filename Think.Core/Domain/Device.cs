using System;
using System.Text;
using System.Collections.Generic;


namespace Think.Core.Domain {
    
    public class Device {
        public virtual int Id { get; set; }
        public virtual User User { get; set; }
        public virtual DeviceSecurity DeviceSecurity { get; set; }
        public virtual int Active { get; set; }
        public virtual int Premium { get; set; }
        public virtual int Periodicity { get; set; }
        public virtual string Pushurl { get; set; }
    }
}
