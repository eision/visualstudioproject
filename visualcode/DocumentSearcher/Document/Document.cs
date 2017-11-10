using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doc
{
    public class Document
    {
        private string id;
        private string title;
        private string body;

        public Document(string id, string title, string body)
        {
            this.id = id;
            this.title = title;
            this.body = body;
        }

         // string ID プロパティ
        public string ID
        {
            set { this.id = value; }
            get { return this.id; }
        }

         // string Title プロパティ
        public string Title
        {
            set { this.title = value; }
            get { return this.title; }
        }

        // string Body プロパティ
        public string Body
        {
            set { this.body = value; }
            get { return this.body; }
        }
    }
}
