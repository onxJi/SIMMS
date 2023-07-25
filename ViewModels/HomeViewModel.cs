using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using SIMMS.Models;
using SIMMS.Modules.HomeModule.ViewModels;
using SIMMS.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SIMMS.ViewModels
{
	public class HomeViewModel : BindableBase
	{
		private IUserRepository _userRepository;
		private IActivityRepository _activityRepository;
		private UserAccountModel _currentUserAccount;
		private string _displayName;
		private IEnumerable<string> valuesPriority = new List<string> { "Baja prioridad",
		"Media prioridad","Alta prioridad"};
		private IEnumerable<string> sortValuesBy = new List<string> { "Baja prioridad",
		"Media prioridad","Alta prioridad","Sin completar",
		"Completados", "Fechas Proximas","Fechas Lejanas"};
		private string _activityTitle, _activityDescription, _priorityValue, sortValue;
		private bool _isCompleted;
		private ObservableCollection<BindableBase> _activityCardObservableCollection;
		private static Role userLogin;
		private DateTime _dateFrom = DateTime.Now;
		private DateTime _dateTo = DateTime.Now;
		private string _txtSearch;
		IEventAggregator _ea;
		private readonly IRegionManager _regionManager;

		#region Campos
		public Role UserLogin
		{
			get { return userLogin; }
			set { SetProperty(ref userLogin, value); }
		}
		public string DisplayName
		{
			get
			{
				return _displayName;
			}
			set
			{
				SetProperty(ref _displayName, value);
			}
		}
		public UserAccountModel CurrentUserAccount
		{
			get { return _currentUserAccount; }
			set
			{
				SetProperty(ref _currentUserAccount, value);
			}
		}

		public IEnumerable<string> ValuesPriority
		{
			get
			{
				return valuesPriority;
			}

			set
			{
				SetProperty(ref valuesPriority, value);
			}
		}

		public bool IsCompleted
		{
			get => _isCompleted;
			set
			{
				SetProperty(ref _isCompleted, value);
			}
		}

		public DateTime DateFrom
		{
			get => _dateFrom;
			set
			{
				SetProperty(ref _dateFrom, value);
			}
		}
		public DateTime DateTo
		{
			get => _dateTo;
			set
			{
				SetProperty(ref _dateTo, value);
			}
		}
		public string ActivityTitle
		{
			get => _activityTitle;
			set
			{
				SetProperty(ref _activityTitle, value);
				AgreeActivityCommand.RaiseCanExecuteChanged();
			}
		}
		public string ActivityDescription
		{
			get => _activityDescription;
			set
			{
				SetProperty(ref _activityDescription, value);
				AgreeActivityCommand.RaiseCanExecuteChanged();
			}
		}
		public string PriorityValue
		{
			get => _priorityValue;
			set
			{
				SetProperty(ref _priorityValue, value);
				AgreeActivityCommand.RaiseCanExecuteChanged();
			}
		}
		public ObservableCollection<BindableBase> ActivityCardObservableCollection
		{
			get => _activityCardObservableCollection;
			set
			{
				SetProperty(ref _activityCardObservableCollection, value);
			}
		}
		public string TxtSearch
		{
			get => _txtSearch;
			set
			{
				SetProperty(ref _txtSearch, value);
				SearchActivities(TxtSearch);
			}
		}
		public IEnumerable<string> SortValuesBy
		{
			get => sortValuesBy;
			set
			{
				SetProperty(ref sortValuesBy, value);
			}
		}
		public string SortValue
		{
			get => sortValue;
			set
			{
				SetProperty(ref sortValue, value);
				SortByActivitiesCase(SortValue);
			}
		}

		#endregion
		//Commands
		public DelegateCommand AgreeActivityCommand { get; }
		public DelegateCommand EditUserInfoCommand { get; }
		public DelegateCommand<string> Navigate { get; }
		//Constructor
		public HomeViewModel(IEventAggregator ea, IRegionManager regionManager)
		{
			_ea = ea;
			_regionManager = regionManager;
			_userRepository = new UserRepository();
			_activityRepository = new ActivityRepository();
			UserLogin = new Role();
			AgreeActivityCommand = new DelegateCommand(ExecuteAgreeActivityCommand,
				CanExecuteAgreeActivityCommand);
			EditUserInfoCommand = new DelegateCommand(ExecuteEditUserInfoCommand);
			_ea.GetEvent<MessageRole>().Subscribe(GetUser);
			_ea.GetEvent<MessageSentEvent>().Subscribe(RefreshActivities);
			Navigate = new DelegateCommand<string>(ExecuteNavigate);
		}

		private void ExecuteNavigate(string obj)
		{
			if (obj != null)
				_regionManager.RequestNavigate("ContentRegion", obj);
		}

		private void ExecuteEditUserInfoCommand()
		{
			//_ea.GetEvent<MessageUserModel>().Publish(UserModel);
		}

		private async void RefreshActivities(string obj)
		{
			if (obj == "Refresh") await LoadUsernameActivities();
			if (obj == "Refresh2") await LoadUsernameActivities();
		}

		private async void GetUser(Role role)
		{
			UserLogin.Username = role.Username;
			await LoadUsernameInfo();
			await LoadUsernameActivities();
		}

		private bool CanExecuteAgreeActivityCommand()
		{
			bool isValidInfo = false;
			if (!string.IsNullOrWhiteSpace(ActivityTitle) ||
				!string.IsNullOrWhiteSpace(ActivityDescription) ||
				!string.IsNullOrWhiteSpace(PriorityValue))
			{
				isValidInfo = true;
			}
			return isValidInfo;
		}
		private async void ExecuteAgreeActivityCommand()
		{
			ActivityModel activityModel = new ActivityModel();
			activityModel.User = UserLogin.Username;
			activityModel.Title = ActivityTitle;
			activityModel.Description = ActivityDescription;
			activityModel.DateFrom = DateFrom;
			activityModel.DateTo = DateTo;
			activityModel.Priority = PriorityValue;
			activityModel.IsCompleted = IsCompleted;
			await _activityRepository.Add(activityModel);
			await Task.Delay(100);
			MessageBox.Show("La Actividad ha sido Registrada");
			await LoadUsernameActivities();
		}
		private async void SearchActivities(string title)
		{

			if (UserLogin.Username != null)
			{
				var userActivity = await _activityRepository.GetActivitiesByTitleAsync(UserLogin.Username, title);
				if (userActivity != null)
				{
					var viewModels = userActivity.Select(item => new ActivityCardViewModel(_ea)
					{
						IdActivity = item.Id,
						UserName = item.User,
						TxtTituloCard = item.Title.Trim(),
						TxtDescriptionCard = item.Description.Trim(),
						DateFrom = item.DateFrom.ToString("d"),
						DateTo = item.DateTo.ToString("d"),
						Priority = item.Priority.Trim(),
						IsCompleted = item.IsCompleted
					}).ToList();
					ActivityCardObservableCollection = new ObservableCollection<BindableBase>(viewModels);

				}
			}
			
		}
		private async void SortByActivitiesCase(string caseActivity)
		{
			switch (caseActivity)
			{
				case "Baja prioridad":
					await SortActivitiesBy(_activityRepository.ConsultActivitiesByPriority(
						UserLogin.Username, caseActivity));
					break;
				case "Media prioridad":
					await SortActivitiesBy(_activityRepository.ConsultActivitiesByPriority(
						UserLogin.Username, caseActivity));
					break;
				case "Alta prioridad":
					await SortActivitiesBy(_activityRepository.ConsultActivitiesByPriority(
						UserLogin.Username, caseActivity));
					break;
				case "Fechas Proximas":
					await SortActivitiesBy(_activityRepository.ConsultActivitiesByDateAsc(
						UserLogin.Username));
					break;
				case "Fechas Lejanas":
					await SortActivitiesBy(_activityRepository.ConsultActivitiesByDateDesc(
						UserLogin.Username));
					break;
			}
		}
		private async Task SortActivitiesBy(Task<DataTable> dt)
		{
			if(	UserLogin.Username != null)
			{
				var userActivity = await _activityRepository.GetActivitiesAsync(UserLogin.Username,
					dt);
				if (userActivity != null)
				{
					var viewModels = userActivity.Select(item => new ActivityCardViewModel(_ea)
					{
						IdActivity = item.Id,
						UserName = item.User,
						TxtTituloCard = item.Title.Trim(),
						TxtDescriptionCard = item.Description.Trim(),
						DateFrom = item.DateFrom.ToString("d"),
						DateTo = item.DateTo.ToString("d"),
						Priority = item.Priority.Trim(),
						IsCompleted = item.IsCompleted
					}).ToList();
					ActivityCardObservableCollection = new ObservableCollection<BindableBase>(viewModels);

				}
			}
			
		}
		private async Task LoadUsernameActivities()
		{
			if (UserLogin.Username != null)
			{
				var userActivity = await _activityRepository.GetActivitiesAsync(UserLogin.Username,
					_activityRepository.ConsultActivities(UserLogin.Username));
				if (userActivity != null)
				{
					var viewModels = userActivity.Select(item => new ActivityCardViewModel(_ea)
					{
						IdActivity = item.Id,
						UserName = item.User,
						TxtTituloCard = item.Title.Trim(),
						TxtDescriptionCard = item.Description.Trim(),
						DateFrom = item.DateFrom.ToString("d"),
						DateTo = item.DateTo.ToString("d"),
						Priority = item.Priority.Trim(),
						IsCompleted = item.IsCompleted
					}).ToList();
					ActivityCardObservableCollection = new ObservableCollection<BindableBase>(viewModels);

				}
			}

		}
		private async Task LoadUsernameInfo()
		{
			
			if(UserLogin.Username != null)
			{
				var user = await _userRepository.GetDataAll(UserLogin.Username);
				if (user != null)
				{
					foreach (var item in user)
					{
						CurrentUserAccount = new UserAccountModel()
						{
							Nombre = item.Name.Trim(),
							Username = item.UserName.Trim(),
							ApellidoP = item.ApellidoP.Trim(),
							ApellidoM = item.ApellidoM.Trim(),
							Email = item.Email.Trim(),
							Position = item.Position.Trim(),
							Profile = item.Profile,
						};
						DisplayName = $"{CurrentUserAccount.Nombre} " +
							$"{CurrentUserAccount.ApellidoP} " +
							$"{CurrentUserAccount.ApellidoM}";

					}

				}
			}

		}
	}
}
