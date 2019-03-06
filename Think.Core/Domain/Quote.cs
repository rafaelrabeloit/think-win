using System;
using System.Text;
using System.Collections.Generic;


namespace Think.Core.Domain {
    
    public class Quote {
        public Quote() {
			GeneratedImages = new List<GeneratedImage>();
        }
        public virtual int Id { get; set; }
        public virtual Language Language { get; set; }
        public virtual RootQuote RootQuote { get; set; }
        public virtual User User { get; set; }
        public virtual RootCategory RootCategory { get; set; }
        public virtual RootReferenceType RootReferenceType { get; set; }
        public virtual RootReference RootReference { get; set; }
        public virtual RootAuthor RootAuthor { get; set; }
        public virtual string CategoryDescription { get; set; }
        public virtual string ReferenceTypeDescription { get; set; }
        public virtual string ReferenceDescription { get; set; }
        public virtual string AuthorFullname { get; set; }
        public virtual string UserNickname { get; set; }
        public virtual string Status { get; set; }
        public virtual string Text { get; set; }
        public virtual IList<GeneratedImage> GeneratedImages { get; set; }
    }
}
