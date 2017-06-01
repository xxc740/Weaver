using AutoMapper;
using Domain;
using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.MenuService
{
    public class MenuService:IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IUserRepository _userRepository;

        public MenuService(IMenuRepository menuRepository,IUserRepository userRepository)
        {
            _menuRepository = menuRepository;
            _userRepository = userRepository;
        }

        public List<MenuServiceModel> GetAllList()
        {
            var menus = _menuRepository.GetAllList().OrderBy(i => i.SerialNumber);
            return Mapper.Map<List<MenuServiceModel>>(menus);
        }

        public List<MenuServiceModel> GetMenuByParent(Guid parentId,int startPage, int pageSize,out int rowCount)
        {
            var menus = _menuRepository.LoadPageList(startPage, pageSize, out rowCount, i => i.ParentId == parentId, i => i.SerialNumber);
            return Mapper.Map<List<MenuServiceModel>>(menus);
        }

        public bool InsertOrUpdate(MenuServiceModel model)
        {
            var menu = _menuRepository.InsertOrUpdate(Mapper.Map<Menu>(model));
            return menu == null ? false : true;
        }

        public void DeleteBatch(List<Guid> Ids)
        {
            _menuRepository.Delete(i => Ids.Contains(i.Id));
        }

        public void Delete(Guid Id)
        {
            _menuRepository.Delete(Id);
        }

        public MenuServiceModel Get(Guid Id)
        {
            return Mapper.Map<MenuServiceModel>(_menuRepository.Get(Id));
        }

        //public List<MenuServiceModel> GetMenusByUser(Guid userId)
        //{
        //    List<MenuServiceModel> result = new List<MenuServiceModel>();
        //    var allMenus = _menuRepository.GetAllList(i => i.Type == 0).OrderBy(i => i.SerialNumber);
        //    if (userId == Guid.Empty)   //超级管理员
        //        return Mapper.Map<List<MenuServiceModel>>(allMenus);
        //}
    }
}
