using System;
using System.Text;
using System.Collections.Generic;


namespace Think.Core.Domain {
    
    public class DeviceType {
        public DeviceType() {
			DeviceSecurities = new List<DeviceSecurity>();
        }
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<DeviceSecurity> DeviceSecurities { get; set; }
    }
}
