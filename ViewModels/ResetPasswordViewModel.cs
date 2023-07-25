using Prism.Events;
using Prism.Mvvm;
using SIMMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMMS.ViewModels
{
    public class ResetPasswordViewModel: BindableBase
    {
        IEventAggregator _ea;
        private bool _visible1=true, _visible2=false;
        public bool Visible1
        {
            get
            {
                return _visible1;
            }

            set
            {
                SetProperty(ref _visible1, value);
            }
        }

        public bool Visible2
        {
            get
            {
                return _visible2;
            }

            set
            {
                SetProperty(ref _visible2, value);
            }
        }
        public ResetPasswordViewModel(IEventAggregator ea) { 
            _ea = ea;
            _ea.GetEvent<MessageSentVisible>().Subscribe(MessageReceived);
        }

        private void MessageReceived(bool obj)
        {
            Visible1 = false;
            Visible2 = obj;
        }
    }
}
