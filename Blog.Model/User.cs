﻿namespace Blog.Model
{
    public class User: IEntityBase
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
