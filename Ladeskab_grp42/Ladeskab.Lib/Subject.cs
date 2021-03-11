using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Lib
{
    abstract class Subject
    {
        private List<IObserver> _subjects = new List<IObserver>();

        void Attach(IObserver s)
        {
            if (!_subjects.Contains(s))
                _subjects.Add(s);
        }

        void Detache(IObserver s)
        {
            _subjects?.Remove(s);
        }

        void Notify()
        {
            if (!_subjects.Any())
                return;

            foreach (IObserver s in _subjects)
            {
                s.Update();
            }
        }
    }
}
