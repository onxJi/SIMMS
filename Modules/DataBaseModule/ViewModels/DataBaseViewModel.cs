using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SIMMS.Models;
using SIMMS.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMMS.Modules.DataBaseModule.ViewModels
{
	public class DataBaseViewModel: BindableBase
	{
		private ObservableCollection<BindableBase> _productCardObservableCollection;
		private IProductRepository _productRepository;
		private IRegionManager _regionManager;
		public ObservableCollection<BindableBase> ProductCardObservableCollection
		{
			get
			{
				return _productCardObservableCollection;
			}

			set
			{
				SetProperty(ref 
					_productCardObservableCollection, 
					value);
			}
		}

		public DelegateCommand<string> CreateProductCommand { get; }
		public DataBaseViewModel(IRegionManager regionManager) {
			_regionManager = regionManager;
			_productRepository = new ProductRepository();
			CreateProductCommand = new DelegateCommand<string>(
				ExecuteCreateProductCommand);
		}

		private void ExecuteCreateProductCommand(string obj)
		{
			if (obj != null)
				_regionManager.RequestNavigate("ContentCreateDBRegion", obj);
		}

		private async Task LoadDataBaseProducts()
		{
			var product = await _productRepository.GetAllAsync();
			if(product != null)
			{
				var dataBaseCard = product.Select(item => new DataBaseCardViewModel()
				{

				}).ToList();
				ProductCardObservableCollection = new 
					ObservableCollection<BindableBase>(dataBaseCard);
			}
		}
		
	}
}
