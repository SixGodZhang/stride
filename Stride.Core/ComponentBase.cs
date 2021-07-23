using System;
using System.Collections.Generic;
using System.Text;

namespace Stride.Core
{
    public abstract class ComponentBase : DisposeBase, IComponent
    {
        private string name;

        protected ComponentBase():this(null)
        {

        }

        protected ComponentBase(string name)
        {
            Name = name ?? GetType().Name;
        }



        public virtual string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value == name) return;
                name = value;
                OnNameChanged();
            }
        }

        protected virtual void OnNameChanged()
        {

        }

        public override string ToString()
        {
            return $"{GetType().Name} : {name}";
        }
    }
}
