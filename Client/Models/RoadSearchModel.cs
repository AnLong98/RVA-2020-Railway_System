using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class RoadSearchModel
    {
        public RoadSearchModel()
        {
            Name = "";
            Label = "";
        }

        public RoadSearchModel(string name, string label)
        {
            Name = name;
            Label = label;
        }

        public string Name { get; set; }
        public string Label { get; set; }

        public bool IsValid()
        {
            return Name != "" || Label != "";
        }
    }
}
