using System;
using System.Collections.Generic;
using System.Text;

namespace Realtor.Data.Entities
{
    public class State
    {
        public string name { get; set; }
        public int id { get; set; }
        public List<Local> locals { get; set; }
    }
}
