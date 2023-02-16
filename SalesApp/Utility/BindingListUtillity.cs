using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SALEERP.Models;

namespace SalesApp.Utility
{
    public class BindingListUtillity
    {
        public static List<SelectListItem> GenerateSelectList(List<CurrencyMaster> collection)
        {
            List<SelectListItem> newSelectList = new List<SelectListItem>();
            foreach (var item in collection)
            {
                newSelectList.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Type,
                    Selected=item.Type.ToUpper()== "INR" ? true:false
                });
            }
            return newSelectList;
        }
        public static List<SelectListItem> GenerateSelectListWidth(List<Size> collection)
        {
            List<SelectListItem> newSelectList = new List<SelectListItem>();
            foreach (var item in collection)
            {
                newSelectList.Add(new SelectListItem
                {
                    Value = item.WidthInch.ToString(),
                    Text = item.WidthInch.ToString()
                });
            }
            return newSelectList;
        }
        public static List<SelectListItem> GenerateSelectListLength(List<Size> collection)
        {
            List<SelectListItem> newSelectList = new List<SelectListItem>();
            foreach (var item in collection)
            {
                newSelectList.Add(new SelectListItem
                {
                    Value = item.LengthInch.ToString(),
                    Text = item.LengthInch.ToString()+"''"
                }); ;
            }
            return newSelectList;
        }
        public static List<SelectListItem> GenerateSelectList(List<ItemMaster> collection)
        {
            List<SelectListItem> newSelectList = new List<SelectListItem>();
            foreach (var item in collection)
            {
                newSelectList.Add(new SelectListItem
                {
                    Value = item.ItemCode,
                    Text = item.ItemName
                });
            }
            return newSelectList;
        }
        public static List<SelectListItem> GenerateSelectList(List<SpecialEdition> collection)
        {
            List<SelectListItem> newSelectList = new List<SelectListItem>();
            foreach (var item in collection)
            {
                newSelectList.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name,
                    Selected = false
                });
            }
            return newSelectList;
        }
        public static List<SelectListItem> GenerateSelectList(List<LoginRoles> collection)
        {
            List<SelectListItem> newSelectList = new List<SelectListItem>();
            foreach (var item in collection)
            {
                newSelectList.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                });
            }
            return newSelectList;
        }
        public static List<SelectListItem> GenerateSelectList(List<Shape> collection)
        {
            List<SelectListItem> newSelectList = new List<SelectListItem>();
            foreach (var item in collection)
            {
                newSelectList.Add(new SelectListItem
                {
                    Value = item.ShapeName,
                    Text = item.ShapeName
                });
            }
            return newSelectList;

        }
        public static List<SelectListItem> GenerateSelectList(List<DesignIntricacyComponentPlacementMarblecolor> collection)
        {
            List<SelectListItem> newSelectList = new List<SelectListItem>();
            foreach (var item in collection)
            {
                newSelectList.Add(new SelectListItem
                {
                    Value =UppercaseFirst(item.Name),
                    Text = UppercaseFirst(item.Name)
                });
            }
            return newSelectList;

        }
        public static List<SelectListItem> GenerateSelectListcolor(List<DesignIntricacyComponentPlacementMarblecolor> collection)
        {
            List<SelectListItem> newSelectList = new List<SelectListItem>();
            foreach (var item in collection)
            {
                newSelectList.Add(new SelectListItem
                {
                    Value = UppercaseFirst(item.Name),
                    Text = UppercaseFirst(item.Name),
                });
            }
            return newSelectList;

        }
        public static List<SelectListItem> GenerateSelectList(List<ItemCategoryMaster> collection)
        {
            List<SelectListItem> newSelectList = new List<SelectListItem>();
            foreach (var item in collection)
            {
                newSelectList.Add(new SelectListItem
                {
                    Value = item.CategoryName,
                    Text = item.CategoryName
                });
            }
            return newSelectList;

        }
        public static List<SelectListItem> GenerateSelectList(List<Nationality> collection)
        {
            List<SelectListItem> newSelectList = new List<SelectListItem>();
            foreach (var item in collection)
            {
                newSelectList.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Nationality1
                });
            }
            return newSelectList;
        }
        public static List<SelectListItem> GenerateSelectList(List<CardTypeMaster> collection)
        {
            List<SelectListItem> newSelectList = new List<SelectListItem>();
            foreach (var item in collection)
            {
                newSelectList.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.CardName
                });
            }
            return newSelectList;
        }
        public static List<SelectListItem> GenerateSelectList(List<PayLaterMaster> collection)
        {
            List<SelectListItem> newSelectList = new List<SelectListItem>();
            foreach (var item in collection)
            {
                newSelectList.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.TypeName
                });
            }
            return newSelectList;
        }
        public static List<SelectListItem> GenerateSelectList(List<CountriesMaster> collection)
        {
            List<SelectListItem> newSelectList = new List<SelectListItem>();
            foreach (var item in collection)
            {
                newSelectList.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                });
            }
            return newSelectList;
        }
        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1).ToLower();
        }
    }
}
