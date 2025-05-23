using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
using System.Text.Json.Nodes;

namespace ujin.Models
{
    public class TicketResponse
    {
        public string Command { get; set; }
        public string Message { get; set; }
        public int Error { get; set; }
        [JsonPropertyName("data")]
        public TicketData Data { get; set; }

    }

    public class TicketData
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("per_page")]
        public int PerPage { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("tickets")]
        public List<Ticket> Tickets { get; set; } = new();
    }

    public class Ticket
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("number")]
        public string Number { get; set; }
        [JsonPropertyName("priority")]
        public string Priority { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("objects")]
        public List<ObjectJson> objects { get; set; }
        [JsonPropertyName("parts")]
        public List<PartInfo> parts { get; set; }
    }
    public class ObjectJson
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class Channel
    {
        public int Id { get; set; }
        [JsonPropertyName("unread_messages")]
        public int UnreadMessages { get; set; }
    }

    public class Assignee
    {
        // Добавьте свойства для Assignee, если они есть в ответе
    }

    public class ContractingCompany
    {
        // Добавьте свойства для ContractingCompany, если они есть в ответе
    }

    public class TicketType
    {
        public int Id { get; set; }
    }

    public class UjinObject
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }

    public class Creator
    {
        public int Id { get; set; }
    }

    public class Initiator
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
    }

    public class Device
    {
        // Добавьте свойства для Device, если они есть в ответе
    }

    public class PaymentInfo
    {
        [JsonPropertyName("is_paid")]
        public bool IsPaid { get; set; }
    }

    public class PaymentStatus
    {
        public object Sum { get; set; } // Может быть null
        [JsonPropertyName("is_fully_paid")]
        public object IsFullyPaid { get; set; } // Может быть null
        public string Title { get; set; }
    }

    public class Support
    {
        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }
    }

    public class ChannelInfo
    {
        public string Title { get; set; }
        public int Id { get; set; }
        [JsonPropertyName("is_hidden")]
        public bool IsHidden { get; set; }
        [JsonPropertyName("can_write_in_ticket_resident_chat")]
        public bool CanWriteInTicketResidentChat { get; set; }
    }
}