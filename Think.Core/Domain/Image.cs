using System;
using System.Text;
using System.Collections.Generic;


namespace Think.Core.Domain {
    
    public class Image {
        public Image() {
			GeneratedImages = new List<GeneratedImage>();
			RootAuthorImages = new List<RootAuthorImage>();
			RootCategoryImages = new List<RootCategoryImage>();
        }
        public virtual int Id { get; set; }
        public virtual int Posx { get; set; }
        public virtual int Posy { get; set; }
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }
        public virtual string Fontcolor { get; set; }
        public virtual string Imagepath { get; set; }
        public virtual IList<GeneratedImage> GeneratedImages { get; set; }
        public virtual IList<RootAuthorImage> RootAuthorImages { get; set; }
        public virtual IList<RootCategoryImage> RootCategoryImages { get; set; }
    }
}
