namespace TaskGrpcApi.Models
{
	public class TaskModel
	{
		public int Id { get; set; }
		public string Title { get; set; } = null!;
		public string? Description { get; set; }
		public bool IsCompleted { get; set; }
	}
}