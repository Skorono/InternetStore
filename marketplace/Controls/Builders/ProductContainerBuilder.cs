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

        public ProductContainerBuilder SetProductHandler(RoutedEventHandler handler)
        {
            container.NotifyChangeHandler(handler);
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
