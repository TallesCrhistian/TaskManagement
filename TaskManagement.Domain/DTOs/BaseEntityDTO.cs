﻿namespace TaskManagement.Domain.DTOs
{
    public class BaseEntityDTO
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
