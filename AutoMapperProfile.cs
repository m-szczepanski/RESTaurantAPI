﻿using AutoMapper;
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
            CreateMap<EmployeeDto, EmployeeDto>();

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
            CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.Dish, opt => opt.MapFrom(src => src.DishId))
                .ForMember(dest => dest.Table, opt => opt.MapFrom(src => src.TableId));

            // User
            CreateMap<Menu, MenuDto>()
                .ForMember(dest => dest.DishName, opt => opt.MapFrom(src => src.Dish.DishName));
            CreateMap<MenuDto, Menu>()
                .ForMember(dest => dest.Dish, opt => opt.MapFrom(src => new Dish() { DishName = src.DishName }));

        }
    }
}