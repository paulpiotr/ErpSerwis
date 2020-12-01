using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace ErpSerwis.WpfApp.ViewFields
{
    public class TextBoxViewField : DataFormDataField
    {
        protected override DependencyProperty GetControlBindingProperty()
        {
            return TextBox.TextProperty;
        }
        protected override Control GetControl()
        {
            DependencyProperty dependencyProperty = GetControlBindingProperty();
            var textBox = new TextBox
            {
                AcceptsReturn = true,
                TextWrapping = TextWrapping.WrapWithOverflow,
                VerticalScrollBarVisibility = ScrollBarVisibility.Visible,
            };
            if (DataMemberBinding != null)
            {
                Binding binding = DataMemberBinding;
                textBox.SetBinding(dependencyProperty, binding);
            }
            //textBox.SetBinding(TextBox.IsEnabledProperty, new Binding("IsReadOnly") { Source = this, Converter = new InvertedBooleanConverter() });
            return textBox;
        }
    }
}
