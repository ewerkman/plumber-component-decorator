using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Plumber.Component
{
    public static class Constants
    {
        public static class UIHints
        {
            public const string Flat = "Flat";
            public const string Grid = "Grid";
            public const string List = "List";
            public const string MediaPicker = "MediaPicker";
            public const string Search = "Search";
            public const string Table = "Table";
            public const string BraintreePayment = "BraintreePayment";
        }

        public static class UITypes
        {
            public const string Empty = "Empty";
            public const string Autocomplete = "Autocomplete";
            public const string DownloadCsv = "DownloadCsv";
            public const string Dropdown = "Dropdown";
            public const string EntityLink = "EntityLink";
            public const string FullDateTime = "FullDateTime";
            public const string Html = "Html";
            public const string ItemLink = "ItemLink";
            public const string List = "List";
            public const string Multiline = "Multiline";
            public const string Options = "Options";
            public const string Product = "Product";
            public const string RichText = "RichText";
            public const string SelectList = "SelectList";
            public const string Sortable = "Sortable";
            public const string SubItemLink = "SubItemLink";
            public const string Tags = "Tags";
        }

        /// <summary>
        ///     The names of the Plumber.Catalog pipelines.
        /// </summary>
        public static class Pipelines
        {
            /// <summary>
            ///     The names of the Plumber.Catalog blocks.
            /// </summary>
            public static class Blocks
            {
                public const string DoActionAddValidationConstraintBlock = "Plumber.Catalog.DoActionAddValidationConstraintBlock";
                public const string DoActionEditComponentBlock = "Plumber.Catalog.DoActionEditComponentBlock";
                public const string GetAddMinMaxPropertyConstraintViewBlock = "Plumber.Catalog.GetAddMinMaxPropertyConstraintViewBlock";
                public const string GetComponentConnectViewBaseBlock = "Plumber.Catalog.GetComponentConnectViewBaseBlock";
                public const string GetComponentViewBlock = "Plumber.Catalog.GetComponentViewBlock";
                public const string GetRawSellableItemViewBlock = "Plumber.Catalog.GetRawSellableItemViewBlock";
                public const string PopulateComponentActionsBlock = "Plumber.Catalog.PopulateComponentActionsBlock";
                public const string ConfigureServiceApiBlock = "Plumber.Catalog.ConfigureServiceApiBlock";
            }

        }
    }
}
