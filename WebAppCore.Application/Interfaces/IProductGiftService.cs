using System;
using System.Collections.Generic;
using System.Text;
using WebAppCore.Application.ViewModels.Blog;
using WebAppCore.Utilities.Dtos;

namespace WebAppCore.Application.Interfaces
{
    public interface IProductGiftService: IDisposable
    {
        void Add(ProductGiftViewModel pageVm);
        void Update(ProductGiftViewModel pageVm);
        void Delete(int id);
        List<ProductGiftViewModel> GetAll();
        PagedResult<ProductGiftViewModel> GetAllPaging(string keyword, int page, int pageSize);
        ProductGiftViewModel GetByAlias(string alias);
        ProductGiftViewModel GetById(int id);
        void SaveChanges();
    }
}
