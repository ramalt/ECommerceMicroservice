using AutoMapper;
using CourseApp.Catalog.Api.Config;
using CourseApp.Catalog.Api.Dtos.Category;
using CourseApp.Catalog.Api.Models;
using CourseApp.Shared.Dtos;
using MongoDB.Driver;

namespace CourseApp.Catalog.Api.Services;

public class CategoryService : ICategoryService
{
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);

        _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        _mapper = mapper;
    }

    public async Task<Response<CategoryDto>> CreateAsync(CategoryCreateDto categoryCreateDto)
    {
        var mappedCategory = _mapper.Map<Category>(categoryCreateDto);
        try
        {
            await _categoryCollection.InsertOneAsync(mappedCategory);
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(mappedCategory),
                                                 statusCode: 201);
        }
        catch (Exception e)
        {
            var errorMessages = new List<string> { e.Message };
            return Response<CategoryDto>.Fail(errors: errorMessages,
                                              statusCode: 500);
        }

    }

    public async Task<Response<List<CategoryDto>>> GetAllAsync()
    {
        var categories = await _categoryCollection.Find(category => true)
                                                  .ToListAsync();

        return Response<List<CategoryDto>>.Success(data: _mapper.Map<List<CategoryDto>>(categories),
                                                   statusCode: 200);
    }

    public async Task<Response<CategoryDto>> GetByIdAsync(string id)
    {
        var category = await _categoryCollection.Find(category => category.Id == id)
                                                .FirstOrDefaultAsync();

        if (category == null)
        {
            return Response<CategoryDto>.Fail(
                                              error: $"Category not found with id {id}",
                                              statusCode: 404);
        }

        return Response<CategoryDto>.Success(data: _mapper.Map<CategoryDto>(category),
                                             statusCode: 200);
    }


}
