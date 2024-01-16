﻿namespace TaskManager.Api.Models
{
    public class Desk : CommpnModel
    {
        public bool IsPrivate { get; set; }
        public required string Colum { get; set; }
        public required User Admin { get; set; }
        public required Project Project { get; set; }
        public List<Task> Tasks { get; set; } = [];
    }
}
