using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain
{
    public class MenuTree
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool Checked { get; set; }

        public List<MenuTree> children { get; set; }

        public int ParentId { get; set; }
    }
}
