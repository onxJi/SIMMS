using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using SIMMS.Models;
using SIMMS.Models.MailServices;
using SIMMS.Modules.ChangePasswordModule.Views;
using SIMMS.Modules.SendCodeModule.Views;
using SIMMS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Navigation;

namespace SIMMS.Modules.SendCodeModule.ViewModels
{
    public class SendCodeRegionViewModel: BindableBase
    {
        private static string _ramdomCode;
        private string _email;
        private string _codeValidation;
        private string _errorMessage;
        private IEventAggregator _ea;
        private IUserRepository _userRepository;
        private UserAccountModel _userAccount;
        IRegionManager _regionManager;
        IRegionNavigationJournal _journal;
        public string Email
        {
            get => _email;
            set
            {
                SetProperty(ref _email, value);
                SendCodeCommand.RaiseCanExecuteChanged();
            }
        }
        public string CodeValidation
        {
            get => _codeValidation;
            set
            {
                SetProperty(ref _codeValidation, value);
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

        //Commands
        public DelegateCommand SendCodeCommand { get; private set; }
        public DelegateCommand ValidationCodeCommand { get; private set; }

        public SendCodeRegionViewModel(IEventAggregator ea, IRegionManager regionManager)
        {
            _userRepository = new UserRepository();
            SendCodeCommand = new DelegateCommand(ExecuteSendCodeCommand,
                CanExecuteSendCodeCommand);
            ValidationCodeCommand = new DelegateCommand(
                ExecuteValidationCodeCommad);
            _ea = ea;
            _regionManager = regionManager;

        }

        private void ExecuteValidationCodeCommad()
        {
            if(CodeValidation == _ramdomCode && CodeValidation!=null)
            {
                bool valid=true;
                _ea.GetEvent<MessageSentVisible>().Publish(valid);
                _ea.GetEvent<MessageSentEvent>().Publish(Email);
                _regionManager.RequestNavigate("ContentReset2", "ChangePasswordRegion");
            }
            
        }

        private bool CanExecuteSendCodeCommand()
        {
            bool result;
            if (!string.IsNullOrWhiteSpace(Email))
            {
                result = true;
            }
            else result = false;
            return result;
        }

        private async void ExecuteSendCodeCommand()
        {
           
            if (await _userRepository.ConsultByEmail(Email) == true &&
                !string.IsNullOrWhiteSpace(Email))
            {
                System.Windows.MessageBox.Show("Correo encontrado");
                Random random = new Random();
                _ramdomCode = (random.Next(999999)).ToString();
                var mailService = new SystemSupportMail();
                await mailService.sendMail(
                    subject: "sySIMMS: Restablecer Contraseña",
                    body: "Hola, usted solicito restablecer su contraseña\n" +
                    "Este es el codigo de validacion: <b>" + _ramdomCode + "</b>" +
                    "\nUna vez haya cambiado su contraseña, se recomienda " +
                    "guardarlo, ya que las contraseñas se encriptan en la " +
                    "base de datos",
                    recipientMail: new List<string> { Email }
                    );

                ErrorMessage = $"Se ha enviado el codigo a: {Email}";


            }
            else
            {
                System.Windows.MessageBox.Show("Ingrese un correo valido");
            }
        }
    }
}
