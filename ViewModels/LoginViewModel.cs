using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using SIMMS.Models;
using SIMMS.Repositories;
using SIMMS.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SIMMS.ViewModels
{
    public class LoginViewModel: BindableBase
    {
        private bool _isViewVisible = true;
        //Fields
        private string _userName;
        private string _password;
        private string _errorMessage;
        private bool validData;
        IEventAggregator _ea;
        private IUserRepository _userRepository;
        public string UserName
        {
            get
            {
                return _userName;
            }

            set
            {
                SetProperty(ref _userName, value);
                
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                SetProperty(ref _password, value);
                
            }
        }

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }

            set
            {
               SetProperty(ref _errorMessage, value);
                
            }
        }
        public bool IsViewVisible
        {
            get
            {
                return _isViewVisible;
            }

            set
            {
                SetProperty(ref _isViewVisible, value);
            }
        }
        public bool ValidData
        {
            get => validData;
            set
            {
                SetProperty(ref validData, value);
                if (string.IsNullOrWhiteSpace(UserName) || UserName.Length < 3 ||
                string.IsNullOrWhiteSpace(Password) || Password.Length < 3)
                    ValidData = true;
            }
        }
        public DelegateCommand LoginCommand { get; set; }
        public DelegateCommand ShowPasswordCommand { get; }
        public DelegateCommand RememberPasswordCommand { get; }
        public DelegateCommand RegisterNewUserCommand { get; }
        public DelegateCommand ResetPasswordCommand { get; }
        public DelegateCommand AssistantCommand { get; }
        

        public LoginViewModel(IEventAggregator ea)
        {
            _ea = ea;
            _userRepository = new UserRepository();
            LoginCommand = new DelegateCommand(ExecuteLoginCommand);
            RegisterNewUserCommand = new DelegateCommand(ExecuteRegisterNewUserCommand);
            ResetPasswordCommand = new DelegateCommand(ExecuteResetPasswordCommand);
            AssistantCommand = new DelegateCommand(ExecuteAssistantCommand);
        }

        private void ExecuteAssistantCommand()
        {
            throw new NotImplementedException();
        }

        private void ExecuteResetPasswordCommand()
        {
            var resetPassword = new ResetPassword();
            resetPassword.Show();
        }

        private void ExecuteRegisterNewUserCommand()
        {
            var registerNew = new RegisterNewUser();
            registerNew.Show();
        }

        private async void ExecuteLoginCommand()
        {
            
            bool IsLogin = await _userRepository.AuthenticateUser(
                new System.Net.NetworkCredential(UserName, Password));
            if (IsLogin)
            {
                var consult = await _userRepository.GetDataAll(UserName);
                foreach(var item in consult)
                {
                    var rol = new Role
                    {
                        Id = item.Id,
                        Username = item.UserName,
                        Rol = new string[] {item.Position}
                    };
                    _ea.GetEvent<MessageRole>().Publish(rol);
                }
                IsViewVisible = false;
            }
            else
            {
                ErrorMessage = "Username or Password incorrects";
                await Task.Delay(1000);
                ErrorMessage = "";
            }
        }
    }
}
