using System;
using System.Collections.Generic;
using System.Text;

namespace KooliProjekt.WpfApplication
{
    public class MainWindowViewModel
    {
        public IList<Project> Data 
        { 
            get
            {
                var items = new List<Project>
                {
                    new Project { Id = 1, Title = "Test 1" },
                    new Project { Id = 2, Title = "Test 2" },
                    new Project { Id = 3, Title = "Test 3" }
                };

                return items;
            }
        }

        public object SelectedItem { get; set; }
    }
}
