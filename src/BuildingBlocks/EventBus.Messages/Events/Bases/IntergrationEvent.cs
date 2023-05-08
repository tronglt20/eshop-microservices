using System.Text.Json.Serialization;

namespace EventBus.Messages.Events.Bases
{
    public class IntergrationEvent
    {
        public IntergrationEvent()
        {
            EventId = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public IntergrationEvent(Guid id, DateTime createDate)
        {
            EventId = id;
            CreationDate = createDate;
        }

        [JsonPropertyName("Id")]
        public Guid EventId { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
