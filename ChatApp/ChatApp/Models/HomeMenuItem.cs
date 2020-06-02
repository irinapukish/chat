using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Models
{
    public enum MenuItemType
    {   
        Me,
        Chats,
        Users,
        Browse,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
