using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTracker.BLL.Services.TaskService.ViewModels
{
    public class TaskVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ClassificationId { get; set; }
        public int SprintId { get; set; }
        public int ExpectedHours { get; set; }
        public int ActualHours { get; set; }
        public bool IsCompleted { get; set; }
    }
}
