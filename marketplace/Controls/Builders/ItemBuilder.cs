using InternetStore.Controls.XAMLControls;
using InternetStore.ModelDB;
using System.Windows;
using System.Windows.Controls;

namespace InternetStore.Controls.Builders
{
    public class ItemBuilder
    {
        private Item Item = null!;

        public string? ItemName { get => Item.ItemName; }
        public float ItemCost { get => Item.Cost; }
        public int ItemCount { get => Item.Count; }

        public ItemBuilder(Product model, bool AllowSync = true)
        {
            Item = new(model);
            Item.AllowSync = AllowSync;
        }

        public Item Build()
        {
            return Item;
        }

        /*public ItemBuilder NotAllowSync()
        {
            Item.AllowSync = false;
            return this;
        }*/

        public ItemBuilder isChangeable()
        {
            Item.isChangeable = true;
            return this;
        }

        public ItemBuilder isSortable()
        {
            Item.isSortable = true;
            return this;
        }

        public ItemBuilder SetDescriptionText(string text)
        {
            Item.DescriptionText.Text = text;
            return this;
        }

        public ItemBuilder SetCost(float cost)
        {
            Item.Cost = cost;
            return this;
        }

        public ItemBuilder SetImage(string path)
        {
            Item.Image = ImageManager.Upload(path);
            return this;
        }

        public ItemBuilder SetFontSize(string UIName, int size)
        {
            var field = UIHelper.FindField<Item>(UIName);

            if (field != null)
            {
                if (field.FieldType.IsAssignableTo(typeof(TextBlock)))
                {
                    TextBlock? uiElement = (TextBlock?)field.GetValue(Item);
                    if (uiElement != null)
                    {
                        uiElement.FontSize = size;
                        field.SetValue(Item, uiElement);
                    }
                }
            }
            return this;
        }

        public ItemBuilder SetFontWidth(string UIName, FontWeight widthOption)
        {
            var field = UIHelper.FindField<Item>(UIName);

            if (field != null)
            {
                if (field.FieldType.IsAssignableTo(typeof(TextBlock)))
                {
                    TextBlock? uiElement = (TextBlock?)field.GetValue(Item);
                    if (uiElement != null)
                    {
                        uiElement.FontWeight = widthOption;
                        field.SetValue(Item, uiElement);
                    }
                }
            }
            return this;
        }

        public ItemBuilder SetVisibility(string UIName, Visibility visibilityOption)
        {
            var field = UIHelper.FindField<Item>(UIName);

            if (field != null)
            {
                if (field.FieldType.IsAssignableTo(typeof(UIElement)))
                {
                    UIElement? uiElement = (UIElement?)field.GetValue(Item);
                    if (uiElement != null)
                    {
                        uiElement.Visibility = visibilityOption;
                        field.SetValue(Item, uiElement);
                    }
                }
            }
            return this;
        }

        public ItemBuilder isEdittable()
        {
            Item.ManipulationPanel.Visibility = Visibility.Visible;
            Item.ManipulationPanel.Width = 75;
            Item.ManipulationPanel.Width = 25;
            Item.ManipulationPanel.DeletePermissions = true;
            Item.ManipulationPanel.UpdateActionList();
            return this;
        }

        public void SetClickHandler(RoutedEventHandler handler)
        {
            Item.UpdateClickHandler(handler);
        }

        public void SetDoubleClickHandler(RoutedEventHandler handler)
        {
            Item.UpdateDoubleClickHandler(handler);
        }
    }
}
