using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morphemename
{
    public class Morpheme
    {
        private string surface;
        private string pos;

        public Morpheme(string surface, string pos)
        {
            this.surface = surface;
            this.pos = pos;
        }

        // string surface プロパティ
        public string Surface
        {
            set { this.surface = value; }
            get { return this.surface; }
        }

        // string pos プロパティ
        public string Pos
        {
            set { this.pos = value; }
            get { return this.pos; }
        }

        public string Tostring() {
            return string.Format("{0} ({1})", this.surface, this.pos);
        }

    }
}
