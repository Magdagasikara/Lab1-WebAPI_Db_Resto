﻿using AutoMapper;
using Lab1_WebAPI_Db_Resto.Data.Repositories.IRepositories;
using Lab1_WebAPI_Db_Resto.Models;
using Lab1_WebAPI_Db_Resto.Models.DTOs;
using Lab1_WebAPI_Db_Resto.Models.ViewModels;

namespace Lab1_WebAPI_Db_Resto.Services.IServices
{
    public class MealServices : IMealServices
    {
        private readonly IMealRepository _mealRepo;
        private readonly IMapper _mapper;

        public MealServices(IMealRepository mealRepo, IMapper mapper)
        {
            _mealRepo = mealRepo;
            _mapper = mapper;
        }
        public async Task AddMealAsync(MealDto meal)
        {
            try
            {
                var newMeal = _mapper.Map<Meal>(meal);
                await _mealRepo.AddMealAsync(newMeal);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding meal in service", ex);
            }
        }

        public async Task DeleteMealByIdAsync(int mealId)
        {
            try
            {
                await _mealRepo.DeleteMealByIdAsync(mealId);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting meal in service", ex);
            }
        }

        public async Task<IEnumerable<MealListVM>> GetAllMealsAsync()
        {
            try
            {
                var meals = await _mealRepo.GetAllMealsAsync();

                return _mapper.Map<List<MealListVM>>(meals);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting meals in service", ex);
            }
        }

        public async Task<MealListVM> GetMealByIdAsync(int mealId)
        {
            try
            {
                var meal = await _mealRepo.GetMealByIdAsync(mealId);
                return _mapper.Map<MealListVM>(meal);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting meal in service", ex);
            }
        }

        public async Task UpdateMealAsync(MealUpdateDto meal)
        {
            try
            {
                var updatedMeal = _mapper.Map<Meal>(meal);
                await _mealRepo.UpdateMealAsync(updatedMeal);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating meal in service", ex);
            }
        }
    }
}
