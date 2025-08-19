using System.Data;
using Dapper;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using TaskGrpcApi.Models;
using TaskGrpcApi.Protos;

namespace TaskGrpcApi.Services
{
    [Authorize]
    public class TaskService : TaskGrpcApi.Protos.TaskService.TaskServiceBase
    {
        private readonly IDbConnection _db;

        public TaskService(IDbConnection db)
        {
            _db = db;
        }

        public override async Task<TaskResponse> CreateTask(CreateTaskRequest request, ServerCallContext context)
        {
            var sql = "INSERT INTO Tasks (Title, Description, IsCompleted) VALUES (@Title, @Description, 0); SELECT last_insert_rowid();";
            var id = await _db.ExecuteScalarAsync<int>(sql, new { request.Title, request.Description });

            var task = new TaskModel
            {
                Id = id,
                Title = request.Title,
                Description = request.Description,
                IsCompleted = false
            };

            return new TaskResponse { Task = MapTask(task) };
        }

        public override async Task<TaskListResponse> ListTasks(Empty request, ServerCallContext context)
        {
            var sql = "SELECT * FROM Tasks";
            var tasks = await _db.QueryAsync<TaskModel>(sql);

            var response = new TaskListResponse();
            response.Tasks.AddRange(tasks.Select(MapTask));
            return response;
        }

        public override async Task<TaskResponse> UpdateTask(UpdateTaskRequest request, ServerCallContext context)
        {
            var sqlCheck = "SELECT * FROM Tasks WHERE Id = @Id";
            var existing = await _db.QueryFirstOrDefaultAsync<TaskModel>(sqlCheck, new { request.Id });

            if (existing == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Task with Id {request.Id} not found"));
            }

            var sql = "UPDATE Tasks SET Title = @Title, Description = @Description, IsCompleted = @IsCompleted WHERE Id = @Id";
            await _db.ExecuteAsync(sql, new { request.Title, request.Description, request.IsCompleted, request.Id });

            var updatedTask = new TaskModel
            {
                Id = request.Id,
                Title = request.Title,
                Description = request.Description,
                IsCompleted = request.IsCompleted
            };

            return new TaskResponse { Task = MapTask(updatedTask) };
        }

        public override async Task<Empty> DeleteTask(TaskIdRequest request, ServerCallContext context)
        {
            var sql = "DELETE FROM Tasks WHERE Id = @Id";
            var affected = await _db.ExecuteAsync(sql, new { request.Id });
            if (affected == 0)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Task with Id {request.Id} not found"));
            }
            return new Empty();
        }

        private TaskItem MapTask(TaskModel task) => new TaskItem
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description ?? string.Empty,
            IsCompleted = task.IsCompleted
        };
    }
}