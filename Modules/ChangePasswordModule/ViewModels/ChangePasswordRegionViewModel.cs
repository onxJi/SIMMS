using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using SIMMS.Models;
using SIMMS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace SIMMS.Modules.ChangePasswordModule.ViewModels
{
    public class ChangePasswordRegionViewModel: BindableBase
    {
        private IUserRepository _userRepository;
        private UserAccountModel _currentUser;
        private string _errorMessage;
        private IEventAggregator _ea;
        private string _newPassword;
        private string _confirmPassword;
        private string _messageToPassword;
        private string _email;
        private bool _enableUpdate;

        public bool EnableUpdate
        {
            get => _enableUpdate;
            set
            {
                SetProperty(ref _enableUpdate, value);
                UpdatePasswordCommand.RaiseCanExecuteChanged();
            }
        }
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        public UserAccountModel CurrentUser
        {
            get => _currentUser; 
            set
            {
                SetProperty(ref _currentUser, value);
            }
        }
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                SetProperty(ref _errorMessage, value);
            }
        }
        public string NewPassword
        {
            get
            {
                return _newPassword;
            }

            set
            {
               SetProperty(ref _newPassword, value);
               UpdatePasswordCommand.RaiseCanExecuteChanged();
            }
        }

        public string ConfirmPassword
        {
            get
            {
                return _confirmPassword;
            }

            set
            {
                SetProperty(ref _confirmPassword, value);
                UpdatePasswordCommand.RaiseCanExecuteChanged();
            }
        }
        public string MessageToPassword
        {
            get
            {
                return _messageToPassword;
            }

            set
            {
               SetProperty(ref _messageToPassword, value);
            }
        }

        public DelegateCommand UpdatePasswordCommand { get; }
        public ChangePasswordRegionViewModel(IEventAggregator ea)
        {
            _ea = ea;
            _userRepository = new UserRepository();
            UpdatePasswordCommand = new DelegateCommand(ExecuteUpdatePasswordCommand,
                CanExecuteUpdatePasswordCommand);
            _ea.GetEvent<MessageSentEvent>().Subscribe(MessageReceived);
            
        }

        private async void MessageReceived(string obj)
        {
            if (obj != null)
            {
                var user = await _userRepository.GetByEmail(obj);
                if (user != null)
                {
                    foreach (var item in user)
                    {
                        CurrentUser = new UserAccountModel()
                        {
                            Nombre = item.Name.Trim(),
                            Username = item.UserName.Trim(),
                            ApellidoP = item.ApellidoP.Trim(),
                            ApellidoM = item.ApellidoM.Trim(),
                            Email = item.Email.Trim(),
                            Position = item.Position.Trim(),
                            Profile = item.Profile,
                        };
                    }
                }
            }
        }

        private bool CanExecuteUpdatePasswordCommand()
        {
            bool result = false;
            if (NewPassword == ConfirmPassword &&
                !string.IsNullOrWhiteSpace(NewPassword) &&
                !string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                result = true;
                MessageToPassword = "Las contraseñas SI coinciden";

            }
            else
            {
                result = false;
                MessageToPassword = "Las contraseñas no coinciden";

            }
            return result;
        }
        private async void ExecuteUpdatePasswordCommand()
        {
            UserModel userModel = new UserModel();
            userModel.Password = BCrypt.Net.BCrypt.HashPassword(NewPassword);
            userModel.Email = CurrentUser.Email;
            await _userRepository.EditByEmail(userModel);
            System.Windows.MessageBox.Show("La contraseña se ha actualizado");
        }

        
    }
}
