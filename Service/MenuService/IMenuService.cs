using System;
using System.Collections.Generic;
using System.Text;

namespace Service.MenuService
{
    public interface IMenuService
    {
        List<MenuServiceModel> GetAllList();
        List<MenuServiceModel> GetMenuByParent(Guid parentId, int startPage, int pageSize, out int rowCount);
        bool InsertOrUpdate(MenuServiceModel model);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="Ids"></param>
        void DeleteBatch(List<Guid> Ids);
        void Delete(Guid Id);

        MenuServiceModel Get(Guid Id);
        //List<MenuServiceModel> GetMenuByUser(Guid userId);
    }
}
