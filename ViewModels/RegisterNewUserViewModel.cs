using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using SIMMS.Models;
using SIMMS.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SIMMS.ViewModels
{
	public class RegisterNewUserViewModel: BindableBase
	{
		//fields 
		private byte[] _imgSelected;
		private string _srcImg = Path.Combine(Directory.GetParent
			(Directory.GetCurrentDirectory()).Parent.FullName, @"/Images/user.png");
		//private string _srcImg = "/Images/user.png";
		private string _newUser;
		private string _newPassword;
		private string _newName;
		private string _newApP;
		private string _newApM;
		private string _newEmail;
		private string _newPosition;
		private string _errorMessage;
		public IUserRepository userRepository;
		
		#region Public Properties
		public string SrcImg
		{
			get { return _srcImg; }
			set
			{
				SetProperty (ref _srcImg, value);
				CreateNewUserCommand.RaiseCanExecuteChanged();
			}
		}
		public byte[] ImgSelected
		{
			get
			{
				return _imgSelected;
			}

			set
			{
				SetProperty(ref _imgSelected, value);
				CreateNewUserCommand.RaiseCanExecuteChanged();
			}
		}
		
		public string NewUser
		{
			get
			{
				return _newUser;
			}

			set
			{
				SetProperty(ref _newUser, value);
				CreateNewUserCommand.RaiseCanExecuteChanged ();
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
				CreateNewUserCommand.RaiseCanExecuteChanged();
			}
		}

		public string NewName
		{
			get
			{
				return _newName;
			}

			set
			{
				SetProperty(ref _newName, value);
				CreateNewUserCommand.RaiseCanExecuteChanged();
			}
		}

		public string NewApP
		{
			get
			{
				return _newApP;
			}

			set
			{
				SetProperty(ref _newApP, value);
				CreateNewUserCommand.RaiseCanExecuteChanged();
			}
		}

		public string NewApM
		{
			get
			{
				return _newApM;
			}

			set
			{
				SetProperty(ref _newApM, value);
				CreateNewUserCommand.RaiseCanExecuteChanged();
			}
		}

		public string NewEmail
		{
			get
			{
				return _newEmail;
			}

			set
			{
				SetProperty(ref _newEmail, value);
				CreateNewUserCommand.RaiseCanExecuteChanged();
			}
		}

		public string NewPosition
		{
			get
			{
				return _newPosition;
			}

			set
			{
				SetProperty(ref _newPosition, value);
				CreateNewUserCommand.RaiseCanExecuteChanged();
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

		#endregion

		#region Commands
		public DelegateCommand SelectImgCommand { get; }
		public DelegateCommand CreateNewUserCommand { get; }

		#endregion

		#region Constructor

		public RegisterNewUserViewModel()
		{
			userRepository = new UserRepository();
			SelectImgCommand = new DelegateCommand(ExecuteSelectImgCommand);
			CreateNewUserCommand = new DelegateCommand(ExecuteCreateNewUserCommand,
														CanExecuteCreateNewUserCommand);
		}
		#endregion


		public int ValidationNullFields()
		{
			string[] listFields = new string[] {NewName,NewApP,NewApM,NewPosition,
												NewUser,NewPassword,NewEmail,SrcImg};
			int j = 8;
			for (int i = 0; i < listFields.Length; i++)
			{
				if (!string.IsNullOrWhiteSpace(listFields[i]))
				{
					--j;

				}
			}

			return j;
		}

		private bool CanExecuteCreateNewUserCommand()
		{
			bool validData;
			if (string.IsNullOrWhiteSpace(NewName) || string.IsNullOrWhiteSpace(NewApP)
				|| string.IsNullOrWhiteSpace(NewApM) || string.IsNullOrWhiteSpace(NewPosition)
				|| string.IsNullOrWhiteSpace(NewEmail) || string.IsNullOrWhiteSpace(NewUser)
				|| string.IsNullOrWhiteSpace(NewPassword)
				|| string.IsNullOrWhiteSpace(SrcImg))
			{
				validData = false;
				ErrorMessage = $"Existen {ValidationNullFields()} campos por llenar";
			}
			else
			{
				validData = true;
				ErrorMessage = $"Existen {ValidationNullFields()} campos por llenar";
			}
			return validData;
		}

		private async void ExecuteCreateNewUserCommand()
		{
			UserModel userModel = new UserModel();
			userModel.Name = NewName;
			userModel.ApellidoP = NewApP;
			userModel.ApellidoM = NewApM;
			userModel.UserName = NewUser;
			userModel.Password = BCrypt.Net.BCrypt.HashPassword(NewPassword);
			userModel.Email = NewEmail;
			userModel.Position = NewPosition;
			userModel.Profile = File.ReadAllBytes(SrcImg);
			await userRepository.Add(userModel);
			ErrorMessage = "Usuario Registrado";
			await Task.Delay(1000);
			ResetCampos();
		}
		private void ExecuteSelectImgCommand()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			bool? result = openFileDialog.ShowDialog();
			if (result == true)
			{
				SrcImg = openFileDialog.FileName;
			}
		}
		public void ResetCampos()
		{
			NewName = string.Empty;
			NewApP = string.Empty;
			NewApM = string.Empty;
			NewUser = string.Empty;
			NewPassword = string.Empty;
			NewEmail = string.Empty;
			NewPosition = string.Empty;

		}


	}
}
