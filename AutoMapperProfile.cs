using AutoMapper;
using RESTaurantAPI.DTOs;
using RESTaurantAPI.Models;

namespace RESTaurantAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Table
            CreateMap<Table, TableDto>();
            CreateMap<TableDto, Table>();

            // Employee
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();

            // Dish
            CreateMap<Dish, DishDto>();
            CreateMap<DishDto, Dish>();

            // Reservation
            CreateMap<Reservation, ReservationDto>()
                .ForMember(dest => dest.TableId, opt => opt.MapFrom(src => src.Table.Id));
            CreateMap<ReservationDto, Reservation>()
                .ForMember(dest => dest.Table, opt => opt.MapFrom(src => new Table() { Id = src.TableId }));

            //Order
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.DishId, opt => opt.MapFrom(src => src.Dish.Id))
                .ForMember(dest => dest.TableId, opt => opt.MapFrom(src => src.Table.Id));
                //.ForMember(dest => dest.DishId, opt => opt.MapFrom(src => src.Dish.Id))
                //.ForMember(dest => dest.TableId, opt => opt.MapFrom(src => src.Table.Id));
            // OrderDto to Order
            CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.Dish, opt => opt.MapFrom(src => src.DishId))
                .ForMember(dest => dest.Table, opt => opt.MapFrom(src => src.TableId));
                //.ForMember(dest => dest.Dish, opt => opt.MapFrom(src => new Dish() { Id = src.DishId }))
                //.ForMember(dest => dest.Table, opt => opt.MapFrom(src => new Table() { Id = src.TableId }));

            // Menu
            //CreateMap<Menu, MenuDto>()
            //.ForMember(dest => dest.DishName, opt => opt.MapFrom(src => src.Dish.DishName));
            //CreateMap<MenuDto, Menu>()
            //.ForMember(dest => dest.Dish, opt => opt.MapFrom(src => new Dish() { DishName = src.DishName }));
            CreateMap<Menu, MenuDto>()
                .ForMember(dest => dest.Dishes, opt => opt.MapFrom(src => src.Dishes));
            CreateMap<MenuDto, Menu>()
                .ForMember(dest => dest.Dishes, opt => opt.MapFrom(src => src.Dishes));


        }
    }
}