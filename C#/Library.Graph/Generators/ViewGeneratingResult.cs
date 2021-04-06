using System;
using System.Linq;

using Library.Graph.Views;

namespace Library.Graph.Generators
{
    public sealed class ViewGeneratingResult<TValue>
        where TValue : notnull
    {
        public AdjacensiesView<TValue> View { get; }

        public ViewGeneratingResult(AdjacensiesView<TValue> view)
        {
            if (view is null)
            {
                throw new ArgumentNullException(nameof(view));
            }
            if (!view.Items.Any())
            {
                throw new ArgumentException("The view items collection is empty.", nameof(view.Items));
            }
            View = view;
        }
    }
}