﻿using Microsoft.AspNetCore.Identity;

namespace AuthService.Models
{
    public class FavouriteLocation
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty; 
        public IdentityUser? User { get; set; }
    }
}
