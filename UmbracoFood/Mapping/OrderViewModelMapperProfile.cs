using System;
using System.Collections.Generic;
using AutoMapper;
using UmbracoFood.Core.Extensions;
using UmbracoFood.Core.Models;
using UmbracoFood.Extensions;
using UmbracoFood.ViewModels;

namespace UmbracoFood.Mapping
{
    public class OrderViewModelMapperProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Owner, o => o.MapFrom(s => s.Owner))
                .ForMember(d => d.Deadline, o => o.MapFrom(s => s.Deadline))
                .ForMember(d => d.EstimatedDeliveryTime, o => o.MapFrom(s => s.EstimatedDeliveryTime))
                .ForMember(d => d.MealsCount, o => o.MapFrom(s => s.OrderedMeals.Sum(om => om.Count)))
                .ForMember(d => d.StatusName, o => o.MapFrom(s => s.Status.GetDescription()))
                .ForMember(d => d.StatusId, o => o.MapFrom(s => (int) s.Status))
                .ForMember(d => d.RestaurantName, o => o.MapFrom(s => s.Restaurant.Name));

            CreateMap<CreateOrderViewModel, Order>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Deadline, o => o.MapFrom(s => s.Deadline))
                .ForMember(d => d.EstimatedDeliveryTime, o => o.Ignore())
                .ForMember(d => d.OrderedMeals, o => o.MapFrom(s => s))
                .ForMember(d => d.Status, o => o.UseValue(OrderStatus.InProgress))
                .ForMember(d => d.Restaurant, o => o.MapFrom(s => new Restaurant() { ID = s.SelectedRestaurantId }))
                .ForMember(d => d.AccountNumber, o => o.MapFrom(s => s.AccountNumber));
            

            CreateMap<CreateOrderViewModel, List<OrderedMeal>>()
                .IgnoreAllUnmapped()
               .ConstructUsing(context =>
                {
                    var order = context.SourceValue as CreateOrderViewModel;

                    var orderMeals = new List<OrderedMeal>();
                    foreach (var meal in order.Meals)
                    {
                        orderMeals.Add(new OrderedMeal()
                        {
                            MealName = meal.Name,
                            Price = meal.Price,
                            PurchaserName = order.Owner,
                            Count = meal.Count
                        });
                    }

                    return orderMeals;
                });

            CreateMap<CreateOrderViewModel, OrderedMeal>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.PurchaserName, o => o.MapFrom(s => s.Owner));

            CreateMap<CreateOrderMeal, OrderedMeal>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.MealName, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Price, o => o.MapFrom(s => s.Price));

            CreateMap<AddMealViewModel, OrderedMeal>()
                .IgnoreAllUnmapped()
                .ForMember(d => d.OrderId, o => o.MapFrom(s => s.OrderId))
                .ForMember(d => d.MealName, o => o.MapFrom(s => s.MealName))
                .ForMember(d => d.Count, o => o.MapFrom(s => s.Count))
                .ForMember(d => d.Price, o => o.MapFrom(s => s.Price))
                .ForMember(d => d.PurchaserName, o => o.MapFrom(s => s.Person));

            CreateMap<OrderedMeal, MealViewModel>()
                .ForMember(d => d.Person, o => o.MapFrom(s => s.PurchaserName));
        }
    }
}