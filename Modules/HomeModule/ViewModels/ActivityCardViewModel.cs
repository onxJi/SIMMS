using Prism.Mvvm;
using SIMMS.Models;
using SIMMS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Prism.Commands;
using SIMMS.Modules.HomeModule.Views;
using Prism.Events;

namespace SIMMS.Modules.HomeModule.ViewModels
{
	public class ActivityCardViewModel: BindableBase
	{
		private string _userName;
		private string _txtTituloCard;
		private string _txtDescriptionCard;
		private string _dateFrom;
		private string _dateTo;
		private bool _isCompleted;
		private string _star1, _star2, _star3;
		private string _priority;
		private string _isCompletedText, _isCompletedIcon;
		private long _idActivity;
		IEventAggregator _ea;
		private IActivityRepository _activityRepository;
		private MessageBoxResult msgResult;

		public MessageBoxResult MsgResult
		{
			get => msgResult;
			set
			{
				SetProperty(ref msgResult, value);
			}
		}
		public string TxtTituloCard
		{
			get
			{
				return _txtTituloCard;
			}

			set
			{
				SetProperty(ref _txtTituloCard, value);
			}
		}

		public string TxtDescriptionCard
		{
			get => _txtDescriptionCard;
			set
			{
				SetProperty(ref _txtDescriptionCard, value);
			}
		}

		public string DateFrom
		{
			get => _dateFrom;
			set
			{
				SetProperty(ref _dateFrom, value);
			}
		}
		public string DateTo
		{
			get => _dateTo;
			set
			{
				SetProperty(ref _dateTo, value);
			}
		}
		public bool IsCompleted
		{
			get => _isCompleted;
			set
			{
				SetProperty(ref _isCompleted, value);
				LoadIsCompleted(IsCompleted);
			}
		}

		public string Star1
		{
			get => _star1;
			set
			{
				SetProperty(ref _star1, value);
			}
		}
		public string Star2
		{
			get => _star2;
			set
			{
				SetProperty(ref _star2, value);
			}
		}
		public string Star3
		{
			get => _star3;
			set
			{
				SetProperty(ref _star3, value);
			}
		}

		public string Priority
		{
			get => _priority;
			set
			{
				SetProperty(ref _priority, value);
				LoadRaitingPriority(Priority);
			}
		}

		public string IsCompletedText
		{
			get => _isCompletedText;
			set
			{
				SetProperty(ref _isCompletedText, value);
			}
		}
		public string IsCompletedIcon
		{
			get => _isCompletedIcon;
			set
			{
				SetProperty(ref _isCompletedIcon, value);
			}
		}
		public string UserName
		{
			get => _userName;
			set
			{
				SetProperty(ref _userName, value);
			}
		}
		public long IdActivity
		{
			get => _idActivity;
			set
			{
				SetProperty(ref _idActivity, value);
			}
		}

		//Commands
		public DelegateCommand ChangeActivityDataCommand { get; }
		public DelegateCommand DeleteActivityDataCommand { get; }
		//CONSTRUCTOR
		public ActivityCardViewModel(IEventAggregator ea)
		{
			_ea = ea;
			_activityRepository = new ActivityRepository();
			LoadRaitingPriority(Priority);
			ChangeActivityDataCommand = new DelegateCommand(ExecuteChangeActivityDataCommand);
			DeleteActivityDataCommand = new DelegateCommand(ExecuteDeleteActivityDataCommand);
		}

		private async void ExecuteDeleteActivityDataCommand()
		{

			MsgResult = MessageBox.Show("Se eliminara de la base de " +
				"datos la actividad", "Advertencia",
				MessageBoxButton.YesNo, MessageBoxImage.Warning);
			if (MsgResult == MessageBoxResult.Yes)
			{
				await _activityRepository.Delete(IdActivity);
				_ea.GetEvent<MessageSentEvent>().Publish("Refresh2");
			}
		}
		private void ExecuteChangeActivityDataCommand()
		{
			ActivityModel model = new ActivityModel();
			model.Id = IdActivity;
			model.User = UserName;
			model.Title = TxtTituloCard;
			model.Description = TxtDescriptionCard;
			model.DateFrom = Convert.ToDateTime(DateFrom);
			model.DateTo = Convert.ToDateTime(DateTo);
			model.Priority = Priority;
			model.IsCompleted = IsCompleted;
			var openChangeActivityView = new ChangeDataActivity();
			openChangeActivityView.Show();
			_ea.GetEvent<MessageActivityModel>().Publish(model);
			
		}
		private void LoadRaitingPriority(string casePriority)
		{
			switch (casePriority)
			{
				case "Baja prioridad":
					Star1 = "/Images/star_fill.png";
					Star2 = "/Images/star_nofill.png";
					Star3 = "/Images/star_nofill.png";
					break;
				case "Media prioridad":
					Star1 = "/Images/star_fill.png";
					Star2 = "/Images/star_fill.png";
					Star3 = "/Images/star_nofill.png";
					break;
				case "Alta prioridad":
					Star1 = "/Images/star_fill.png";
					Star2 = "/Images/star_fill.png";
					Star3 = "/Images/star_fill.png";
					break;
			}
		}

		private void LoadIsCompleted(bool isCompleted)
		{
			switch (isCompleted)
			{
				case true:
					IsCompletedIcon = "/Images/badge-check.png";
					IsCompletedText = "Completado";
					break;
				case false:
					IsCompletedIcon = "/Images/badge-nocheck.png";
					IsCompletedText = "NO Completado";
					break;
			}
		}
	}
}
