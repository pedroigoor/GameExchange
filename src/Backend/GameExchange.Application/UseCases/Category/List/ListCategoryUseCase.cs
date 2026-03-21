using GameExchange.Communication.Response;
using GameExchange.Domain.Repositories.Category;
using GameExchange.Domain.Repositories.Platform;
using GameExchange.Infrastructe.DataAccess.Repositories;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Application.UseCases.Category.List
{
    public class ListCategoryUseCase(ICategoryReadOnlyRepository categoryRepository) : IListCategoryUseCase
    {
        private readonly ICategoryReadOnlyRepository _categoryRepository = categoryRepository;

        public async Task<List<ResponseCategoryJson>> Execute()
        {
            var categories = await _categoryRepository.GetAll();

            return categories.Adapt<List<ResponseCategoryJson>>();
        }
    }
}
