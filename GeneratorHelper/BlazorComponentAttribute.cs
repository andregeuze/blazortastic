using System;
using System.Collections.Generic;
using System.Text;

namespace GeneratorHelper
{
    public class BlazorComponentAttribute : Attribute
    {
        public BlazorComponentAttribute() : this(true, true, true, true)
        {

        }

        public BlazorComponentAttribute(bool withCreate, bool withDelete, bool withEdit, bool withOverview)
        {
            WithCreate = withCreate;
            WithDelete = withDelete;
            WithEdit = withEdit;
            WithOverview = withOverview;
        }

        public bool WithCreate { get; }
        public bool WithDelete { get; }
        public bool WithEdit { get; }
        public bool WithOverview { get; }
    }
}
