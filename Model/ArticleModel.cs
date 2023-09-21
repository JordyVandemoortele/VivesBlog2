using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ArticleModel
    {
        public Article? Article { get; set; }
        public IList<Person>? Authors { get; set; }
    }
}
