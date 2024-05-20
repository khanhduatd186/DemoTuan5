using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace DemoTuan5.Helper
{
    public class ShowActionListView : IScopedDependency
    {
        private bool unreadCount;

        public bool UnreadCount
        {
            get => unreadCount;
            set
            {
                unreadCount = value;
                UnreadCountChanged?.Invoke(this, new EventArgs());
            }
        }

        public event EventHandler UnreadCountChanged;
    }
}
