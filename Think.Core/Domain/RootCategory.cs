using System;
using System.Text;
using System.Collections.Generic;


namespace Think.Core.Domain {
    
    public class RootCategory {
        public RootCategory() {
			Categories = new List<Category>();
			Quotes = new List<Quote>();
			RootCategoryImages = new List<RootCategoryImage>();
			RootQuotes = new List<RootQuote>();
        }
        public virtual int Id { get; set; }
        public virtual IList<Category> Categories { get; set; }
        public virtual IList<Quote> Quotes { get; set; }
        public virtual IList<RootCategoryImage> RootCategoryImages { get; set; }
        public virtual IList<RootQuote> RootQuotes { get; set; }
    }
}
