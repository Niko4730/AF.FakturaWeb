﻿namespace UdemyApp.Shared
{
    public class Rule
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Occurrence { get; set; }
        public int UserId { get; set; }
    }
}
