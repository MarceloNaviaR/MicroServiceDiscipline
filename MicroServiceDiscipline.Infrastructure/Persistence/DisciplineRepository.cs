using Dapper;
using MicroServiceDiscipline.Domain.Entities;
using MicroServiceDiscipline.Domain.Ports;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace MicroServiceDiscipline.Infrastructure.Persistence
{
    public class DisciplineRepository : IDisciplineRepository
    {
        private readonly string _connectionString;

        public DisciplineRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("La cadena de conexión 'DefaultConnection' no fue encontrada.");
        }

        public async Task<int> AddAsync(Discipline discipline)
        {
            using IDbConnection connection = new NpgsqlConnection(_connectionString);
            var sql = @"INSERT INTO public.disciplines (name, description, start_time, end_time, instructor_id, created_at, created_by) VALUES (@Name, @Description, @StartTime, @EndTime, @InstructorId, @CreatedAt, @CreatedBy) RETURNING id;";
            return await connection.ExecuteScalarAsync<int>(sql, discipline);
        }

        public async Task<bool> UpdateAsync(Discipline discipline)
        {
            using IDbConnection connection = new NpgsqlConnection(_connectionString);
            var sql = @"UPDATE public.disciplines SET name = @Name, description = @Description, start_time = @StartTime, end_time = @EndTime, instructor_id = @InstructorId, last_modification = @LastModification, last_modified_by = @LastModifiedBy WHERE id = @Id;";
            var affectedRows = await connection.ExecuteAsync(sql, discipline);
            return affectedRows > 0;
        }

        public async Task<Discipline?> GetByIdAsync(int id)
        {
            using IDbConnection connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Discipline>("SELECT * FROM public.disciplines WHERE id = @Id", new { Id = id });
        }

        public async Task<IEnumerable<Discipline>> GetAllAsync()
        {
            using IDbConnection connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryAsync<Discipline>("SELECT * FROM public.disciplines ORDER BY name");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using IDbConnection connection = new NpgsqlConnection(_connectionString);
            var affectedRows = await connection.ExecuteAsync("DELETE FROM public.disciplines WHERE id = @Id", new { Id = id });
            return affectedRows > 0;
        }
    }
}