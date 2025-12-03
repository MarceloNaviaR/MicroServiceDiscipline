using Dapper;
using Npgsql;
using MicroServiceDiscipline.Domain.Entities;
using MicroServiceDiscipline.Domain.Ports;
using MicroServiceDiscipline.Infrastructure.Provider;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceDiscipline.Infrastructure.Persistence
{
    public class DisciplineRepository : IDisciplineRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<DisciplineRepository> _logger;

        public DisciplineRepository(IDisciplineConnectionProvider connectionProvider, ILogger<DisciplineRepository> logger)
        {
            _connectionString = connectionProvider.GetConnectionString();
            _logger = logger;
        }

        // ============================================================
        // GET ALL
        // ============================================================
        public async Task<IEnumerable<Discipline>> GetAllAsync()
        {
            using var conn = new NpgsqlConnection(_connectionString);

            // Eliminadas referencias a LastModification/By
            const string sql = @"
                SELECT 
                    id, 
                    name, 
                    id_user AS UserId, 
                    start_time AS StartTime, 
                    end_time AS EndTime, 
                    is_active AS IsActive, 
                    created_at AS CreatedAt, 
                    created_by AS CreatedBy
                FROM disciplines 
                WHERE is_active = true;";

            return await conn.QueryAsync<Discipline>(sql);
        }

        // ============================================================
        // GET BY ID
        // ============================================================
        public async Task<Discipline?> GetByIdAsync(int id)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            const string sql = @"
                SELECT 
                    id, 
                    name, 
                    id_user AS UserId, 
                    start_time AS StartTime, 
                    end_time AS EndTime, 
                    is_active AS IsActive, 
                    created_at AS CreatedAt, 
                    created_by AS CreatedBy
                FROM disciplines 
                WHERE id = @id;";

            return await conn.QuerySingleOrDefaultAsync<Discipline>(sql, new { id });
        }

        // ============================================================
        // ADD (CREATE)
        // ============================================================
        public async Task<int> AddAsync(Discipline discipline)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            using var transaction = await conn.BeginTransactionAsync();

            try
            {
                discipline.CreatedAt = DateTime.UtcNow;
                discipline.IsActive = true;

                if (string.IsNullOrEmpty(discipline.CreatedBy)) discipline.CreatedBy = "System";

                // SQL limpio de LastModification
                const string sql = @"
                    INSERT INTO disciplines (
                        name, id_user, start_time, end_time, is_active, 
                        created_at, created_by
                    )
                    VALUES (
                        @Name, @UserId, @StartTime, @EndTime, @IsActive, 
                        @CreatedAt, @CreatedBy
                    )
                    RETURNING id;";

                var newId = await conn.ExecuteScalarAsync<int>(sql, discipline, transaction);
                await transaction.CommitAsync();

                return newId;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, $"Error creando disciplina {discipline.Name}");
                throw;
            }
        }

        // ============================================================
        // UPDATE
        // ============================================================
        public async Task<bool> UpdateAsync(Discipline discipline)
        {
            using var conn = new NpgsqlConnection(_connectionString);

            // 🛑 AQUÍ ESTABA EL ERROR: Se eliminaron las asignaciones a LastModification

            const string sql = @"
                UPDATE disciplines
                SET
                    name = @Name,
                    id_user = @UserId,
                    start_time = @StartTime,
                    end_time = @EndTime,
                    is_active = @IsActive
                WHERE id = @Id;";

            var affectedRows = await conn.ExecuteAsync(sql, discipline);
            return affectedRows > 0;
        }

        // ============================================================
        // DELETE (Soft Delete)
        // ============================================================
        public async Task<bool> DeleteAsync(int id)
        {
            using var conn = new NpgsqlConnection(_connectionString);

            // 🛑 AQUÍ TAMBIÉN: Solo actualizamos is_active, nada de fechas de modificación
            const string sql = @"
                UPDATE disciplines
                SET is_active = false
                WHERE id = @id;";

            var affectedRows = await conn.ExecuteAsync(sql, new { id });
            return affectedRows > 0;
        }
    }
}