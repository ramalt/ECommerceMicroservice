using System.Data;
using System.Runtime.CompilerServices;
using CourseApp.Shared.Dtos;
using Dapper;
using Npgsql;

namespace CourseApp.Discount.Api.Services;

public class DiscountService : IDiscountService
{
    private readonly IConfiguration _config;
    private readonly IDbConnection _connection;

    public DiscountService(IConfiguration config)
    {
        _config = config;
        _connection = new NpgsqlConnection(_config.GetConnectionString("PostgreSQL"));
    }

    public async Task<Response<NoContent>> Delete(int id)
    {
        var status = await _connection.ExecuteAsync("DELETE FROM Discounts WHERE id=@Id", new {Id = id});
        if(status < 0 )
            return Response<NoContent>.Fail(404, "Discount not found");

        return Response<NoContent>.Success(204);
    }

    public async Task<Response<List<Models.Discount>>> GetAll()
    {
        var discounts = await _connection.QueryAsync<Models.Discount>("SELECT * FROM Discounts");

        return Response<List<Models.Discount>>.Success(discounts.ToList(), 200);
    }

    public async Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId)
    {
        var discount = (await _connection.QueryAsync<Models.Discount>("SELECT * FROM Discounts WHERE userid=@UserId AND code=@Code", new{
            UserId = userId,
            Code = code
        })).FirstOrDefault();

        if(discount == null)
            return Response<Models.Discount>.Fail(404, "Discount not found");

        return Response<Models.Discount>.Success(discount, 200);
    }

    public async Task<Response<Models.Discount>> GetById(int id)
    {
        var discount =( await _connection.QueryAsync<Models.Discount>("SELECT * FROM Discounts WHERE id=@Id", new {Id = id})).SingleOrDefault();

        if(discount == null)
            return Response<Models.Discount>.Fail(404, "Discount not found");

        return Response<Models.Discount>.Success(discount, 200);
    }

    public async Task<Response<NoContent>> Save(Models.Discount discount)
    {
        var status = await _connection.ExecuteAsync("INSERT INTO Discounts (userid, rate, code) VALUES (@UserId, @Rate, @Code)", discount);
        if(status < 0 )
            return Response<NoContent>.Fail(404, "Discount not found");

        return Response<NoContent>.Success(204);
    }

    public async Task<Response<NoContent>> Update(Models.Discount discount)
    {
        var status = await _connection.ExecuteAsync("UPDATE Discounts SET userid=@UserId, code=@Code, rate=@Rate WHERE id=@Id", new{
            Id = discount.Id,
            UserId = discount.UserId,
            Code = discount.Code,
            Rate = discount.Rate
        });
        if(status < 0 )
            return Response<NoContent>.Fail(404, "Discount not found");

        return Response<NoContent>.Success(204);
    }
}
