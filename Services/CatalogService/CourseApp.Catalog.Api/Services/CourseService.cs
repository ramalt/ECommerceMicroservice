using CatalogService.CourseApp.Catalog.Api.Config;
using CatalogService.CourseApp.Catalog.Api.Dtos.Course;
using CatalogService.CourseApp.Catalog.Api.Models;
using CourseApp.Shared.Dtos;
using AutoMapper;
using MongoDB.Driver;

namespace CatalogService.CourseApp.Catalog.Api.Services;

public class CourseService : ICourseService
{
    private readonly IMongoCollection<Course> _courseCollection;
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);

        _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
        _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);

        _mapper = mapper;
    }

    public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
    {
        var newCourse = _mapper.Map<Course>(courseCreateDto);

        newCourse.CreatedDate = DateTime.Now;

        try
        {
            await _courseCollection.InsertOneAsync(newCourse);

            var mappedCourse = _mapper.Map<CourseDto>(newCourse);
            return Response<CourseDto>.Success(data: mappedCourse, statusCode: 201);
        }
        catch (System.Exception e)
        {
            var errorMessages = new List<string> { e.Message };
            return Response<CourseDto>.Fail(errors: errorMessages,
                                              statusCode: 500);
        }
    }

    public async Task<Response<List<CourseDto>>> GetAllAsync()
    {
        var courses = await _courseCollection.Find(course => true)
                                                 .ToListAsync();

        if (courses.Any())
        {
            // TODO: foreach çok sağlıklı değil, Agregation join yapılmalı
            foreach (var course in courses)
                course.Category = await _categoryCollection.Find(c => c.Id == course.CategoryId).FirstAsync();

            return Response<List<CourseDto>>.Success(data: _mapper.Map<List<CourseDto>>(courses),
                                                       statusCode: 200);
        }
        else
        {
            return new Response<List<CourseDto>>();
        }

    }

    public async Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string id)
    {
        var courses = await _courseCollection.Find(course => course.UserId == id)
                                               .ToListAsync();
        if (courses.Any())
        {
            // TODO: foreach yerine Aggregation join 
            foreach (var course in courses)
            {
                course.Category = await _categoryCollection.Find(c => c.Id == course.CategoryId).FirstAsync();
            }
        }
        else
        {
            courses = new List<Course>();
        }

        var courseMapped = _mapper.Map<List<CourseDto>>(courses);
        return Response<List<CourseDto>>.Success(data: courseMapped,
                                             statusCode: 200);
    }

    public async Task<Response<CourseDto>> GetByIdAsync(string id)
    {
        var course = await _courseCollection.Find(course => course.Id == id)
                                                .FirstOrDefaultAsync();

        if (course == null)
        {
            return Response<CourseDto>.Fail(
                                              error: $"course not found with id {id}",
                                              statusCode: 404);
        }

        course.Category = await _categoryCollection.Find(c => c.Id == course.CategoryId).FirstAsync();
        return Response<CourseDto>.Success(data: _mapper.Map<CourseDto>(course),
                                             statusCode: 200);
    }

    public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
    {
        var updatedCourse = _mapper.Map<Course>(courseUpdateDto);

        try
        {
            await _courseCollection.FindOneAndReplaceAsync(c => c.Id == courseUpdateDto.Id, updatedCourse);
            return new Response<NoContent>();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task<Response<NoContent>> DeleteAsync(string id)
    {
        try
        {
            await _courseCollection.DeleteOneAsync(c => c.Id == id);
            return new Response<NoContent>();
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
