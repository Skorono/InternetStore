using InternetStore.Controls.XAMLControls;
using System.ComponentModel;
using System.Windows;

namespace InternetStore.Controls.Builders
{

    public class ProductContainerBuilder
    {
        private ProductArea container = null!;

        public ProductContainerBuilder()
        {
            container = new ProductArea();
        }

        public ProductArea Build()
        {
            return container;
        }

        public ProductContainerBuilder SetProductHandler(RoutedEventHandler handler)
        {
            container.NotifyChangeHandler(handler);
            return this;
        }

        public ProductContainerBuilder EditionalAccept() {
            container.EditionAccept = true;
            return this;
        }
    }

}
