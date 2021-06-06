using System;
using System.Collections.Generic;
using System.Text;

namespace MyFirstMvcApp.ViewModel
{
    public class DoAddViewModel
    {
        public int Attack { get; set; }
        public int Health { get; set; }
        public int Damage => Attack * 10 + Health;
    }
}
