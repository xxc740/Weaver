using AutoMapper;
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
            var menus = _menuRepository.LoadPageList(startPage,pageSize,)
        }
    }
}
