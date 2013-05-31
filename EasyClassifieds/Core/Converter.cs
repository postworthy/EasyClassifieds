using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyClassifieds.Core
{
    public class Converter
    {
        private dynamic func { get; set; }

        public Converter()
        {
            this.func = func;
        }

        public Converter RegisterFunc<I, O>(Func<I, O> func)
        {
            this.func = func;
            return this;
        }

        public dynamic Convert(dynamic arg)
        {
            return func(arg);
        }
    }
}