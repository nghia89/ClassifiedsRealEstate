using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppCore.Application.Interfaces;
using WebAppCore.Application.Mappers;
using WebAppCore.Application.ViewModels.General;
using WebAppCore.Data.Entities;
using WebAppCore.Data.Enums;
using WebAppCore.Infrastructure.Interfaces;
using WebAppCore.Repository.Interfaces;

namespace WebAppCore.Application.Implementation
{
	public class TypeService:ITypeService
	{
		private IRepository<Data.Entities.Type,int> _typeRepository;
		private IRepository<Classifieds,int> _classifiedsRepository;
		private IUnitOfWork _unitOfWork;
		private ITypeRepository _typeServiceRepository;

		public TypeService(IRepository<Data.Entities.Type,int> typeRepository,
			IRepository<Classifieds,int> classifiedsRepository,IUnitOfWork unitOfWork,
			ITypeRepository typeServiceRepository)
		{
			this._typeRepository = typeRepository;
			this._classifiedsRepository = classifiedsRepository;
			this._unitOfWork = unitOfWork;
			this._typeServiceRepository = typeServiceRepository;
		}

		public TypeViewModel Add(TypeViewModel typeViewModel)
		{
			var productCategory = typeViewModel.AddModel();
			_typeRepository.Add(productCategory);
			return typeViewModel;
		}

		public void Delete(int id)
		{
			_typeRepository.Remove(id);
		}

		public async Task<List<TypeViewModel>> GetAll()
		{
			var data =await _typeRepository.GetAllAsyn();
			return data.Select(a => a.ToModel()).ToList();
		}

		public List<TypeViewModel> GetAll(string keyword)
		{
			if(!string.IsNullOrEmpty(keyword))
				return _typeRepository.FindAll(x => x.Name.Contains(keyword))
					.OrderBy(x => x.ParentId).Select(x => x.ToModel()).ToList();
			else
				return _typeRepository.FindAll().OrderBy(x => x.ParentId)
					.Select(x => x.ToModel())
					.ToList();
		}

		public List<TypeViewModel> GetAllByParentId(int parentId)
		{
			return _typeRepository.FindAll(x => x.Status == Status.Active
			&& x.ParentId == parentId)
			 .Select(x => x.ToModel())
			 .ToList();
		}

		public async Task<TypeViewModel> GetById(int id)
		{

			var data = await _typeServiceRepository.GetById(id);
			return data.ToModel();
		}

		public async Task<List<TypeViewModel>> GetHomeCategories(int top)
		{
			var listData = await _typeServiceRepository.GetHomeCategories(top);
			return listData.Select(a => a.ToModel()).ToList(); ;
		}

		public void ReOrder(int sourceId,int targetId)
		{
			var source = _typeRepository.FindById(sourceId);
			var target = _typeRepository.FindById(targetId);
			int tempOrder = source.SortOrder;
			source.SortOrder = target.SortOrder;
			target.SortOrder = tempOrder;

			_typeRepository.Update(source);
			_typeRepository.Update(target);
		}

		public void Save()
		{
			_unitOfWork.Commit();
		}

		public void Update(TypeViewModel typeViewModel)
		{
			var productCategory = typeViewModel.AddModel();
			_typeRepository.Update(productCategory);
		}

		public void UpdateParentId(int sourceId,int targetId,Dictionary<int,int> items)
		{
			var sourceCategory = _typeRepository.FindById(sourceId);
			sourceCategory.ParentId = targetId;
			_typeRepository.Update(sourceCategory);

			//Get all sibling
			var sibling = _typeRepository.FindAll(x => items.ContainsKey(x.Id));
			foreach(var child in sibling)
			{
				child.SortOrder = items[child.Id];
				_typeRepository.Update(child);
			}
		}
	}
}
