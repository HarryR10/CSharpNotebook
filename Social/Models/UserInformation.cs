namespace Social.Models
{
    public struct UserInformation
    {
        public UserInformation(int userId, string name, bool online)
        {
            Name = name;
            UserId = userId;
            Online = online;
        }

        public string Name { get; set; }

        public bool Online { get; set; }

        public int UserId { get; set; }
    }
}
