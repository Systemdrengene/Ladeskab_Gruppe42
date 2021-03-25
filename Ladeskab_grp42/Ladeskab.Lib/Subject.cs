using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Lib
{
    public class Subject
    {
        private List<IObserver> _subjects = new List<IObserver>();

        public void Attach(IObserver s)
        {
            if (!_subjects.Contains(s))
                _subjects.Add(s);
        }

        public void Detache(IObserver s)
        {
            _subjects?.Remove(s);
        }

        public void Notify(string msg)
        {
            if (!_subjects.Any())
                return;

            foreach (IObserver s in _subjects)
            {
                s.Update(this, msg);
            }
        }
    }
}
