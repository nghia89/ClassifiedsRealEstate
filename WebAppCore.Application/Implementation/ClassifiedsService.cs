using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCore.Application.Interfaces;
using WebAppCore.Application.Mappers;
using WebAppCore.Application.ViewModels.Blog;
using WebAppCore.Application.ViewModels.Common;
using WebAppCore.Data.Entities;
using WebAppCore.Data.Enums;
using WebAppCore.Infrastructure.Interfaces;
using WebAppCore.Repository.Interfaces;
using WebAppCore.Utilities.Constants;
using WebAppCore.Utilities.Dtos;
using WebAppCore.Utilities.Helpers;

namespace WebAppCore.Application.Implementation
{
	public class ClassifiedsService:IClassifiedsService
	{
		private readonly IRepository<Classifieds,int> _Classifieds;
		private readonly IRepository<Tag,string> _tagRepository;
		private readonly IRepository<ClassifiedsTag,int> _ClassifiedsTag;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IClassifiedsRepository _ClassifiedsRepository;

		public ClassifiedsService(IRepository<Classifieds,int> Classifieds,
			IRepository<ClassifiedsTag,int> ClassifiedsTag,
			IRepository<Tag,string> tagRepository,
			IClassifiedsRepository ClassifiedsRepository,
			IUnitOfWork unitOfWork)
		{
			_Classifieds = Classifieds;
			_ClassifiedsTag = ClassifiedsTag;
			_tagRepository = tagRepository;
			_ClassifiedsRepository = ClassifiedsRepository;
			_unitOfWork = unitOfWork;
		}

		public ClassifiedsViewModel Add(ClassifiedsViewModel ClassifiedsVM)
		{
			var classifieds = ClassifiedsVM.AddModel();
			List<ClassifiedsTag> ClassifiedsTag = new List<ClassifiedsTag>();
			classifieds.ClassifiedsTags = ClassifiedsTag;
			if(!string.IsNullOrEmpty(classifieds.Tags))
			{
				var tags = classifieds.Tags.Split(',');
				foreach(string t in tags)
				{
					var tagId = TextHelper.ToUnsignString(t);
					if(!_tagRepository.FindAll(x => x.Id == tagId).Any())
					{
						Tag tag = new Tag {
							Id = tagId,
							Name = t,
							Type = CommonConstants.BlogTag
						};
						_tagRepository.Add(tag);
					}

					var classifiedsTag = new ClassifiedsTag { TagId = tagId };
					classifieds.ClassifiedsTags.Add(classifiedsTag);
				}
			}
			_Classifieds.Add(classifieds);
			return ClassifiedsVM;
		}

		public void Delete(int id)
		{
			_Classifieds.Remove(id);
		}

		public List<ClassifiedsViewModel> GetAll()
		{
			return _Classifieds.FindAll(c => c.ClassifiedsTags)
				.Select(x => x.ToModel()).ToList();
		}

		public async Task<PagedResult<ClassifiedsViewModel>> GetAllPaging(string keyword,int page,int pageSize)
		{
			var query = await _Classifieds.GetAllAsyn();
			if(!string.IsNullOrEmpty(keyword))
				query = query.Where(x => x.Title.Contains(keyword)).ToList();

			int totalRow = query.Count();
			var data = query.OrderByDescending(x => x.DateCreated)
				.Skip((page - 1) * pageSize)
				.Take(pageSize);

			var paginationSet = new PagedResult<ClassifiedsViewModel>() {
				Results = data.Select(x => x.ToModel()).ToList(),
				CurrentPage = page,
				RowCount = totalRow,
				PageSize = pageSize,
			};

			return paginationSet;
		}

		public async Task<ClassifiedsViewModel> GetById(int id)
		{
			var data = await _Classifieds.GetAByIdIncludeAsyn(x => x.Id == id,a=>a.Type);

			return data.ToModel();
		}

		public void Save()
		{
			_unitOfWork.Commit();
		}

		public void Update(ClassifiedsViewModel blog)
		{
			_Classifieds.Update(blog.AddModel());
			if(!string.IsNullOrEmpty(blog.Tags))
			{
				string[] tags = blog.Tags.Split(',');
				foreach(string t in tags)
				{
					var tagId = TextHelper.ToUnsignString(t);
					if(!_tagRepository.FindAll(x => x.Id == tagId).Any())
					{
						Tag tag = new Tag {
							Id = tagId,
							Name = t,
							Type = CommonConstants.ProductTag
						};
						_tagRepository.Add(tag);
					}
					_ClassifiedsTag.RemoveMultiple(_ClassifiedsTag.FindAll(x => x.Id == blog.Id).ToList());
					ClassifiedsTag classifiedsTag = new ClassifiedsTag {
						ClassifiedsId = blog.Id,
						TagId = tagId
					};
					_ClassifiedsTag.Add(classifiedsTag);
				}
			}
		}

		public async Task<List<ClassifiedsViewModel>> GetLastest(int top)
		{
			var data = await _ClassifiedsRepository.GetLastest(top);

			return data.Select(x => x.ToModel()).ToList();
		}

		public async Task<List<ClassifiedsViewModel>> GetHotProduct(int top)
		{
			var data = await _Classifieds.FindAllAsync(x => x.Status == Status.Active && x.HomeFlag == true);
			return
				data.OrderByDescending(x => x.DateCreated)
				.Take(top)
				.Select(x => x.ToModel())
				.ToList();
		}

		public List<ClassifiedsViewModel> GetListPaging(int page,int pageSize,string sort,out int totalRow)
		{
			var query = _Classifieds.FindAll(x => x.Status == Status.Active);

			switch(sort)
			{
				case "popular":
					query = query.OrderByDescending(x => x.ViewCount);
					break;

				default:
					query = query.OrderByDescending(x => x.DateCreated);
					break;
			}

			totalRow = query.Count();

			return query.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.Select(x => x.ToModel()).ToList();
		}

		public List<string> GetListByName(string name)
		{
			return _Classifieds.FindAll(x => x.Status == Status.Active
			&& x.Title.Contains(name)).Select(y => y.Title).ToList();
		}

		public List<ClassifiedsViewModel> Search(string keyword,int page,int pageSize,string sort,out int totalRow)
		{
			var query = _Classifieds.FindAll(x => x.Status == Status.Active
			&& x.Title.Contains(keyword));

			switch(sort)
			{
				case "popular":
					query = query.OrderByDescending(x => x.ViewCount);
					break;

				default:
					query = query.OrderByDescending(x => x.DateCreated);
					break;
			}

			totalRow = query.Count();

			return query.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.Select(x => x.ToModel())
				.ToList();
		}

		public List<ClassifiedsViewModel> GetReatedBlogs(int id,int top)
		{
			return _Classifieds.FindAll(x => x.Status == Status.Active
				&& x.Id != id)
			.OrderByDescending(x => x.DateCreated)
			.Take(top)
			.Select(x => x.ToModel())
			.ToList();
		}

		public List<TagViewModel> GetListTagById(int id)
		{
			return _ClassifiedsTag.FindAll(x => x.ClassifiedsId == id,c => c.Tag)
				.Select(y => y.Tag)
				.Select(x => x.ToModel())
				.ToList();
		}

		public void IncreaseView(int id)
		{
			var product = _Classifieds.FindById(id);
			if(product.ViewCount.HasValue)
				product.ViewCount += 1;
			else
				product.ViewCount = 1;
		}

		public List<ClassifiedsViewModel> GetListByTag(string tagId,int page,int pageSize,out int totalRow)
		{
			var query = from p in _Classifieds.FindAll()
						join pt in _ClassifiedsTag.FindAll()
						on p.Id equals pt.ClassifiedsId
						where pt.TagId == tagId && p.Status == Status.Active
						orderby p.DateCreated descending
						select p;

			totalRow = query.Count();

			query = query.Skip((page - 1) * pageSize).Take(pageSize);

			var model = query
				.Select(x => x.ToModel());
			return model.ToList();
		}

		public TagViewModel GetTag(string tagId)
		{
			return _tagRepository.FindSingle(x => x.Id == tagId).ToModel();
		}

		public List<ClassifiedsViewModel> GetList(string keyword)
		{
			var query = !string.IsNullOrEmpty(keyword) ?
				_Classifieds.FindAll(x => x.Title.Contains(keyword)).ProjectTo<ClassifiedsViewModel>()
				: _Classifieds.FindAll().Select(x => x.ToModel());
			return query.ToList();
		}

		public List<TagViewModel> GetListTag(string searchText)
		{
			return _tagRepository.FindAll(x => x.Type == CommonConstants.ProductTag
			&& searchText.Contains(x.Name)).Select(x => x.ToModel()).ToList();
		}

		public List<ClassifiedsViewModel> RelatedBlog(int id,int top)
		{
			var getById = _Classifieds.FindById(id);
			return _Classifieds.FindAll(x => x.Id != id && x.Status == Status.Active).OrderByDescending(x => x.DateCreated)
				.Take(top).Select(x => x.ToModel()).ToList();
		}

		public async Task<PagedResult<ClassifiedsViewModel>> GetAllPaging(int id,string keyword,int page,int pageSize)
		{
			var query = await _Classifieds.GetAllAsyn(x=>x.Type);
			if(!string.IsNullOrEmpty(keyword))
				query = query.Where(x => x.Title.Contains(keyword)).ToList();
			if(id != 0)
				query = query.Where(x => x.TypeId.Equals(id)).ToList();

			int totalRow = query.Count();
			var data = query.OrderByDescending(x => x.DateCreated)
			.Skip((page - 1) * pageSize)
			.Take(pageSize);
			//var data = query.Select(x => x.ToModel()).OrderByDescending(x => x.DateCreated).ToList();

			var paginationSet = new PagedResult<ClassifiedsViewModel>() {
				Results = data.Select(x => x.ToModel()).ToList(),
				CurrentPage = page,
				RowCount = totalRow,
				PageSize = pageSize,
			};

			return paginationSet;
		}

		public async Task<PagedResult<ClassifiedsViewModel>> GetViewAll(string keyword,int page,int pageSize)
		{
			var (data, totalCount) = await _Classifieds.Paging(page,pageSize,x => x.Status == Status.Active,null);
			var paginationSet = new PagedResult<ClassifiedsViewModel>() {
				Results = data.Select(x => x.ToModel()).ToList(),
				CurrentPage = page,
				RowCount = (int)totalCount,
				PageSize = pageSize,
			};

			return paginationSet;
		}
	}
}
