using Think.Core.Domain;
using Think.WinStore.Data;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Think.WinStore
{

    public class VariableGridView : GridView
    {
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            var viewModel = item as QuoteItem;

            element.SetValue(VariableSizedWrapGrid.ColumnSpanProperty, viewModel.WSize);
            element.SetValue(VariableSizedWrapGrid.RowSpanProperty, viewModel.HSize);

            base.PrepareContainerForItemOverride(element, item);
        }
    }
}
