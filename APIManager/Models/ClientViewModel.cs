﻿namespace APIManager.Models
{
    public class ClientViewModel
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public ICollection<RequestViewModel> Routes { get; set; }
    }
}
