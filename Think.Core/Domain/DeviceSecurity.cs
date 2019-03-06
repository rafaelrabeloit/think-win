using System;
using System.Text;
using System.Collections.Generic;


namespace Think.Core.Domain {
    
    public class DeviceSecurity {
        public DeviceSecurity() {
			Devices = new List<Device>();
        }
        public virtual int Id { get; set; }
        public virtual DeviceType DeviceType { get; set; }
        public virtual string AppVersion { get; set; }
        public virtual int Enable { get; set; }
        public virtual string PrivateKey { get; set; }
        public virtual string ApiKey { get; set; }
        public virtual IList<Device> Devices { get; set; }
    }
}
