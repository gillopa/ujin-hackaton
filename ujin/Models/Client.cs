using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ujin.Models
{
    [Table("client")]
    public class Client
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("customer_uuid")]
        public Guid? CustomerUuid { get; set; }

        [Column("username")]
        public string? Username { get; set; }

        [Column("password")]
        public string? Password { get; set; }

        [Column("customer_user_id")]
        public string? CustomerUserId { get; set; }

        [Column("deleted")]
        public DateTimeOffset? Deleted { get; set; }

        [Column("device_unique_id")]
        public string? DeviceUniqueId { get; set; }

        [Column("access_token")]
        public string? AccessToken { get; set; }

        [Column("refresh_token")]
        public string? RefreshToken { get; set; }

        [Column("token_expire_in")]
        public DateTimeOffset? TokenExpireIn { get; set; }

        [Column("tokens_code")]
        public string? TokensCode { get; set; }

        [Column("code_expire_in")]
        public DateTimeOffset? CodeExpireIn { get; set; }

        // Навигационное свойство для связи с таблицей customer, если у вас есть сущность Customer
        // public Customer? Customer { get; set; }
    }
} 