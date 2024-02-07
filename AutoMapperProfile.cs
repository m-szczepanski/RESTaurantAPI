using AutoMapper;
using RESTaurantAPI.DTOs;
using RESTaurantAPI.Models;

namespace RESTaurantAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Table to TableDTO
            CreateMap<Table, TableDto>();
            // DTO to Table
            CreateMap<TableDto, Table>();

            // Employee to EmployeeDTO
            CreateMap<Employee, EmployeeDto>();
            //  EmployeeDTO to Employee
            CreateMap<EmployeeDto, Employee>();

            // Dish
            CreateMap<Dish, DishDto>();
            CreateMap<DishDto, Dish>();

            // Reservation to Reservation
            CreateMap<Reservation, ReservationDto>()
                .ForMember(dest => dest.TableId, opt => opt.MapFrom(src => src.Table.Id));
            // ReservationDTO to Reservation
            CreateMap<ReservationDto, Reservation>()
                .ForMember(dest => dest.Table, opt => opt.MapFrom(src => src.TableId));

            //Order to OrderDTO
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.DishId, opt => opt.MapFrom(src => src.Dish.Id))
                .ForMember(dest => dest.TableId, opt => opt.MapFrom(src => src.Table.Id));
            // OrderDTO to Order
            CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.Dish, opt => opt.MapFrom(src => src.DishId))
                .ForMember(dest => dest.Table, opt => opt.MapFrom(src => src.TableId));

            //Menu to MenuDTO
            CreateMap<Menu, MenuDto>()
                .ForMember(dest => dest.Dishes, opt => opt.MapFrom(src => src.Dishes));
            //MenuDTO to Menu
            CreateMap<MenuDto, Menu>()
                .ForMember(dest => dest.Dishes, opt => opt.MapFrom(src => src.Dishes));


        }
    }
}