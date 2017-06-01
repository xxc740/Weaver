using AutoMapper;
using Domain;
using Service.MenuService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    /// <summary>
    /// domain模型与servicemodel的映射关系
    /// </summary>
    public class WeaverMapper
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Menu, MenuServiceModel>();
                cfg.CreateMap<MenuServiceModel, Menu>();
            });
        }
    }
}
