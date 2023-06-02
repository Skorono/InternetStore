using InternetStore.Controls.XAMLControls;
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

        public ProductContainerBuilder SetProductClickHandler(RoutedEventHandler handler)
        {
            container.NotifyChangeClickHandler(handler);
            return this;
        }

        public ProductContainerBuilder SetProductDoubleClickHandler(RoutedEventHandler handler)
        {
            container.NotifyChangeDoubleClickHandler(handler);
            return this;
        }

        public ProductContainerBuilder RentalPermissions()
        {
            container.AddingAccept = true;
            return this;
        }

        public ProductContainerBuilder AdministratorPermissions()
        {
            RentalPermissions();
            container.EditionAccept = true;
            return this;
        }
    }

}
