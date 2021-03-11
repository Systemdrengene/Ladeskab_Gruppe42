using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Lib
{
    abstract class Observer
    {
        private List<ISubject> _subjects = new List<ISubject>();

        void Attach(ISubject s)
        {
            if (!_subjects.Contains(s))
                _subjects.Add(s);
        }

        void Detache(ISubject s)
        {
            _subjects?.Remove(s);
        }

        void Notify()
        {
            if (!_subjects.Any())
                return;

            foreach (ISubject s in _subjects)
            {
                s.Update();
            }
        }
    }
}
