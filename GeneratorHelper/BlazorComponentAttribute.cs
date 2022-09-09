using System;

namespace GeneratorHelper
{
    public class BlazorComponentAttribute : Attribute
    {
        public BlazorComponentAttribute() : this(true, true, true, true, true)
        {

        }

        public BlazorComponentAttribute(bool withCreate, bool withDelete, bool withEdit, bool withOverview, bool withDetails)
        {
            WithCreate = withCreate ? "Create" : "DoNotCreate";
            WithDelete = withDelete ? "Delete" : "DoNotDelete";
            WithEdit = withEdit ? "Edit" : "DoNotEdit";
            WithOverview = withOverview ? "Overview" : "DoNotOverview";
            WithDetails = withDetails ? "Details" : "DoNotDetails";
        }

        public string WithCreate { get; }
        public string WithDelete { get; }
        public string WithEdit { get; }
        public string WithOverview { get; }
        public string WithDetails { get; }
    }
}
