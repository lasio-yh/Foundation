using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Waf.Foundation;

namespace Emulator.ControlPlugin.Models
{
    public class DataSource : Model
    {
        private readonly ObservableCollection<Entity> items;

        public DataSource()
        {
            items = new ObservableCollection<Entity>();
        }

        public IReadOnlyList<Entity> Items => items;

        public void Add(Entity contact)
        {
            items.Add(contact);
        }

        public void Remove(Entity contact)
        {
            items.Remove(contact);
        }

        public void Clear()
        {
            items.Clear();
        }
    }
}
