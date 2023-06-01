using DocumentFormat.OpenXml.Drawing;
using InternetStore.Controls.XAMLControls;
using InternetStore.ModelDB;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace InternetStore.Controls.Builders
{
    public class ItemBuilder
    {
        private Item Item = null!;

        public ItemBuilder(Product model)
        {
            Item = new(model);
        }

        public Item Build()
        {
            return Item;
        }

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
            TextBlockButton ActionBlock = new();
            ActionBlock.Text = "Изменить";
            ActionBlock.Width = 100;
            ActionBlock.Height = 30;
            Item.ManipulationPanel.AcceptableActionList.Add(ActionBlock);
            Item.ManipulationPanel.UpdateActionList();
            return this;
        }


    }
}
