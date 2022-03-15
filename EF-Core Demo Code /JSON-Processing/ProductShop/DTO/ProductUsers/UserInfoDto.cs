namespace ProductShop.DTO.ProductUsers
{
    using System;

    public class UserInfoDto
    {
        public UserInfoDto()
        {
        }

        public int UsersCount { get; set; }

        public UserDetailsDto[] Users { get; set; }
    }
}
