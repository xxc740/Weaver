using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.MenuService;
using Weaver.Models;

namespace Weaver.Controllers
{
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取功能树Json数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetMenuTreeData()
        {
            var menus = _menuService.GetAllList();
            List<TreeModel> treeModel = new List<TreeModel>();
            foreach(var menu in menus)
            {
                treeModel.Add(new TreeModel()
                {
                    Id = menu.Id.ToString(),
                    Text = menu.Name,
                    ParentId = menu.ParentId == Guid.Empty ? "#" : menu.ParentId.ToString()
                });
            }

            return Json(treeModel);
        }

        /// <summary>
        /// 获取子级功能列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="startPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IActionResult GetMenuByParent(Guid parentId,int startPage,int pageSize)
        {
            int rowCount = 0;
            var result = _menuService.GetMenuByParent(parentId, startPage, pageSize, out rowCount);

            return Json(new
            {
                rowCount = rowCount,
                pageCount = Math.Ceiling(Convert.ToDecimal(rowCount)/pageSize),
                rows = result
            });
        }

        public IActionResult Edit(MenuServiceModel data)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    Result = "Faild",
                    Message = GetModelStateError()
                });
            }
            if (_menuService.InsertOrUpdate(data))
            {
                return Json(new { Result = "Success" });
            }

            return Json(new { Result = "Faild" });
        }

        public IActionResult DeleteMuti(string ids)
        {
            try
            {
                string[] idArray = ids.Split(',');
                List<Guid> delIds = new List<Guid>();
                foreach(string id in idArray)
                {
                    delIds.Add(Guid.Parse(id));
                }
                _menuService.DeleteBatch(delIds);
                return Json(new {
                    Result = "Success"
                });
            }catch(Exception ex)
            {
                return Json(new
                {
                    Result ="Faild",
                    Message = ex.Message
                });
            }
        }

        public IActionResult Delete(Guid id)
        {
            try
            {
                _menuService.Delete(id);
                return Json(new {
                    Result = "Success"
                });
            }catch(Exception ex)
            {
                return Json(new
                {
                    Result = "Falid",
                    Message = ex.Message
                });
            }
        }

        public ActionResult Get(Guid id)
        {
            var data = _menuService.Get(id);
            return Json(data);
        }
    }
}