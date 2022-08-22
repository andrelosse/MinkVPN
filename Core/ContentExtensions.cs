using System.Windows;

namespace MinkVPN.Core
{
    internal class ContentExtensions
    {
        public static readonly DependencyProperty Icon = DependencyProperty.RegisterAttached("Icon", typeof(string), typeof(ContentExtensions), new PropertyMetadata(default(string)));

        public static void SetIcon(UIElement uie, string value)
        {
            uie.SetValue(Icon, value);
        }
        public static string GetIcon (UIElement uie)
        {
            return (string)uie.GetValue(Icon);
        }
    }
}
