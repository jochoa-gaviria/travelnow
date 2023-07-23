
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TravelNow.DataAccess.Contracts.Entities
{
    public class Book
    {
        /// <summary>
        /// Id de reserva
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// Id de hotel
        /// </summary>
        public ObjectId HotelId { get; set; }

        /// <summary>
        /// Id de habitación
        /// </summary>
        public ObjectId RoomId { get; set; }

        /// <summary>
        /// Id Cliente
        /// </summary>
        public ObjectId ClientId { get; set; }

        /// <summary>
        /// Define si está disponible
        /// </summary>
        public UInt16 PeopleNumber { get; set; }

        /// <summary>
        /// Fecha inicio
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Fecha fin
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
