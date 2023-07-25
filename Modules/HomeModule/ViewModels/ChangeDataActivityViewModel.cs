using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using SIMMS.Models;
using SIMMS.Modules.HomeModule.Views;
using SIMMS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace SIMMS.Modules.HomeModule.ViewModels
{
	public class ChangeDataActivityViewModel : BindableBase
	{
		private string _activityTitle, _activityDescription, _priorityValue;
		private bool _isCompleted;
		private DateTime _dateFrom;
		private DateTime _dateTo;
		private string _userName, _idString;
		private IEnumerable<string> valuesPriority = new List<string> { "Baja prioridad",
		"Media prioridad","Alta prioridad"};
		private IActivityRepository _activityRepository;
		IEventAggregator _ea;
		private readonly ChangeDataActivity _view;
		private long _idActivity;
		private string _errorMessage;

		#region FIELDS
		public string ErrorMessage
		{
			get => _errorMessage;
			set => SetProperty(ref _errorMessage, value);
		}
		public string ActivityTitle
		{
			get => _activityTitle;
			set
			{
				SetProperty(ref _activityTitle, value);
			}
		}
		public string ActivityDescription
		{
			get => _activityDescription;
			set
			{
				SetProperty(ref _activityDescription, value);
			}
		}
		public string PriorityValue
		{
			get => _priorityValue;
			set
			{
				SetProperty(ref _priorityValue, value);
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
		public IEnumerable<string> ValuesPriority
		{
			get => valuesPriority;
			set
			{
				SetProperty(ref valuesPriority, value);
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
		public string IdString
		{
			get => _idString;
			set
			{
				SetProperty(ref _idString, value);
			}
		}

		#endregion
		//Commands
		public DelegateCommand ChangeActivityDataCommand
		{
			get;
		}

		public ChangeDataActivityViewModel(IEventAggregator ea)
		{
			_ea = ea;
			_activityRepository = new ActivityRepository();
			
			ChangeActivityDataCommand = new DelegateCommand(ExecuteChangeActivityDataCommand);
			_ea.GetEvent<MessageActivityModel>().Subscribe(GetActivityModel);
		}

		private void GetActivityModel(ActivityModel model)
		{
			IdActivity = model.Id;
			IdString = IdActivity.ToString();
			UserName = model.User;
			ActivityTitle = model.Title;
			ActivityDescription = model.Description;
			DateFrom = model.DateFrom;
			DateTo = model.DateTo;
			PriorityValue = model.Priority;
			IsCompleted = model.IsCompleted;
		}

		private async void ExecuteChangeActivityDataCommand()
		{
			var objActivity = new ActivityModel();
			objActivity.Id = IdActivity;
			objActivity.Title = ActivityTitle;
			objActivity.Description = ActivityDescription;
			objActivity.DateFrom = DateFrom;
			objActivity.DateTo = DateTo;
			objActivity.Priority = PriorityValue;
			objActivity.IsCompleted = IsCompleted;
			await _activityRepository.Update(objActivity);
			ErrorMessage = "Se ha cambiado la Actividad\n"+
				"CIERRE LA VENTANA PARA VER LOS CAMBIOS";
			await Task.Delay(100);
			_ea.GetEvent<MessageSentEvent>().Publish("Refresh");

		}


	}
}
