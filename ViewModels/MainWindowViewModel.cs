using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using SIMMS.Models;
using SIMMS.Views;
using System;
using System.Security.Principal;
using System.Threading;

namespace SIMMS.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        IEventAggregator _ea;
        private bool _isEnableAlmacen, 
            _isEnableOperaciones, _isEnableFinanzas,
            _isEnableRecursosH;
		IContainerExtension _container;
		private readonly IRegionManager _regionManager;
		IRegion _region;
		public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
		}

		public bool IsEnableAlmacen
		{
			get
			{
				return _isEnableAlmacen;
			}

			set
			{
				SetProperty(ref _isEnableAlmacen , value);
			}
		}

		public bool IsEnableOperaciones
		{
			get
			{
				return _isEnableOperaciones;
			}

			set
			{
				SetProperty(ref _isEnableOperaciones , value);
			}
		}

		public bool IsEnableFinanzas
		{
			get
			{
				return _isEnableFinanzas;
			}

			set
			{
				SetProperty(ref _isEnableFinanzas, value);
			}
		}

		public bool IsEnableRecursosH
		{
			get
			{
				return _isEnableRecursosH;
			}

			set
			{
				SetProperty(ref _isEnableRecursosH, value);
			}
		}


		public DelegateCommand DataBaseCommmand { get; }
		public DelegateCommand<string> NavigateCommand { get; private set; }
		public MainWindowViewModel(IEventAggregator ea, IContainerExtension container, IRegionManager regionManager)
        {
            _ea = ea;
			_container = container;
			_regionManager = regionManager;
            _ea.GetEvent<MessageRole>().Subscribe(GetUserReceived);
			DataBaseCommmand = new DelegateCommand(ExecuteDataBaseCommand);
			NavigateCommand = new DelegateCommand<string>(ExecuteNavigateCommand);
		}

		private void ExecuteNavigateCommand(string navigatePath)
		{
			if (navigatePath != null)
				_regionManager.RequestNavigate("ContentRegion", navigatePath);
		}

		private void ExecuteDataBaseCommand()
		{
			_regionManager.RequestNavigate("ContentRegion", "DataBase");
		}

		private void GetUserReceived(Role role)
		{
            Thread.CurrentPrincipal = new GenericPrincipal(
                new GenericIdentity(role.Username), role.Rol);

			if (Thread.CurrentPrincipal.IsInRole("Admin"))
			{
				IsEnableAlmacen = true;
				IsEnableOperaciones = true;
				IsEnableFinanzas = true;
				IsEnableRecursosH = true;
			}
			else if (Thread.CurrentPrincipal.IsInRole("Almacen"))
			{
				IsEnableAlmacen=true;
				IsEnableOperaciones = true;
				IsEnableFinanzas = false;
				IsEnableRecursosH = false;
			}
			else if (Thread.CurrentPrincipal.IsInRole("Operaciones"))
			{
				IsEnableAlmacen=false;
				IsEnableOperaciones = true;
				IsEnableFinanzas = false;
				IsEnableRecursosH = false;
			}

		}
	}
}
