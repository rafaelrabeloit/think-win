using System;
using System.Text;
using System.Collections.Generic;


namespace Think.Core.Domain {
    
    public class User {
        public User() {
			Devices = new List<Device>();
			GeneratedImages = new List<GeneratedImage>();
			Logs = new List<Log>();
			Quotes = new List<Quote>();
			RootAuthors = new List<RootAuthor>();
			RootQuotes = new List<RootQuote>();
			UserQuotes = new List<UserQuote>();
        }
        public virtual int Id { get; set; }
        public virtual Language Language { get; set; }
        public virtual string Nickname { get; set; }
        public virtual string TypeAccount { get; set; }
        public virtual DateTime CreatedOn { get; set; }
        public virtual DateTime? ModifiedOn { get; set; }
        public virtual string FacebookToken { get; set; }
        public virtual string FacebookUid { get; set; }
        public virtual string Password { get; set; }
        public virtual int Salt { get; set; }
        public virtual IList<Device> Devices { get; set; }
        public virtual IList<GeneratedImage> GeneratedImages { get; set; }
        public virtual IList<Log> Logs { get; set; }
        public virtual IList<Quote> Quotes { get; set; }
        public virtual IList<RootAuthor> RootAuthors { get; set; }
        public virtual IList<RootQuote> RootQuotes { get; set; }
        public virtual IList<UserQuote> UserQuotes { get; set; }
    }
}
