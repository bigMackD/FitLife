﻿using FitLife.Shared.Infrastructure.Response;

namespace FitLife.Contracts.Response.Meals
{
    public class EditMealResponse : IBaseResponse
    {
        public bool Success { get; set; }
        public string[] Errors { get; set; }
    }
}